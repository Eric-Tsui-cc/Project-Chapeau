import { useEffect, useRef, useState } from 'react'
import LoginPage from './pages/LoginPage'
import TablesPage from './pages/TablesPage'
import OrderPage from './pages/OrderPage'
import PreparationPage from './pages/PreparationPage'
import { csrfFetch, SESSION_EXPIRED_EVENT } from './api/client'
import type { Table } from './types/Table'
import type { Employee } from './types/Employee'
import './App.css'

function App() {
  const [employee, setEmployee] = useState<Employee | null>(null)
  const [selectedTable, setSelectedTable] = useState<Table | null>(null)
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
      setSelectedTable(null)
      setLogoutError(false)
      setLoginNotice('Your session has expired. Please sign in again.')
    }
    window.addEventListener(SESSION_EXPIRED_EVENT, expireSession)
    return () => window.removeEventListener(SESSION_EXPIRED_EVENT, expireSession)
  }, [])

  function login(currentEmployee: Employee) {
    setSelectedTable(null)
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
      setSelectedTable(null)
      setLoginNotice('')
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
  if (employee === null) return <LoginPage onLogin={login} notice={loginNotice} />

  const page = employee.role === 'Chef' || employee.role === 'Bartender'
    ? <PreparationPage key={employee.role} station={employee.role === 'Chef' ? 'kitchen' : 'bar'} employee={employee} onLogout={() => void logout()} />
    : selectedTable !== null
      ? <OrderPage key={selectedTable.id} table={selectedTable} employee={employee} onBack={() => setSelectedTable(null)} />
      : <TablesPage employee={employee} onLogout={() => void logout()} onSelectTable={setSelectedTable} />
  return (
    <>
      {logoutError && <p className="session-error" role="alert">Sign-out could not be confirmed. Please try Sign out again.</p>}
      {page}
    </>
  )
}
export default App