import type { CustomerMenuItem } from '../lib/customer-cart'
import { getMenuDetails } from '../lib/menu-details'

export function CustomerAllergens({ allergens }: { allergens: string[] }) {
  return (
    <div className="customer-allergens">
      <span className="customer-allergens-label">Allergens</span>
      {allergens.length > 0 ? (
        <ul aria-label="Listed allergens">
          {allergens.map((allergen) => <li key={allergen}>{allergen}</li>)}
        </ul>
      ) : <span className="customer-allergens-unknown">Please confirm with your server.</span>}
    </div>
  )
}

export function CustomerMenuDetails({ item }: { item: CustomerMenuItem }) {
  const details = getMenuDetails(item)
  if (!details) return <p className="customer-detail-note">Ingredient and allergen information is not available for this item. Please ask your server.</p>
  return (
    <section className="customer-item-details" aria-label="Ingredients and allergens">
      <div><h4>Ingredients</h4><p>{details.ingredients}</p></div>
      <CustomerAllergens allergens={details.allergens} />
      {details.allergenNotes && <p className="customer-detail-note">{details.allergenNotes}</p>}
      <p className="customer-detail-notice"><strong>Demo recipe.</strong> Please confirm ingredients, allergies and cross-contact with your server before ordering.</p>
    </section>
  )
}
