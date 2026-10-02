import { useRef, useState } from 'react'
import type { SubmitEvent } from 'react'
import type { Employee } from '../types/Employee'

type LoginPageProps = {
  onLogin: (employee: Employee) => void
}

function LoginPage({ onLogin }: LoginPageProps) {
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
      const response = await fetch('/api/auth/login', {
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
      <section className="login-card" aria-labelledby="login-heading">
        <p className="brand">CHAPEAU</p>
        <h1 id="login-heading">Staff sign in</h1>
        <p>Enter your employee PIN to continue.</p>
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
          <button className="primary-action" type="submit" disabled={isSubmitting}>
            {isSubmitting ? 'Signing in...' : 'Sign in'}
          </button>
        </form>
      </section>
    </main>
  )
}

export default LoginPage
