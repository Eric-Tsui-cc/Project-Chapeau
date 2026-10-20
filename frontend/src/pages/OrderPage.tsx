import { apiFetch } from '../api/client'
import { useEffect, useRef, useState } from 'react'
import type { Employee } from '../types/Employee'
import type { Table } from '../types/Table'
import AppShell from '../components/AppShell'
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

const statusLabels: Record<OrderDetails['items'][number]['status'], string> = {
  Running: 'Pending',
  Preparing: 'Preparing',
  Prepared: 'Ready',
  Served: 'Served',
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
        const current = await apiFetch(`/api/orders/current?tableId=${table.id}`, { signal: controller.signal })
        if (current.status === 404) {
          if (active) setState({ kind: 'empty' })
          return
        }
        if (!current.ok) throw new Error('Unable to find the current order.')
        const summary: { id: number } = await current.json()
        const details = await apiFetch(`/api/orders/${summary.id}`, { signal: controller.signal })
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
      const response = await apiFetch('/api/orders', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ tableId: table.id }),
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
      const response = await apiFetch(`/api/orders/${state.order.id}/items/${itemId}/serve`, {
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

  const busy = isCreating || servingId !== null || state.kind === 'loading'
  const allServed = state.kind === 'ready' && state.order.items.every((item) => item.status === 'Served')
  const itemCount = state.kind === 'ready' ? state.order.items.reduce((sum, item) => sum + item.count, 0) : 0

  return (
    <AppShell employee={employee} pageName={`Table ${table.number}`} pageCaption={state.kind === 'ready' ? `Order #${state.order.id}` : 'Current order'}
      back={{ label: 'Back to tables', onClick: onBack, disabled: busy }}
      actions={<button type="button" className="button secondary" onClick={refreshOrder} disabled={busy}>↻ Refresh</button>}>
      {notice && <p role="status" className="order-notice">✓ {notice}</p>}
      {serveError && <p role="alert" className="error order-notice">{serveError}</p>}
      {state.kind === 'loading' && <div className="order-panel"><p role="status">Loading order...</p></div>}
      {state.kind === 'error' && <div className="order-panel"><p role="alert" className="error">{state.message}</p></div>}
      {state.kind === 'empty' && (
        <section className="order-panel empty-panel">
          <span className="empty-icon" aria-hidden="true">🍽</span>
          <h2>No open order</h2>
          <p>Start an order for Table {table.number} under your employee name to begin taking items.</p>
          <button className="button primary" type="button" onClick={startOrder} disabled={isCreating}>{isCreating ? 'Starting order...' : 'Start order'}</button>
        </section>
      )}
      {state.kind === 'ready' && (
        <section className="order-panel">
          <div className="order-head">
            <h2>Order #{state.order.id}</h2>
            <span className={`status ${state.order.isPaid ? 'free' : 'occupied'}`}>
              <span className="status-dot" aria-hidden="true" />
              {state.order.isPaid ? 'Paid' : 'Unpaid'}
            </span>
          </div>
          <p className="order-count-line">{itemCount} {itemCount === 1 ? 'item' : 'items'} on this order.</p>
          {!state.order.isPaid && <div className="order-toolbar">
            <button className="button primary" type="button" disabled={servingId !== null} onClick={() => { setNotice(''); setShowMenu(true) }}>+ Add item</button>
            <button className="button success" type="button" disabled={servingId !== null || state.order.items.length === 0 || !allServed} onClick={() => { setNotice(''); setShowCheckout(true) }}>Checkout</button>
          </div>}
          {!state.order.isPaid && !allServed && <p className="checkout-hint">ⓘ Serve all items to enable checkout.</p>}
          {state.order.items.length === 0 ? <p className="menu-empty">No items have been added yet.</p> : (
            <ul className="order-items">
              {state.order.items.map((item) => (
                <li key={item.id}>
                  <div className="order-item-main">
                    <strong>{item.menuItemName}</strong>
                    <p>Quantity: {item.count}</p>
                    {item.comment.trim() && <p className="order-note">Note: {item.comment}</p>}
                  </div>
                  <div className="order-item-actions">
                    <span className={`item-status status-${item.status.toLowerCase()}`}>{statusLabels[item.status]}</span>
                    {item.status === 'Prepared' && !state.order.isPaid && (
                      <button type="button" className="button secondary" disabled={servingId !== null || serveError !== null} onClick={() => void serveItem(item.id)}>
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
    </AppShell>
  )
}

export default OrderPage
