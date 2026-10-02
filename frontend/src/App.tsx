import { useState } from 'react'
import LoginPage from './pages/LoginPage'
import TablesPage from './pages/TablesPage'
import OrderPage from './pages/OrderPage'
import PreparationPage from './pages/PreparationPage'
import type { Table } from './types/Table'
import type { Employee } from './types/Employee'
import './App.css'

function App() {
  // This is UI state only; backend authentication is not implemented yet.
  const [employee, setEmployee] = useState<Employee | null>(null)
  const [selectedTable, setSelectedTable] = useState<Table | null>(null)

  function logout() {
    setSelectedTable(null)
    setEmployee(null)
  }

  if (employee === null) {
    return <LoginPage onLogin={setEmployee} />
  }

  if (selectedTable !== null) {
    return <OrderPage key={selectedTable.id} table={selectedTable} employee={employee} onBack={() => setSelectedTable(null)} />
  }

  if (employee.role === 'Chef' || employee.role === 'Bartender') {
    return <PreparationPage key={employee.role} station={employee.role === 'Chef' ? 'kitchen' : 'bar'} employee={employee} onLogout={logout} />
  }

  return <TablesPage employee={employee} onLogout={logout} onSelectTable={setSelectedTable} />
}

export default App
