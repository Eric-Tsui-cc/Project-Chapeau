import { useEffect, useState } from 'react'
import type { Employee } from '../types/Employee'

import type { Table } from '../types/Table'

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
  const [selectedId, setSelectedId] = useState<number | null>(null)
  const selectedTable = tables.find((table) => table.id === selectedId)

  useEffect(() => {
    const controller = new AbortController()
    const timeout = setTimeout(() => controller.abort(), 10000)
    let active = true

    async function loadTables() {
      try {
        const response = await fetch('/api/tables', { signal: controller.signal })
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
    setSelectedId(null)
    setReloadCount((count) => count + 1)
  }

  return (
    <main className="tables-page tables-overview">
      <div className="employee-bar">
        <p>Signed in as <strong>{employee.firstName} {employee.lastName}</strong></p>
        <button type="button" className="secondary-button" onClick={onLogout}>Sign out</button>
      </div>
      <header className="page-header">
        <div>
          <p className="brand">CHAPEAU</p>
          <h1>Tables</h1>
          <p>View current table availability.</p>
        </div>
        <button type="button" onClick={refreshTables} disabled={isLoading}>
          Refresh
        </button>
      </header>

      <section aria-label="Tables" aria-busy={isLoading}>
        {isLoading && <p role="status">Loading tables...</p>}
        {!isLoading && error && <p role="alert" className="error">{error} Please try refreshing the tables.</p>}
        {!isLoading && !error && tables.length === 0 && <p>No tables found.</p>}
        {!isLoading && !error && tables.length > 0 && (
          <ul className="table-grid">
            {tables.map((table) => (
              <li key={table.id}>
                <button
                  type="button"
                  className={`table-card${selectedId === table.id ? ' is-selected' : ''}`}
                  aria-pressed={selectedId === table.id}
                  onClick={() => setSelectedId((currentId) => currentId === table.id ? null : table.id)}
                >
                {selectedId === table.id && <span className="selection-mark" aria-hidden="true">✓</span>}
                <span className="table-number">Table {table.number}</span>
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
      <footer className="table-actions">
        <p role="status">{selectedTable ? `Table ${selectedTable.number} selected` : 'Select a table to continue.'}</p>
        <button
          className="primary-action"
          type="button"
          disabled={!selectedTable || isLoading || error !== null}
          onClick={() => { if (selectedTable) onSelectTable(selectedTable) }}
        >View order</button>
      </footer>
    </main>
  )
}

export default TablesPage


