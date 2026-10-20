export const MAX_QUANTITY = 99

export type CustomerMenuItem = {
  id: number
  name: string
  card: string
  category: string
  price: number
  isAvailable: boolean
}

export type CartLine = {
  key: string
  menuItemId: number
  name: string
  unitPriceCents: number
  quantity: number
  comment: string
}

export function validQuantity(value: number) {
  return Number.isInteger(value) && value >= 1 && value <= MAX_QUANTITY
}

export function saveCartLine(
  lines: CartLine[], item: CustomerMenuItem, quantity: number,
  note: string, editingKey?: string,
): CartLine[] {
  if (!item.isAvailable) throw new Error('This item is currently sold out.')
  if (!validQuantity(quantity)) throw new Error(`Choose a whole quantity from 1 to ${MAX_QUANTITY}.`)
  if (!Number.isFinite(item.price) || item.price < 0) throw new Error('The price is unavailable. Please ask your server.')
  if (editingKey !== undefined && !lines.some((line) => line.key === editingKey)) {
    throw new Error('This item is no longer in your basket.')
  }
  const comment = note.trim()
  const unitPriceCents = Math.round(item.price * 100)
  const key = JSON.stringify([item.id, unitPriceCents, comment])
  const remaining = lines.filter((line) => line.key !== editingKey)
  const match = remaining.find((line) => line.key === key)
  const combined = quantity + (match?.quantity ?? 0)
  if (!validQuantity(combined)) throw new Error(`You already have this item with these notes. The combined quantity cannot exceed ${MAX_QUANTITY}.`)
  if (match) return remaining.map((line) => line.key === key ? { ...line, quantity: combined } : line)
  const nextLine = { key, menuItemId: item.id, name: item.name, unitPriceCents, quantity, comment }
  const next = [...remaining]
  const index = editingKey === undefined ? next.length : lines.findIndex((line) => line.key === editingKey)
  next.splice(Math.min(index, next.length), 0, nextLine)
  return next
}

export function setCartQuantity(lines: CartLine[], key: string, quantity: number) {
  if (!validQuantity(quantity)) return lines
  return lines.map((line) => line.key === key ? { ...line, quantity } : line)
}

export function cartTotalCents(lines: CartLine[]) {
  return lines.reduce((total, line) => total + line.unitPriceCents * line.quantity, 0)
}

export function formatMoney(cents: number) {
  return `$${(cents / 100).toFixed(2)}`
}
