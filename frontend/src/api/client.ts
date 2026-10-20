export const SESSION_EXPIRED_EVENT = 'chapeau:session-expired'
const safeMethods = new Set(['GET', 'HEAD', 'OPTIONS', 'TRACE'])

// Fetch a fresh token for each mutation: tokens issued before login must not
// be reused after the authenticated identity changes. Nothing is persisted.
export async function csrfFetch(url: string, options: RequestInit = {}): Promise<Response> {
  const method = (options.method ?? 'GET').toUpperCase()
  const headers = new Headers(options.headers)

  if (!safeMethods.has(method)) {
    const tokenResponse = await fetch('/api/auth/csrf', {
      credentials: 'same-origin',
      cache: 'no-store',
      signal: options.signal ?? AbortSignal.timeout(10000),
    })
    if (!tokenResponse.ok) throw new Error('Unable to get request verification token')
    const data: { requestToken?: unknown } = await tokenResponse.json()
    if (typeof data.requestToken !== 'string' || !data.requestToken.trim()) {
      throw new Error('Invalid request verification token')
    }
    headers.set('X-CSRF-TOKEN', data.requestToken)
  }

  // Do not retry mutations automatically: the server may already have saved them.
  return fetch(url, { ...options, method, headers, credentials: 'same-origin' })
}

// Login uses csrfFetch directly: its 401 means invalid credentials, not expiry.
export async function apiFetch(url: string, options?: RequestInit): Promise<Response> {
  const response = await csrfFetch(url, options)
  if (response.status === 401 && !options?.signal?.aborted) {
    window.dispatchEvent(new Event(SESSION_EXPIRED_EVENT))
  }
  return response
}