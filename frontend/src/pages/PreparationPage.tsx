import { apiFetch } from '../api/client'
import { useEffect, useRef, useState } from 'react'
import type { Employee } from '../types/Employee'
import AppShell from '../components/AppShell'

type QueueItem = {
  orderId: number
  orderItemId: number
  tableNumber: number
  menuItemName: string
  count: number
  comment: string
  orderTime: string
  status: 'Running' | 'Preparing'
}

type PreparationPageProps = {
  station: 'kitchen' | 'bar'
  employee: Employee
  onLogout: () => void
}

function waitingTime(value: string, now: number) {
  // AddItem stores UTC. SQLite currently returns these values without a zone.
  const utcValue = /(?:Z|[+-]\d{2}:\d{2})$/i.test(value) ? value : `${value}Z`
  const timestamp = Date.parse(utcValue)
  if (!Number.isFinite(timestamp)) return 'Time unavailable'
  const minutes = Math.max(0, Math.floor((now - timestamp) / 60000))
  return minutes < 1 ? 'Less than 1 min' : `${minutes} min waiting`
}

function PreparationPage({ station, employee, onLogout }: PreparationPageProps) {
  const [items, setItems] = useState<QueueItem[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [actionError, setActionError] = useState<string | null>(null)
  const [notice, setNotice] = useState('')
  const [updatingId, setUpdatingId] = useState<number | null>(null)
  const [now, setNow] = useState(() => Date.now())
  const [lastUpdated, setLastUpdated] = useState<number | null>(null)
  const busy = useRef(false)
  const refresh = useRef<() => Promise<void>>(async () => {})
  const mutationController = useRef<AbortController | null>(null)

  useEffect(() => {
    let active = true
    let fetching = false
    let controller: AbortController | null = null

    async function loadQueue() {
      if (fetching || busy.current || !active) return
      fetching = true
      setLoading(true)
      controller = new AbortController()
      const requestController = controller
      const timeout = setTimeout(() => requestController.abort(), 10000)
      try {
        const response = await apiFetch(`/api/${station}/items`, { signal: requestController.signal })
        if (!response.ok) throw new Error('Queue request failed')
        const data: QueueItem[] = await response.json()
        if (active) {
          setItems(data)
          setError(null)
          setActionError(null)
          setLastUpdated(Date.now())
          setNow(Date.now())
        }
      } catch {
        if (active) setError('Unable to refresh the queue. Displayed items may be out of date. Please refresh before updating items.')
      } finally {
        clearTimeout(timeout)
        fetching = false
        if (active) setLoading(false)
      }
    }

    refresh.current = loadQueue
    void loadQueue()
    const interval = setInterval(() => { setNow(Date.now()); void loadQueue() }, 15000)
    return () => {
      active = false
      clearInterval(interval)
      controller?.abort()
      mutationController.current?.abort()
    }
  }, [station])

  async function updateItem(item: QueueItem) {
    if (busy.current || loading || error || actionError) return
    busy.current = true
    setUpdatingId(item.orderItemId)
    setNotice('')
    const controller = new AbortController()
    mutationController.current = controller
    const timeout = setTimeout(() => controller.abort(), 10000)
    const nextStatus = station === 'kitchen' && item.status === 'Running' ? 'Preparing' : 'Prepared'
    try {
      const response = await apiFetch(station === 'kitchen'
        ? `/api/kitchen/items/${item.orderItemId}/status`
        : `/api/bar/items/${item.orderItemId}/prepare`, {
        method: 'PATCH',
        signal: controller.signal,
        ...(station === 'kitchen' ? {
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({ status: nextStatus }),
        } : {}),
      })
      if (!response.ok) {
        setActionError(response.status === 409 || response.status === 404
          ? 'This item has changed or is no longer available. Refresh the queue.'
          : 'Unable to confirm the update. Refresh the queue before trying again.')
        return
      }
      setNotice(`${item.menuItemName}: ${nextStatus === 'Preparing' ? 'preparation started' : 'ready to serve'}.`)
      busy.current = false
      await refresh.current()
    } catch {
      setActionError('Unable to confirm the update. Refresh the queue to check the current status.')
    } finally {
      clearTimeout(timeout)
      busy.current = false
      setUpdatingId(null)
    }
  }

  const blocked = loading || updatingId !== null || error !== null || actionError !== null
  const pendingCount = items.filter((item) => station === 'kitchen' ? item.status === 'Running' : false).length
  return (
    <AppShell employee={employee} pageName={station === 'kitchen' ? 'Kitchen' : 'Bar'} pageCaption="Preparation queue" onLogout={onLogout}
      actions={<button type="button" className="button secondary" disabled={loading || updatingId !== null} onClick={() => void refresh.current()}>↻ Refresh</button>}>
      <p className="queue-summary" role="status">
        <span className="live-dot" aria-hidden="true" />
        {loading ? 'Refreshing queue...' : `${items.length} ${items.length === 1 ? 'item' : 'items'} in queue`}
        {station === 'kitchen' && pendingCount > 0 && ` · ${pendingCount} to start`}
        {lastUpdated !== null && ` · Updated ${new Date(lastUpdated).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}`}
      </p>
      {notice && <p role="status" className="order-notice">✓ {notice}</p>}
      {(error || actionError) && <p role="alert" className="order-notice error">{error ?? actionError}</p>}
      {!loading && !error && items.length === 0 && (
        <section className="order-panel empty-panel centered">
          <span className="empty-icon" aria-hidden="true">✅</span>
          <h2>All caught up</h2>
          <p>New items will appear here automatically — the queue refreshes every 15 seconds.</p>
        </section>
      )}
      <ul className="prep-grid" aria-label={`${station} queue`} aria-busy={loading}>
        {items.map((item) => (
          <li key={item.orderItemId} className={`prep-card ${item.status === 'Preparing' ? 'preparing' : 'pending'}`}>
            <div className="prep-card-heading">
              <strong>Table {item.tableNumber} · #{item.orderId}</strong>
              <span className={`item-status ${item.status === 'Preparing' ? 'prep-preparing' : 'prep-pending'}`}>{item.status === 'Preparing' ? 'Preparing' : 'Pending'}</span>
            </div>
            <div className="prep-content">
              <h2>{item.menuItemName}</h2>
              <p className="prep-quantity">Quantity: {item.count}</p>
              <p className="prep-time">⏱ {waitingTime(item.orderTime, now)}</p>
              {item.comment.trim() && <p className="prep-note">Note: {item.comment}</p>}
            </div>
            <button className={`button ${station === 'kitchen' && item.status === 'Running' ? 'prep-start' : 'prep-ready'}`} type="button" disabled={blocked} onClick={() => void updateItem(item)}>
              {updatingId === item.orderItemId ? 'Updating...' : station === 'kitchen' && item.status === 'Running' ? 'Start preparing' : 'Mark ready'}
            </button>
          </li>
        ))}
      </ul>
    </AppShell>
  )
}

export default PreparationPage
