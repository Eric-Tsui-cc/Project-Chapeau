import type { ReactNode } from 'react'
import type { Employee } from '../types/Employee'

type AppShellProps = {
  employee: Employee
  pageName: string
  pageCaption: string
  /** Standard pages provide a real logout; embedded pages (order flow) may omit it. */
  onLogout?: () => void
  /** Optional back button shown in the top bar (e.g. "Back to tables"). */
  back?: { label: string; onClick: () => void; disabled?: boolean }
  /** Right-hand toolbar area (e.g. Refresh button). */
  actions?: ReactNode
  children: ReactNode
}

const roleLabels: Record<Employee['role'], string> = {
  Waiter: 'Waiter',
  Chef: 'Chef',
  Bartender: 'Bartender',
}

function initials(employee: Employee) {
  return `${employee.firstName[0] ?? ''}${employee.lastName[0] ?? ''}`.toUpperCase()
}

/** Shared application frame: dark top bar plus page content area. */
function AppShell({ employee, pageName, pageCaption, onLogout, back, actions, children }: AppShellProps) {
  const showLogout = onLogout !== undefined
  return (
    <div className="app-shell">
      <header className="app-bar">
        <div className="app-bar-left">
          {back && (
            <button type="button" className="button ghost" onClick={back.onClick} disabled={back.disabled}>
              ← {back.label}
            </button>
          )}
          <div className="app-brand">
            <span className="app-brand-mark" aria-hidden="true">C</span>
            <span className="app-brand-name">CHAPEAU</span>
          </div>
          <div className="app-bar-divider" aria-hidden="true" />
          <div className="app-bar-title">
            <strong>{pageName}</strong>
            <span>{pageCaption}</span>
          </div>
        </div>
        <div className="app-bar-right">
          {actions}
          <div className="employee-chip">
            <span className="employee-avatar" aria-hidden="true">{initials(employee)}</span>
            <span className="employee-meta">
              <strong>{employee.firstName} {employee.lastName}</strong>
              <span>{roleLabels[employee.role]}</span>
            </span>
          </div>
          {showLogout && <button type="button" className="button ghost" onClick={onLogout}>Sign out</button>}
        </div>
      </header>
      <main className="app-content">
        {children}
      </main>
    </div>
  )
}

export default AppShell
