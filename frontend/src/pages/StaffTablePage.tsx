import { useEffect, useState } from 'react'
import { Link, useLocation, useNavigate, useParams } from 'react-router'
import { apiFetch } from '../api/client'
import RouteMessage from '../components/RouteMessage'
import type { Employee } from '../types/Employee'
import type { Table } from '../types/Table'
import OrderPage from './OrderPage'
import type { OrderView } from './OrderPage'

type TableState =
  | { kind: 'loading' }
  | { kind: 'ready'; table: Table }
  | { kind: 'missing' }
  | { kind: 'error' }

function TableOrderLoader({ tableId, employee, view }: { tableId: number; employee: Employee; view: OrderView }) {
  const navigate = useNavigate()
  const location = useLocation()
  const [state, setState] = useState<TableState>({ kind: 'loading' })
  const [attempt, setAttempt] = useState(0)
  const orderPath = `/staff/tables/${tableId}`

  useEffect(() => {
    const controller = new AbortController()
    const timeout = setTimeout(() => controller.abort(), 10000)
    let active = true
    async function loadTable() {
      try {
        const response = await apiFetch('/api/tables', { signal: controller.signal })
        if (!response.ok) throw new Error('Table lookup failed')
        const tables: Table[] = await response.json()
        const table = tables.find((candidate) => candidate.id === tableId)
        if (active) setState(table ? { kind: 'ready', table } : { kind: 'missing' })
      } catch {
        if (active) setState({ kind: 'error' })
      } finally {
        clearTimeout(timeout)
      }
    }
    void loadTable()
    return () => { active = false; clearTimeout(timeout); controller.abort() }
  }, [tableId, attempt])

  if (state.kind === 'loading') return <main className="session-screen"><p role="status">Loading table...</p></main>
  if (state.kind === 'missing') return <RouteMessage title="Table not found" message="This table is not available. Please choose a table from the floor overview." backTo="/staff/tables" backLabel="Back to tables" />
  if (state.kind === 'error') return (
    <main className="session-screen"><section className="session-card">
      <h1>Unable to load table</h1>
      <p role="alert">Check your connection and try again.</p>
      <button className="button primary" type="button" onClick={() => { setState({ kind: 'loading' }); setAttempt((value) => value + 1) }}>Retry</button>
      <Link to="/staff/tables">Back to tables</Link>
    </section></main>
  )

  function changeView(next: OrderView) {
    if (next === 'order') {
      if (location.state?.fromOrder === orderPath) navigate(-1)
      else navigate(orderPath, { replace: true })
    } else {
      navigate(`${orderPath}/${next}`, { state: { fromOrder: orderPath } })
    }
  }

  return <OrderPage table={state.table} employee={employee} view={view} onViewChange={changeView} onBack={() => {
    if (location.state?.fromTables) navigate(-1)
    else navigate('/staff/tables', { replace: true })
  }} />
}

export default function StaffTablePage({ employee }: { employee: Employee }) {
  const { tableId = '', '*': subpage = '' } = useParams()
  const numericId = Number(tableId)
  if (!/^[1-9]\d*$/.test(tableId) || !Number.isSafeInteger(numericId) || numericId > 2147483647 || !['', 'items', 'checkout'].includes(subpage)) {
    return <RouteMessage title="Page not found" message="Choose a table from the floor overview to continue." backTo="/staff/tables" backLabel="Back to tables" />
  }
  const view: OrderView = subpage === 'items' || subpage === 'checkout' ? subpage : 'order'
  return <TableOrderLoader key={tableId} tableId={numericId} employee={employee} view={view} />
}
