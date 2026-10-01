import { useState } from 'react'
import LoginPage from './pages/LoginPage'
import TablesPage from './pages/TablesPage'
import type { Employee } from './types/Employee'
import './App.css'

function App() {
  // This is UI state only; backend authentication is not implemented yet.
  const [employee, setEmployee] = useState<Employee | null>(null)

  if (employee === null) {
    return <LoginPage onLogin={setEmployee} />
  }

  return <TablesPage employee={employee} onLogout={() => setEmployee(null)} />
}

export default App
