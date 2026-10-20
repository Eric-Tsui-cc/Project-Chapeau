import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { installDemoApi } from './demo/mock-api'

// Preview copy only: serve mocked API responses so the UI can be reviewed
// without the backend. Install before the first render so no request can
// slip through to the (absent) proxy. Disable by removing/emptying VITE_DEMO.
if (import.meta.env.VITE_DEMO === '1') {
  installDemoApi()
}

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <App />
  </StrictMode>,
)
