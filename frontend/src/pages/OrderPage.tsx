import { useEffect, useRef, useState } from 'react'
import type { Employee } from '../types/Employee'
import type { Table } from '../types/Table'
import AddItemPage from './AddItemPage'
import CheckoutPage from './CheckoutPage'
import type { OrderDetails } from '../types/Order'

type PageState =
  | { kind: 'loading' }
  | { kind: 'empty' }
  | { kind: 'ready'; order: OrderDetails }
  | { kind: 'error'; message: string }

type OrderPageProps = {
  table: Table
  employee: Employee
  onBack: () => void
}

function OrderPage({ table, employee, onBack }: OrderPageProps) {
  const [state, setState] = useState<PageState>({ kind: 'loading' })
  const [reloadCount, setReloadCount] = useState(0)
  const [isCreating, setIsCreating] = useState(false)
  const creating = useRef(false)
  const [showMenu, setShowMenu] = useState(false)
  const [showCheckout, setShowCheckout] = useState(false)
  const [notice, setNotice] = useState('')
  const [servingId, setServingId] = useState<number | null>(null)
  const [serveError, setServeError] = useState<string | null>(null)
  const serving = useRef(false)

  useEffect(() => {
    const controller = new AbortController()
    let active = true
    const timeout = setTimeout(() => controller.abort(), 10000)

    async function loadOrder() {
      try {
        const current = await fetch(`/api/orders/current?tableId=${table.id}`, { signal: controller.signal })
        if (current.status === 404) {
          if (active) setState({ kind: 'empty' })
          return
        }
        if (!current.ok) throw new Error('Unable to find the current order.')
        const summary: { id: number } = await current.json()
        const details = await fetch(`/api/orders/${summary.id}`, { signal: controller.signal })
        if (!details.ok) throw new Error('Unable to load order details.')
        const order: OrderDetails = await details.json()
        if (active) setState({ kind: 'ready', order })
      } catch (error) {
        if (active) setState({ kind: 'error', message: controller.signal.aborted
          ? 'The request timed out. Please refresh.'
          : error instanceof Error ? error.message : 'Unable to load the order. Please refresh.' })
      } finally {
        clearTimeout(timeout)
      }
    }

    void loadOrder()
    return () => {
      active = false
      clearTimeout(timeout)
      controller.abort()
    }
  }, [table.id, reloadCount])

  function refreshOrder() {
    setServeError(null)
    setState({ kind: 'loading' })
    setReloadCount((count) => count + 1)
  }

  async function startOrder() {
    if (creating.current) return
    creating.current = true
    setIsCreating(true)
    try {
      const response = await fetch('/api/orders', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ tableId: table.id, employeeId: employee.id }),
        signal: AbortSignal.timeout(10000),
      })
      // Another employee may have opened this table after our initial lookup.
      if (response.status === 409 || response.ok) {
        refreshOrder()
        return
      }
      throw new Error(`Unable to start the order (HTTP ${response.status}). Please refresh before trying again.`)
    } catch (error) {
      setState({ kind: 'error', message: error instanceof Error && error.name !== 'TimeoutError' && error.name !== 'TypeError'
        ? error.message
        : 'The result could not be confirmed. Refresh to check whether the order was created before trying again.' })
    } finally {
      creating.current = false
      setIsCreating(false)
    }
  }

  async function serveItem(itemId: number) {
    if (serving.current || state.kind !== 'ready' || state.order.isPaid) return
    const item = state.order.items.find((item) => item.id === itemId)
    if (item?.status !== 'Prepared') return
    serving.current = true
    setServingId(itemId)
    setServeError(null)
    setNotice('')
    try {
      const response = await fetch(`/api/orders/${state.order.id}/items/${itemId}/serve`, {
        method: 'PATCH',
        signal: AbortSignal.timeout(10000),
      })
      if (!response.ok) {
        setServeError(response.status === 409
          ? 'This item cannot be marked as served in its current state. Refresh the order.'
          : response.status === 404
            ? 'This order item could not be found. Refresh the order.'
            : 'Unable to confirm the update. Refresh the order to check its status.')
        return
      }
      setNotice(`${item.menuItemName} marked as served.`)
      refreshOrder()
    } catch {
      setServeError('Unable to confirm the update. Refresh the order to check its status.')
    } finally {
      serving.current = false
      setServingId(null)
    }
  }

  if (showCheckout && state.kind === 'ready') {
    return <CheckoutPage orderId={state.order.id} tableNumber={table.number} onBack={() => { setShowCheckout(false); refreshOrder() }} onDone={onBack} />
  }

  if (showMenu && state.kind === 'ready') {
    return <AddItemPage orderId={state.order.id} tableNumber={table.number}
      onBack={() => { setShowMenu(false); refreshOrder() }}
      onAdded={() => { setShowMenu(false); setNotice('Item added to the order.'); refreshOrder() }} />
  }

  return (
    <main className="tables-page">
      <div className="employee-bar">
        <button className="secondary-button" type="button" onClick={onBack} disabled={isCreating || servingId !== null}>Back to tables</button>
        <p>Signed in as <strong>{employee.firstName} {employee.lastName}</strong></p>
      </div>
      <header className="page-header">
        <div><p className="brand">CHAPEAU</p><h1>Table {table.number}</h1><p>Current order</p></div>
        <button type="button" onClick={refreshOrder} disabled={state.kind === 'loading' || isCreating || servingId !== null}>Refresh</button>
      </header>
      {notice && <p role="status" className="order-notice">{notice}</p>}
      {serveError && <p role="alert" className="error order-notice">{serveError}</p>}
      {state.kind === 'loading' && <p role="status">Loading order...</p>}
      {state.kind === 'error' && <p role="alert" className="error">{state.message}</p>}
      {state.kind === 'empty' && (
        <section className="order-panel">
          <h2>No open order</h2>
          <p>Start an order for Table {table.number} under your employee name?</p>
          <button className="primary-action" type="button" onClick={startOrder} disabled={isCreating}>{isCreating ? 'Starting order...' : 'Start order'}</button>
        </section>
      )}
      {state.kind === 'ready' && (
        <section className="order-panel">
          <h2>Order #{state.order.id}</h2>
          <p>{state.order.isPaid ? 'Paid' : 'Unpaid'}</p>
          {!state.order.isPaid && <div className="order-toolbar">
            <button className="primary-action" type="button" disabled={servingId !== null} onClick={() => { setNotice(''); setShowMenu(true) }}>Add item</button>
            <button className="primary-action" type="button" disabled={servingId !== null || state.order.items.length === 0 || state.order.items.some((item) => item.status !== 'Served')} onClick={() => { setNotice(''); setShowCheckout(true) }}>Checkout</button>
          </div>}
          {!state.order.isPaid && state.order.items.some((item) => item.status !== 'Served') && <p className="checkout-hint">Serve all items to enable checkout.</p>}
          {state.order.items.length === 0 ? <p>No items have been added yet.</p> : (
            <ul className="order-items">
              {state.order.items.map((item) => (
                <li key={item.id}>
                  <div><strong>{item.menuItemName}</strong><p>Quantity: {item.count}</p>{item.comment.trim() && <p>Note: {item.comment}</p>}</div>
                  <div className="order-item-actions">
                    <span className="item-status">{item.status === 'Running' ? 'Pending' : item.status === 'Prepared' ? 'Ready' : item.status}</span>
                    {item.status === 'Prepared' && !state.order.isPaid && (
                      <button type="button" className="primary-action" disabled={servingId !== null || serveError !== null} onClick={() => void serveItem(item.id)}>
                        {servingId === item.id ? 'Updating...' : 'Mark served'}
                      </button>
                    )}
                  </div>
                </li>
              ))}
            </ul>
          )}
        </section>
      )}
    </main>
  )
}

export default OrderPage
