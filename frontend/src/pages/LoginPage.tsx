import { csrfFetch } from '../api/client'
import { useRef, useState } from 'react'
import type { SubmitEvent } from 'react'
import type { Employee } from '../types/Employee'

type LoginPageProps = {
  onLogin: (employee: Employee) => void
  notice?: string
}

const isDemo = import.meta.env.VITE_DEMO === '1'

function LoginPage({ onLogin, notice = '' }: LoginPageProps) {
  const [pin, setPin] = useState('')
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const inFlight = useRef(false)

  async function handleSubmit(event: SubmitEvent<HTMLFormElement>) {
    event.preventDefault()
    if (inFlight.current) return
    setError(null)

    // The API currently accepts a non-negative C# int, not a PIN string.
    if (!/^\d+$/.test(pin) || Number(pin) > 2147483647) {
      setError('Enter a valid numeric PIN.')
      return
    }

    inFlight.current = true
    setIsSubmitting(true)
    try {
      const response = await csrfFetch('/api/auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ pin: Number(pin) }),
        signal: AbortSignal.timeout(10000),
      })

      if (response.status === 401) {
        setError('Invalid PIN. Please try again.')
        return
      }
      if (!response.ok) {
        throw new Error('Unable to sign in. Please try again.')
      }

      const employee: Employee = await response.json()
      setPin('')
      onLogin(employee)
    } catch {
      setError('Unable to sign in. Please check your connection and try again.')
    } finally {
      inFlight.current = false
      setIsSubmitting(false)
    }
  }

  return (
    <main className="login-page">
      <section className="login-hero" aria-hidden="true">
        <div className="brand">
          <span className="app-brand-mark">C</span>
          <span className="app-brand-name">CHAPEAU</span>
        </div>
        <h1>Service,<br />served smoothly.</h1>
        <p>The tabletop console for waiters, kitchen and bar. Take orders, follow every course and settle the bill in one place.</p>
        <div className="login-hero-points">
          <span>Order at the table</span>
          <span>Live kitchen & bar queues</span>
          <span>Checkout in two taps</span>
        </div>
      </section>
      <section className="login-side">
        <div className="login-card" aria-labelledby="login-heading">
          <h2 id="login-heading">Staff sign in</h2>
          <p>Enter your employee PIN to continue.</p>
          {notice && <p className="session-login-notice" role="status">{notice}</p>}
          <form onSubmit={handleSubmit} aria-busy={isSubmitting}>
            <label htmlFor="pin">Employee PIN</label>
            <input
              id="pin"
              name="pin"
              type="password"
              inputMode="numeric"
              autoComplete="current-password"
              pattern="[0-9]+"
              maxLength={10}
              required
              value={pin}
              onChange={(event) => setPin(event.target.value)}
              disabled={isSubmitting}
              aria-describedby={error ? 'login-error' : undefined}
            />
            {error && <p id="login-error" className="error" role="alert">{error}</p>}
            <button className="button primary" type="submit" disabled={isSubmitting}>
              {isSubmitting ? 'Signing in...' : 'Sign in'}
            </button>
          </form>
          {isDemo && <p className="login-hint"><span className="demo-badge">DEMO</span> Demo PINs: 1234 · 3456 · 4567</p>}
        </div>
      </section>
    </main>
  )
}

export default LoginPage
