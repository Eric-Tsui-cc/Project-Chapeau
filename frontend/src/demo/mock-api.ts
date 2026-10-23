// Demo API used only by the preview copy (set VITE_DEMO=1 to enable).
// It patches window.fetch and serves in-memory mock data for every endpoint
// the UI talks to, so the pages can be reviewed without the .NET backend.

import type { Employee } from '../types/Employee'
import type { Table } from '../types/Table'
import type { OrderDetails } from '../types/Order'

type MenuItem = {
  id: number
  name: string
  card: string
  category: string
  price: number
  stock: number
}

type QueueItem = {
  orderId: number
  orderItemId: number
  tableNumber: number
  menuItemName: string
  count: number
  comment: string
  orderTime: string
  status: 'Running' | 'Preparing'
}

type Bill = {
  id: number
  orderId: number
  tableNumber: number
  amount: number
  tip: number
  paymentMethod: 'Cash' | 'Debit' | 'Credit'
}

type MockOrder = {
  id: number
  table: number
  isPaid: boolean
  items: OrderDetails['items']
}

const employees: Employee[] = [
  { id: 1, firstName: 'Alice', lastName: 'Johnson', role: 'Waiter' },
  { id: 2, firstName: 'Bob', lastName: 'Smith', role: 'Waiter' },
  { id: 3, firstName: 'Charlie', lastName: 'Brown', role: 'Chef' },
  { id: 4, firstName: 'Diana', lastName: 'Ross', role: 'Bartender' },
]

const menuItems: MenuItem[] = [
  { id: 1, name: 'Tomato Soup', card: 'Lunch', category: 'Starters', price: 6.5, stock: 24 },
  { id: 2, name: 'Goat Cheese Salad', card: 'Lunch', category: 'Starters', price: 8, stock: 18 },
  { id: 3, name: 'Beef Croquettes', card: 'Lunch', category: 'Starters', price: 7.5, stock: 12 },
  { id: 4, name: 'Club Sandwich', card: 'Lunch', category: 'Mains', price: 12.5, stock: 30 },
  { id: 5, name: 'Croque Monsieur', card: 'Lunch', category: 'Mains', price: 11, stock: 22 },
  { id: 6, name: 'Salmon Fillet', card: 'Lunch', category: 'Mains', price: 16, stock: 9 },
  { id: 7, name: 'French Fries', card: 'Lunch', category: 'Entremet', price: 4, stock: 40 },
  { id: 8, name: 'Green Beans', card: 'Lunch', category: 'Entremet', price: 3.5, stock: 15 },
  { id: 9, name: 'Crème Brûlée', card: 'Lunch', category: 'Desserts', price: 6, stock: 20 },
  { id: 10, name: 'Chocolate Mousse', card: 'Lunch', category: 'Desserts', price: 5.5, stock: 3 },
  { id: 11, name: 'Mushroom Risotto', card: 'Dinner', category: 'Mains', price: 17.5, stock: 11 },
  { id: 12, name: 'Sirloin Steak', card: 'Dinner', category: 'Mains', price: 24, stock: 7 },
  { id: 13, name: 'Duck Confit', card: 'Dinner', category: 'Mains', price: 21, stock: 6 },
  { id: 14, name: 'Onion Tart', card: 'Dinner', category: 'Starters', price: 7, stock: 14 },
  { id: 15, name: 'Roasted Potatoes', card: 'Dinner', category: 'Entremet', price: 4.5, stock: 25 },
  { id: 16, name: 'Tarte Tatin', card: 'Dinner', category: 'Desserts', price: 7.5, stock: 10 },
  { id: 17, name: 'Panna Cotta', card: 'Dinner', category: 'Desserts', price: 6.5, stock: 0 },
  { id: 18, name: 'Pilsner 33cl', card: 'Drinks', category: 'Beers', price: 3.4, stock: 60 },
  { id: 19, name: 'Tripel 25cl', card: 'Drinks', category: 'Beers', price: 4.8, stock: 35 },
  { id: 20, name: 'House Red 25cl', card: 'Drinks', category: 'Wines', price: 5.2, stock: 28 },
  { id: 21, name: 'House White 25cl', card: 'Drinks', category: 'Wines', price: 5.2, stock: 26 },
  { id: 22, name: 'Whisky 4cl', card: 'Drinks', category: 'Spirit', price: 7.5, stock: 18 },
  { id: 23, name: 'Espresso', card: 'Drinks', category: 'CoffeeTea', price: 2.8, stock: 50 },
  { id: 24, name: 'Fresh Mint Tea', card: 'Drinks', category: 'CoffeeTea', price: 3.2, stock: 30 },
  { id: 25, name: 'Espresso Martini', card: 'Drinks', category: 'CoffeeTea', price: 8.5, stock: 12 },
]

const tables: Table[] = [
  { id: 1, number: 1, capacity: 2, status: 'Occupied' },
  { id: 2, number: 2, capacity: 2, status: 'Free' },
  { id: 3, number: 3, capacity: 4, status: 'Occupied' },
  { id: 4, number: 4, capacity: 4, status: 'Free' },
  { id: 5, number: 5, capacity: 4, status: 'Occupied' },
  { id: 6, number: 6, capacity: 6, status: 'Free' },
  { id: 7, number: 7, capacity: 6, status: 'Free' },
  { id: 8, number: 8, capacity: 6, status: 'Occupied' },
  { id: 9, number: 9, capacity: 8, status: 'Free' },
  { id: 10, number: 10, capacity: 8, status: 'Free' },
]

const minutesAgo = (minutes: number) => new Date(Date.now() - minutes * 60000).toISOString()

let nextOrderId = 101
let nextItemId = 1001
let nextBillId = 501
const orders: MockOrder[] = [
  {
    id: 100,
    table: 1,
    isPaid: false,
    items: [
      { id: 901, menuItemName: 'Tomato Soup', count: 2, unitPrice: 6.5, status: 'Served', comment: '' },
      { id: 902, menuItemName: 'Club Sandwich', count: 1, unitPrice: 12.5, status: 'Prepared', comment: 'No pickles' },
      { id: 903, menuItemName: 'Crème Brûlée', count: 2, unitPrice: 6, status: 'Running', comment: '' },
      { id: 904, menuItemName: 'Espresso', count: 2, unitPrice: 2.8, status: 'Prepared', comment: '' },
    ],
  },
  {
    id: 99,
    table: 3,
    isPaid: false,
    items: [
      { id: 895, menuItemName: 'Sirloin Steak', count: 3, unitPrice: 24, status: 'Running', comment: 'Medium rare' },
      { id: 896, menuItemName: 'House Red 25cl', count: 3, unitPrice: 5.2, status: 'Served', comment: '' },
    ],
  },
  {
    id: 98,
    table: 5,
    isPaid: false,
    items: [
      { id: 890, menuItemName: 'Pilsner 33cl', count: 4, unitPrice: 3.4, status: 'Prepared', comment: '' },
      { id: 891, menuItemName: 'Goat Cheese Salad', count: 4, unitPrice: 8, status: 'Running', comment: '' },
    ],
  },
  {
    id: 97,
    table: 8,
    isPaid: false,
    items: [
      { id: 885, menuItemName: 'Salmon Fillet', count: 2, unitPrice: 16, status: 'Preparing', comment: '' },
      { id: 886, menuItemName: 'French Fries', count: 2, unitPrice: 4, status: 'Served', comment: '' },
    ],
  },
]

const kitchenQueue: QueueItem[] = [
  { orderId: 100, orderItemId: 903, tableNumber: 1, menuItemName: 'Crème Brûlée', count: 2, comment: '', orderTime: minutesAgo(9), status: 'Running' },
  { orderId: 99, orderItemId: 895, tableNumber: 3, menuItemName: 'Sirloin Steak', count: 3, comment: 'Medium rare', orderTime: minutesAgo(16), status: 'Running' },
  { orderId: 98, orderItemId: 891, tableNumber: 5, menuItemName: 'Goat Cheese Salad', count: 4, comment: 'Dressing on the side', orderTime: minutesAgo(4), status: 'Running' },
  { orderId: 97, orderItemId: 885, tableNumber: 8, menuItemName: 'Salmon Fillet', count: 2, comment: '', orderTime: minutesAgo(7), status: 'Preparing' },
]

const barQueue: QueueItem[] = [
  { orderId: 100, orderItemId: 904, tableNumber: 1, menuItemName: 'Espresso', count: 2, comment: '', orderTime: minutesAgo(6), status: 'Running' },
  { orderId: 97, orderItemId: 887, tableNumber: 8, menuItemName: 'Fresh Mint Tea', count: 1, comment: 'Extra honey', orderTime: minutesAgo(3), status: 'Running' },
]

export function installDemoApi() {
  const originalFetch = window.fetch
  let currentEmployee: Employee | null = null
  // Only demo identities use sessionStorage. Real sign-in still uses HttpOnly cookies.
  const demoSessionKey = 'chapeau-demo-employee'
  try {
    const employeeId = Number(sessionStorage.getItem(demoSessionKey))
    currentEmployee = employees.find((employee) => employee.id === employeeId) ?? null
  } catch { /* Demo sign-in can still work when browser storage is unavailable. */ }

  const respond = (status: number, body: unknown) =>
    new Response(body === undefined ? null : JSON.stringify(body), {
      status,
      headers: { 'Content-Type': 'application/json' },
    })

  const readJson = async <T,>(request: Request): Promise<T> => {
    try {
      return JSON.parse(await request.text()) as T
    } catch {
      return {} as T
    }
  }

  async function handle(url: URL, method: string, request: Request): Promise<Response> {
    const path = url.pathname
    const delay = Math.random() * 250 + 100
    await new Promise((resolve) => setTimeout(resolve, delay))

    // Demo-only placeholder; real tokens are issued by ASP.NET Core.
    if (path === '/api/auth/csrf' && method === 'GET') {
      return respond(200, { requestToken: 'demo-csrf-token' })
    }

    // Keep scripted replies confined to the explicitly enabled demo mode.
    if (path === '/api/assistant/chat' && method === 'POST') {
      const body = await readJson<{ message?: string }>(request)
      if (typeof body.message !== 'string' || !body.message.trim() || body.message.length > 500) return respond(400, 'Invalid question.')
      if (/allerg|gluten|vegan|vegetarian|nuts|dairy/i.test(body.message)) {
        return respond(200, { reply: 'Demo reply: please ask your server to confirm ingredients and dietary suitability.', menuItemIds: [] })
      }
      const available = menuItems.filter((item) => item.stock > 0)
      const drinks = ['Beers', 'Wines', 'Spirit', 'CoffeeTea']
      const picks = /dessert|sweet/i.test(body.message)
        ? available.filter((item) => item.category === 'Desserts').slice(0, 2)
        : /main|meal|dinner|lunch/i.test(body.message)
          ? [available.find((item) => item.category === 'Mains'), available.find((item) => drinks.includes(item.category))].filter((item): item is MenuItem => item !== undefined)
          : /drink|coffee|tea|wine|beer/i.test(body.message)
            ? available.filter((item) => drinks.includes(item.category)).slice(0, 2)
            : available.slice(0, 2)
      return respond(200, { reply: 'Demo reply: here are some sample menu picks. These are scripted suggestions; no AI service was called.', menuItemIds: picks.map((item) => item.id) })
    }

    // --- auth ---
    if (path === '/api/auth/login' && method === 'POST') {
      const body = await readJson<{ pin: number }>(request)
      // 1234 = waiter (Alice), 3456 = chef (Charlie), 4567 = bartender (Diana);
      // every other valid PIN falls back to the waiter so the main flow is always visible.
      const roleByPin: Record<number, number> = { 1234: 1, 3456: 3, 4567: 4 }
      const employee = employees.find((candidate) => candidate.id === (roleByPin[body.pin] ?? 1)) ?? null
      if (!employee) return respond(404, { message: 'Unknown PIN' })
      currentEmployee = employee
      try { sessionStorage.setItem(demoSessionKey, String(employee.id)) } catch { /* Storage is optional in demo mode. */ }
      return respond(200, employee)
    }

    if (path === '/api/auth/me' && method === 'GET') {
      return currentEmployee ? respond(200, currentEmployee) : respond(401, { message: 'Not signed in' })
    }
    if (path === '/api/auth/logout' && method === 'POST') {
      currentEmployee = null
      try { sessionStorage.removeItem(demoSessionKey) } catch { /* Storage is optional in demo mode. */ }
      return respond(204, undefined)
    }

    // --- tables ---
    if (path === '/api/tables' && method === 'GET') return respond(200, tables)

    if (path === '/api/menuitems/customer' && method === 'GET') {
      return respond(200, menuItems.map(({ id, name, card, category, price, stock }) => ({
        id, name, card, category, price, isAvailable: stock > 0,
      })))
    }

    if (path === '/api/customer/orders/items' && method === 'POST') {
      const body = await readJson<{ tableNumber: number; items: { menuItemId: number; count: number; comment?: string }[] }>(request)
      if (!Number.isInteger(body.tableNumber) || body.tableNumber <= 0 || !Array.isArray(body.items) || body.items.length === 0 || body.items.some((item) => !item || !Number.isInteger(item.menuItemId) || item.menuItemId <= 0 || !Number.isInteger(item.count) || item.count <= 0)) return respond(400, 'Invalid table or items.')
      const table = tables.find((candidate) => candidate.number === body.tableNumber)
      if (!table) return respond(404, 'Table not found')
      const order = orders.find((candidate) => candidate.table === table.id && !candidate.isPaid)
      if (!order) return respond(409, 'Please ask your server to open the table.')
      if (order.items.length > 0) return respond(409, 'An order has already been placed. Please ask your server to add items.')
      const counts = new Map<number, number>()
      for (const item of body.items) counts.set(item.menuItemId, (counts.get(item.menuItemId) ?? 0) + item.count)
      for (const [id, count] of counts) {
        const menuItem = menuItems.find((candidate) => candidate.id === id)
        if (!menuItem) return respond(404, 'One or more menu items were not found.')
        if (menuItem.stock < count) return respond(409, 'Not enough stock available.')
      }
      const orderTime = new Date().toISOString()
      for (const line of body.items) {
        const menuItem = menuItems.find((candidate) => candidate.id === line.menuItemId)!
        const item: OrderDetails['items'][number] = {
          id: nextItemId++, menuItemName: menuItem.name, count: line.count,
          unitPrice: menuItem.price, status: 'Running', comment: line.comment?.trim() ?? '',
        }
        order.items.push(item)
        menuItem.stock -= line.count
        const queue = ['Beers', 'Wines', 'Spirit', 'CoffeeTea'].includes(menuItem.category) ? barQueue : kitchenQueue
        queue.push({ orderId: order.id, orderItemId: item.id, tableNumber: table.number,
          menuItemName: item.menuItemName, count: item.count, comment: item.comment, orderTime, status: 'Running' })
      }
      return respond(200, { orderId: order.id, tableNumber: table.number, message: 'Your order has been submitted.' })
    }

    // --- menu ---
    if (path === '/api/menuitems' && method === 'GET') return respond(200, menuItems)

    // --- orders ---
    if (path === '/api/orders/current' && method === 'GET') {
      const tableId = Number(url.searchParams.get('tableId') ?? -1)
      const order = orders.find((candidate) => candidate.table === tableId && !candidate.isPaid)
      return order ? respond(200, { id: order.id }) : respond(404, { message: 'No open order' })
    }

    if (path === '/api/orders' && method === 'POST') {
      const body = await readJson<{ tableId: number; employeeId: number }>(request)
      orders.push({ id: nextOrderId++, table: body.tableId, isPaid: false, items: [] })
      return respond(201, { id: nextOrderId - 1 })
    }

    const orderItemMatch = path.match(/^\/api\/orders\/(\d+)\/items$/)
    if (orderItemMatch && method === 'POST') {
      const body = await readJson<{ menuItemId: number; count: number; comment: string }>(request)
      const menuItem = menuItems.find((candidate) => candidate.id === body.menuItemId)
      const order = orders.find((candidate) => candidate.id === Number(orderItemMatch[1]))
      if (!menuItem || !order || order.isPaid) return respond(404, { message: 'Not found' })
      const item: OrderDetails['items'][number] = {
        id: nextItemId++,
        menuItemName: menuItem.name,
        count: body.count,
        unitPrice: menuItem.price,
        status: 'Running',
        comment: body.comment ?? '',
      }
      order.items.push(item)
      kitchenQueue.push({
        orderId: order.id, orderItemId: item.id, tableNumber: order.table,
        menuItemName: menuItem.name, count: item.count, comment: item.comment,
        orderTime: new Date().toISOString(), status: 'Running',
      })
      if (menuItem.category === 'Beers' || menuItem.category === 'Wines' || menuItem.category === 'Spirit' || menuItem.category === 'CoffeeTea') {
        barQueue.push(kitchenQueue.pop()!)
      }
      return respond(201, item)
    }

    const orderDetailsMatch = path.match(/^\/api\/orders\/(\d+)$/)
    if (orderDetailsMatch && method === 'GET') {
      const order = orders.find((candidate) => candidate.id === Number(orderDetailsMatch[1]))
      return order
        ? respond(200, { id: order.id, isPaid: order.isPaid, items: order.items })
        : respond(404, { message: 'Not found' })
    }

    const serveMatch = path.match(/^\/api\/orders\/(\d+)\/items\/(\d+)\/serve$/)
    if (serveMatch && method === 'PATCH') {
      const item = orders.find((candidate) => candidate.id === Number(serveMatch[1]))?.items
        .find((candidate) => candidate.id === Number(serveMatch[2]))
      if (!item || item.status !== 'Prepared') return respond(409, { message: 'Not servable' })
      item.status = 'Served'
      return respond(200, item)
    }

    // --- kitchen / bar queues ---
    if (path === '/api/kitchen/items' && method === 'GET') return respond(200, kitchenQueue)
    if (path === '/api/bar/items' && method === 'GET') return respond(200, barQueue)

    const kitchenMatch = path.match(/^\/api\/kitchen\/items\/(\d+)\/status$/)
    if (kitchenMatch && method === 'PATCH') {
      const body = await readJson<{ status: 'Preparing' | 'Prepared' }>(request)
      const index = kitchenQueue.findIndex((candidate) => candidate.orderItemId === Number(kitchenMatch[1]))
      if (index === -1) return respond(404, { message: 'Not found' })
      const item = kitchenQueue[index]
      if (body.status === 'Preparing') {
        item.status = 'Preparing'
        return respond(200, item)
      }
      kitchenQueue.splice(index, 1)
      const order = orders.find((candidate) => candidate.id === item.orderId)
      const orderItem = order?.items.find((candidate) => candidate.id === item.orderItemId)
      if (orderItem) orderItem.status = 'Prepared'
      return respond(200, item)
    }

    const barMatch = path.match(/^\/api\/bar\/items\/(\d+)\/prepare$/)
    if (barMatch && method === 'PATCH') {
      const index = barQueue.findIndex((candidate) => candidate.orderItemId === Number(barMatch[1]))
      if (index === -1) return respond(404, { message: 'Not found' })
      const item = barQueue.splice(index, 1)[0]
      const order = orders.find((candidate) => candidate.id === item.orderId)
      const orderItem = order?.items.find((candidate) => candidate.id === item.orderItemId)
      if (orderItem) orderItem.status = 'Prepared'
      return respond(200, item)
    }

    // --- bills ---
    if (path === '/api/bill' && method === 'POST') {
      const body = await readJson<{ orderId: number; paymentMethod: Bill['paymentMethod']; tip: number }>(request)
      const order = orders.find((candidate) => candidate.id === body.orderId)
      if (!order || order.isPaid || order.items.some((candidate) => candidate.status !== 'Served')) {
        return respond(409, { message: 'Not eligible' })
      }
      order.isPaid = true
      const table = tables.find((candidate) => candidate.number === order.table)
      if (table) table.status = 'Free'
      const amount = order.items.reduce((sum, item) => sum + item.unitPrice * item.count, 0)
      const bill: Bill = {
        id: nextBillId++, orderId: order.id, tableNumber: order.table,
        amount, tip: body.tip, paymentMethod: body.paymentMethod,
      }
      return respond(201, bill)
    }

    return respond(404, { message: `Demo API has no handler for ${method} ${path}` })
  }

  window.fetch = async (input, init) => {
    try {
      const request = input instanceof Request ? input : new Request(input as string, init)
      const url = new URL(request.url, window.location.href)
      if (url.pathname.startsWith('/api/')) {
        return await handle(url, request.method, request)
      }
    } catch {
      // fall through to the real fetch
    }
    return originalFetch(input as RequestInfo, init as RequestInit)
  }

  console.info('[preview] Demo API enabled — data is mocked in memory. PIN 1234 signs in as a waiter.')
}
