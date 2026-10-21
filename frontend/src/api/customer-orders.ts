import { csrfFetch } from './client'
import { validQuantity } from '../lib/customer-cart'
import type { CartLine } from '../lib/customer-cart'

export type CustomerConfirmation = { orderId: number; tableNumber: number; message: string }
export type CustomerSubmitResult =
  | { kind: 'submitted'; confirmation: CustomerConfirmation }
  | { kind: 'rejected' | 'uncertain'; message: string }

export async function submitCustomerOrder(tableNumber: number, lines: CartLine[]): Promise<CustomerSubmitResult> {
  if (!Number.isInteger(tableNumber) || tableNumber < 1 || tableNumber > 2147483647) {
    return { kind: 'rejected', message: 'Enter the table number shown at your table.' }
  }
  if (lines.length === 0 || lines.some((line) => !validQuantity(line.quantity))) {
    return { kind: 'rejected', message: 'Your basket needs at least one item with a valid quantity.' }
  }
  const uncertain: CustomerSubmitResult = {
    kind: 'uncertain',
    message: 'We could not confirm whether your order was received. Please ask your server to check before ordering again. Your basket has been kept.',
  }
  try {
    const response = await csrfFetch('/api/customer/orders/items', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        tableNumber,
        items: lines.map((line) => ({ menuItemId: line.menuItemId, count: line.quantity, comment: line.comment })),
      }),
      signal: AbortSignal.timeout(15000),
    })
    if (response.ok) {
      const result: Partial<CustomerConfirmation> = await response.json()
      if (!Number.isInteger(result.orderId) || Number(result.orderId) <= 0 || result.tableNumber !== tableNumber) return uncertain
      return { kind: 'submitted', confirmation: { orderId: result.orderId!, tableNumber, message: 'Your order has been submitted.' } }
    }
    if (response.status === 409) {
      const reason = (await response.text()).toLowerCase()
      return { kind: 'rejected', message: reason.includes('stock')
        ? 'Some items have insufficient stock. Reduce the quantities or choose other items, then try again.'
        : reason.includes('already')
          ? 'This table already has an order. Please ask your server to add items.'
          : 'This table cannot accept the order yet. Check the table number and ask your server to open the table first.' }
    }
    if (response.status === 404) return { kind: 'rejected', message: 'The table or a selected item could not be found. Check the table number or ask your server.' }
    if (response.status === 400) return { kind: 'rejected', message: 'The request was not accepted. Check the table number and quantities. If it persists, refresh the menu and try again.' }
    if (response.status === 401 || response.status === 403) return { kind: 'rejected', message: 'The request could not be verified. Refresh the page or ask your server for help.' }
    return uncertain
  } catch {
    return uncertain
  }
}
