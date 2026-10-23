import CustomerMenuPage from './pages/CustomerMenuPage'
import { useEffect, useRef, useState } from 'react'
import { Navigate, Route, Routes, useLocation, useNavigate } from 'react-router'
import LoginPage from './pages/LoginPage'
import TablesPage from './pages/TablesPage'
import StaffTablePage from './pages/StaffTablePage'
import PreparationPage from './pages/PreparationPage'
import RouteMessage from './components/RouteMessage'
import { csrfFetch, SESSION_EXPIRED_EVENT } from './api/client'
import type { Employee } from './types/Employee'
import './App.css'

function staffHome(employee: Employee) {
  return employee.role === 'Chef' ? '/staff/kitchen'
    : employee.role === 'Bartender' ? '/staff/bar' : '/staff/tables'
}

function StaffApp() {
  const location = useLocation()
  const navigate = useNavigate()
  const [employee, setEmployee] = useState<Employee | null>(null)
  const [isChecking, setIsChecking] = useState(true)
  const [checkError, setCheckError] = useState(false)
  const [checkAttempt, setCheckAttempt] = useState(0)
  const [isSigningOut, setIsSigningOut] = useState(false)
  const [logoutError, setLogoutError] = useState(false)
  const [loginNotice, setLoginNotice] = useState('')
  const logoutInFlight = useRef(false)

  useEffect(() => {
    const controller = new AbortController()
    let active = true
    const timeout = setTimeout(() => controller.abort(), 10000)
    async function restoreSession() {
      try {
        const response = await fetch('/api/auth/me', {
          credentials: 'same-origin', cache: 'no-store', signal: controller.signal,
        })
        if (response.status === 401) {
          if (active) setEmployee(null)
          return
        }
        if (!response.ok) throw new Error('Session check failed')
        const currentEmployee: Employee = await response.json()
        if (active) setEmployee(currentEmployee)
      } catch {
        if (active) setCheckError(true)
      } finally {
        clearTimeout(timeout)
        if (active) setIsChecking(false)
      }
    }
    void restoreSession()
    return () => { active = false; clearTimeout(timeout); controller.abort() }
  }, [checkAttempt])

  useEffect(() => {
    function expireSession() {
      setEmployee(null)
      setLogoutError(false)
      setLoginNotice('Your session has expired. Please sign in again.')
    }
    window.addEventListener(SESSION_EXPIRED_EVENT, expireSession)
    return () => window.removeEventListener(SESSION_EXPIRED_EVENT, expireSession)
  }, [])

  function login(currentEmployee: Employee) {
    setLoginNotice('')
    setLogoutError(false)
    setEmployee(currentEmployee)
  }

  async function logout() {
    if (logoutInFlight.current) return
    logoutInFlight.current = true
    setIsSigningOut(true)
    setLogoutError(false)
    try {
      const response = await csrfFetch('/api/auth/logout', {
        method: 'POST', credentials: 'same-origin', signal: AbortSignal.timeout(10000),
      })
      if (!response.ok) throw new Error('Sign-out failed')
      setEmployee(null)
      setLoginNotice('')
      navigate('/staff/login', { replace: true })
    } catch {
      setLogoutError(true)
    } finally {
      logoutInFlight.current = false
      setIsSigningOut(false)
    }
  }

  if (isChecking || checkError || isSigningOut) {
    return (
      <main className="session-screen">
        <section className="session-card" aria-labelledby="session-title">
          <h1 id="session-title">CHAPEAU</h1>
          {isChecking || isSigningOut ? (
            <p role="status">{isSigningOut ? 'Signing out...' : 'Checking your session...'}</p>
          ) : (
            <>
              <p role="alert">Unable to check your session. Check that the server is running and try again.</p>
              <button className="button primary" type="button" onClick={() => {
                setCheckError(false)
                setIsChecking(true)
                setCheckAttempt((value) => value + 1)
              }}>Retry</button>
            </>
          )}
        </section>
      </main>
    )
  }
  if (employee === null) {
    if (location.pathname !== '/staff/login') {
      return <Navigate to="/staff/login" replace state={{ from: location.pathname }} />
    }
    return <LoginPage onLogin={login} notice={loginNotice} />
  }

  const home = staffHome(employee)
  const from: unknown = location.state?.from
  const loginDestination = typeof from === 'string' && from.startsWith('/staff/') && from !== '/staff/login'
    ? from : home
  const roleRedirect = <Navigate to={home} replace />
  return (
    <>
      {logoutError && <p className="session-error" role="alert">Sign-out could not be confirmed. Please try Sign out again.</p>}
      <Routes>
        <Route index element={roleRedirect} />
        <Route path="login" element={<Navigate to={loginDestination} replace />} />
        <Route path="tables" element={employee.role === 'Waiter'
          ? <TablesPage employee={employee} onLogout={() => void logout()} onSelectTable={(table) => navigate(`/staff/tables/${table.id}`, { state: { fromTables: true } })} />
          : roleRedirect} />
        <Route path="tables/:tableId/*" element={employee.role === 'Waiter'
          ? <StaffTablePage employee={employee} /> : roleRedirect} />
        <Route path="kitchen" element={employee.role === 'Chef'
          ? <PreparationPage key="kitchen" station="kitchen" employee={employee} onLogout={() => void logout()} /> : roleRedirect} />
        <Route path="bar" element={employee.role === 'Bartender'
          ? <PreparationPage key="bar" station="bar" employee={employee} onLogout={() => void logout()} /> : roleRedirect} />
        <Route path="*" element={<RouteMessage title="Page not found" message="Return to your workspace to continue." backTo={home} backLabel="Back to workspace" />} />
      </Routes>
    </>
  )
}
// A dedicated public entry point; staff session checks only run in StaffApp.
function App() {
  const location = useLocation()
  const isCustomerPage = location.pathname.replace(/\/+$/, '') === '/menu'

  useEffect(() => {
    const favicon = document.querySelector<HTMLLinkElement>('link[rel="icon"]')
    if (favicon) favicon.href = isCustomerPage ? '/favicon-customer.svg' : '/favicon.svg'
    document.title = isCustomerPage ? 'Chapeau | Menu' : 'Chapeau | Staff'
  }, [isCustomerPage])

  return (
    <Routes>
      <Route path="/" element={<Navigate to="/menu" replace />} />
      <Route path="/menu" element={<CustomerMenuPage />} />
      <Route path="/staff/*" element={<StaffApp />} />
      <Route path="*" element={<RouteMessage title="Page not found" message="The page you requested is not available." backTo="/menu" backLabel="Browse the menu" />} />
    </Routes>
  )
}

export default App
