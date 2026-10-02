export type OrderDetails = {
  id: number
  isPaid: boolean
  items: {
    id: number
    menuItemName: string
    count: number
    unitPrice: number
    status: 'Running' | 'Preparing' | 'Prepared' | 'Served'
    comment: string
  }[]
}
