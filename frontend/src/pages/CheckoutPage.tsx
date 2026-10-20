import { apiFetch } from '../api/client'
import { useEffect, useRef, useState } from 'react'
import type { SubmitEvent } from 'react'
import type { OrderDetails } from '../types/Order'

type Bill = {
  id: number
  orderId: number
  tableNumber: number
  amount: number
  tip: number
  paymentMethod: 'Cash' | 'Debit' | 'Credit'
}

type CheckoutPageProps = {
  orderId: number
  tableNumber: number
  onBack: () => void
  onDone: () => void
}

const cents = (value: number) => Math.round(value * 100)
const money = (value: number) => `$${(value / 100).toFixed(2)}`

function CheckoutPage({ orderId, tableNumber, onBack, onDone }: CheckoutPageProps) {
  const [order, setOrder] = useState<OrderDetails | null>(null)
  const [loading, setLoading] = useState(true)
  const [paymentMethod, setPaymentMethod] = useState<Bill['paymentMethod'] | null>(null)
  const [tip, setTip] = useState('0')
  const [error, setError] = useState<string | null>(null)
  const [submitting, setSubmitting] = useState(false)
  const [blocked, setBlocked] = useState(false)
  const [bill, setBill] = useState<Bill | null>(null)
  const inFlight = useRef(false)

  useEffect(() => {
    const controller = new AbortController()
    let active = true
    const timer = setTimeout(() => controller.abort(), 10000)
    async function load() {
      try {
        const response = await apiFetch(`/api/orders/${orderId}`, { signal: controller.signal })
        if (!response.ok) throw new Error('Unable to load order')
        const data: OrderDetails = await response.json()
        if (active) setOrder(data)
      } catch {
        if (active) setError('Unable to load checkout. Return to the order and try again.')
      } finally {
        clearTimeout(timer)
        if (active) setLoading(false)
      }
    }
    void load()
    return () => { active = false; clearTimeout(timer); controller.abort() }
  }, [orderId])

  const pricesValid = order !== null && order.items.every((item) => Number.isFinite(item.unitPrice) && item.unitPrice >= 0)
  const subtotal = pricesValid ? order.items.reduce((sum, item) => sum + cents(item.unitPrice) * item.count, 0) : 0
  const tipValid = /^\d+(\.\d{1,2})?$/.test(tip) && Number.isSafeInteger(cents(Number(tip)))
  const tipCents = tipValid ? cents(Number(tip)) : 0
  const eligible = pricesValid && !order.isPaid && order.items.length > 0 && order.items.every((item) => item.status === 'Served')

  async function checkout(event: SubmitEvent<HTMLFormElement>) {
    event.preventDefault()
    if (inFlight.current || blocked || !eligible || !paymentMethod || !tipValid) return
    inFlight.current = true
    setSubmitting(true)
    setError(null)
    let submitted = false
    try {
      // Check for newly added items or price changes before recording payment.
      const check = await apiFetch(`/api/orders/${orderId}`, { signal: AbortSignal.timeout(10000) })
      if (!check.ok) throw new Error('Unable to verify the order')
      const latest: OrderDetails = await check.json()
      const signature = (value: OrderDetails) => JSON.stringify(value.items.map((item) => [item.id, item.count, item.unitPrice, item.status]).sort((a, b) => Number(a[0]) - Number(b[0])))
      if (latest.isPaid || signature(latest) !== signature(order)) {
        setOrder(latest)
        setError('The order has changed. Review the updated details before confirming payment.')
        return
      }
      submitted = true
      const response = await apiFetch('/api/bill', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ orderId, paymentMethod, tip: tipCents / 100 }),
        signal: AbortSignal.timeout(10000),
      })
      if (!response.ok) {
        setBlocked(true)
        setError(response.status === 409
          ? 'Checkout was refused: the order may already be paid or contain unserved items. Return to the order to check.'
          : response.status === 400 || response.status === 404
            ? 'Checkout was refused. Return to the order and check its details.'
            : 'Payment status is uncertain. Do not collect payment again. Return to the order to check its status.')
        return
      }
      const result: Bill = await response.json()
      setBill(result)
    } catch {
      setBlocked(submitted)
      setError(submitted
        ? 'Payment status is uncertain. Do not collect payment again. Return to the order to check its status.'
        : 'Unable to verify the order. Check your connection and try again.')
    } finally {
      inFlight.current = false
      setSubmitting(false)
    }
  }

  if (bill) return (
    <main className="nested-page">
      <header className="page-head"><div>
        <h1>Payment recorded</h1>
        <p className="page-caption">The order is paid and the table is available again.</p>
      </div></header>
      <section className="order-panel receipt">
        <span className="receipt-icon" aria-hidden="true">✓</span>
        <h2>Receipt #{bill.id}</h2>
        <p className="order-count-line">Table {bill.tableNumber} · Order #{bill.orderId}</p>
        <dl className="bill-totals">
          <div><dt>Order amount</dt><dd>{money(cents(bill.amount))}</dd></div>
          <div><dt>Tip</dt><dd>{money(cents(bill.tip))}</dd></div>
          <div className="bill-total"><dt>Total paid</dt><dd>{money(cents(bill.amount) + cents(bill.tip))}</dd></div>
          <div><dt>Payment method</dt><dd>{bill.paymentMethod}</dd></div>
        </dl>
        <div className="receipt-actions">
          <button className="button primary" type="button" onClick={onDone}>Back to tables</button>
        </div>
      </section>
    </main>
  )

  return (
    <main className="nested-page">
      <div className="page-toolbar">
        <button className="button secondary" type="button" onClick={onBack} disabled={submitting}>← Back to order</button>
        <p className="context">Table {tableNumber} · Order #{orderId}</p>
      </div>
      <header className="page-head">
        <div>
          <h1>Checkout</h1>
          <p className="page-caption">Review the bill and record payment after it has been received.</p>
        </div>
      </header>
      {loading && <div className="order-panel"><p role="status">Loading checkout...</p></div>}
      {error && <p className="order-notice error" role="alert">{error}</p>}
      {order && <div className="checkout-layout">
        <section className="order-panel"><h2>Order details</h2>
          <ul className="order-items checkout-items">{order.items.map((item) => <li key={item.id}><div className="order-item-main"><strong>{item.menuItemName}</strong><p>{item.count} × {Number.isFinite(item.unitPrice) ? money(cents(item.unitPrice)) : 'Price unavailable'}</p></div><strong>{Number.isFinite(item.unitPrice) ? money(cents(item.unitPrice) * item.count) : '—'}</strong></li>)}</ul>
          {!eligible && <div className={`checkout-blocked ${order.isPaid ? '' : 'notice'}`} role="status">{order.isPaid ? '🔒 This order is already paid.' : !pricesValid ? 'ℹ Order prices are unavailable. Return to the order and refresh.' : order.items.length === 0 ? 'ℹ An empty order cannot be checked out.' : '⏳ All items must be served before checkout.'}</div>}
        </section>
        <section className="order-panel"><h2>Payment</h2>
          <form className="checkout-form" onSubmit={checkout}>
            <fieldset disabled={submitting || blocked || !eligible}>
              <legend>Payment method</legend>
              <div className="filter-buttons">
                {(['Cash', 'Debit', 'Credit'] as const).map((method) => (
                  <button key={method} type="button" className={`filter-button${paymentMethod === method ? ' is-active' : ''}`} aria-pressed={paymentMethod === method} onClick={() => setPaymentMethod(method)}>{method}</button>
                ))}
              </div>
            </fieldset>
            <label htmlFor="checkout-tip">Tip (optional)</label>
            <input id="checkout-tip" type="text" inputMode="decimal" value={tip} onChange={(event) => setTip(event.target.value)} disabled={submitting || blocked || !eligible} aria-describedby={!tipValid ? 'tip-error' : undefined} />
            {!tipValid && <p id="tip-error" className="error">Enter a non-negative amount with up to two decimal places, or 0.</p>}
            <dl className="bill-totals">
              <div><dt>Subtotal</dt><dd>{pricesValid ? money(subtotal) : '—'}</dd></div>
              <div><dt>Tip</dt><dd>{tipValid ? money(tipCents) : '—'}</dd></div>
              <div className="bill-total"><dt>Total</dt><dd>{pricesValid && tipValid ? money(subtotal + tipCents) : '—'}</dd></div>
            </dl>
            <button className="button success payment-button" type="submit" disabled={submitting || blocked || !eligible || !paymentMethod || !tipValid}>{submitting ? 'Recording payment...' : 'Confirm payment'}</button>
          </form>
        </section>
      </div>}
    </main>
  )
}

export default CheckoutPage
