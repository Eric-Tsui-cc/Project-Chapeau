import { apiFetch } from '../api/client'
import { useEffect, useState } from 'react'
import type { Employee } from '../types/Employee'

import type { Table } from '../types/Table'
import AppShell from '../components/AppShell'

type TablesPageProps = {
  employee: Employee
  onLogout: () => void
  onSelectTable: (table: Table) => void
}

function TablesPage({ employee, onLogout, onSelectTable }: TablesPageProps) {
  const [tables, setTables] = useState<Table[]>([])
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [reloadCount, setReloadCount] = useState(0)
  const freeCount = tables.filter((table) => table.status === 'Free').length
  const occupiedCount = tables.length - freeCount

  useEffect(() => {
    const controller = new AbortController()
    const timeout = setTimeout(() => controller.abort(), 10000)
    let active = true

    async function loadTables() {
      try {
        const response = await apiFetch('/api/tables', { signal: controller.signal })
        if (!response.ok) {
          throw new Error(`Request failed: HTTP ${response.status}`)
        }
        const data: Table[] = await response.json()
        if (active) setTables(data)
      } catch (error) {
        if (active) {
          setError(controller.signal.aborted
            ? 'The request timed out. Please try again.'
            : error instanceof Error ? error.message : 'Unable to load tables. Please try again.')
        }
      } finally {
        clearTimeout(timeout)
        if (active) setIsLoading(false)
      }
    }

    void loadTables()


    return () => {
      active = false
      clearTimeout(timeout)
      controller.abort()
    }
  }, [reloadCount])

  function refreshTables() {
    setIsLoading(true)
    setError(null)
    setReloadCount((count) => count + 1)
  }


  return (
    <AppShell employee={employee} pageName="Tables" pageCaption="Floor overview" onLogout={onLogout}
      actions={<button type="button" className="button secondary" onClick={refreshTables} disabled={isLoading}>↻ Refresh</button>}>
      <header className="page-head">
        <div>
          <h1>Good {new Date().getHours() < 12 ? 'morning' : new Date().getHours() < 18 ? 'afternoon' : 'evening'}, {employee.firstName}</h1>
          <p id="table-open-hint" className="page-caption">Click or tap a table to view its order.</p>
        </div>
      </header>

      <section aria-label="Tables" aria-busy={isLoading}>
        <div className="stats-row" aria-hidden={isLoading}>
          <div className="stat-chip free"><strong>{freeCount}</strong> tables free</div>
          <div className="stat-chip occupied"><strong>{occupiedCount}</strong> tables occupied</div>
          <div className="stat-chip"><strong>{tables.length}</strong> tables total</div>
        </div>

        {isLoading && <div className="order-panel"><p role="status">Loading tables...</p></div>}
        {!isLoading && error && <div className="order-panel"><p role="alert" className="error">{error}</p><p>Please try refreshing the tables.</p></div>}
        {!isLoading && !error && tables.length === 0 && <div className="order-panel"><p>No tables found.</p></div>}
        {!isLoading && !error && tables.length > 0 && (
          <ul className="table-grid">
            {tables.map((table) => (
              <li key={table.id}>
                <button
                  type="button"
                  className="table-card"
                  aria-describedby="table-open-hint"
                  onClick={() => onSelectTable(table)}
                >
                  <div className="table-top">
                    <span className="table-number">
                      {String(table.number).padStart(2, '0')}
                      <small>Table</small>
                    </span>
                  </div>
                  <span className="table-capacity">
                    {table.capacity ? `${table.capacity} seats` : '\u00A0'}
                  </span>
                  <span className={`status ${table.status === 'Free' ? 'free' : 'occupied'}`}>
                    <span className="status-dot" aria-hidden="true" />
                    {table.status === 'Free' ? 'Available' : 'Occupied'}
                  </span>
                </button>
              </li>
            ))}
          </ul>
        )}
      </section>
    </AppShell>
  )
}

export default TablesPage
