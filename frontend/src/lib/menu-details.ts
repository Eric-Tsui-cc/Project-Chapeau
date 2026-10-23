import menuDetails from '../../../data/menu-details.json'
import type { CustomerMenuItem } from './customer-cart'

const detailsById = new Map(menuDetails.items.map((item) => [item.menuItemId, item]))

export function getMenuDetails(item: Pick<CustomerMenuItem, 'id' | 'name'>) {
  const details = detailsById.get(item.id)
  if (details?.name === item.name) return details
  // The older demo menu has different IDs; only use an exact name match there.
  if (import.meta.env.VITE_DEMO === '1') {
    return menuDetails.items.find((candidate) => candidate.name === item.name)
  }
  return undefined
}
