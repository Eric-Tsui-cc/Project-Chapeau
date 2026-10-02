import { useEffect, useRef, useState } from 'react'
import type { SubmitEvent } from 'react'

type MenuItem = {
  id: number
  name: string
  card: string
  category: string
  price: number
  stock: number
}

const categoryNames: Record<string, string> = {
  Mains: 'Mains', Starters: 'Starters', Entremet: 'Sides', Desserts: 'Desserts',
  Beers: 'Beers', Wines: 'Wines', Spirit: 'Spirits', CoffeeTea: 'Coffee, tea & cocktails',
}

type AddItemPageProps = {
  orderId: number
  tableNumber: number
  onBack: () => void
  onAdded: () => void
}

function AddItemPage({ orderId, tableNumber, onBack, onAdded }: AddItemPageProps) {
  const [items, setItems] = useState<MenuItem[]>([])
  const [loading, setLoading] = useState(true)
  const [loadError, setLoadError] = useState<string | null>(null)
  const [reloadCount, setReloadCount] = useState(0)
  const [card, setCard] = useState<string | null>(null)
  const [category, setCategory] = useState<string | null>(null)
  const [selectedId, setSelectedId] = useState<number | null>(null)
  const [quantity, setQuantity] = useState('1')
  const [comment, setComment] = useState('')
  const [error, setError] = useState<string | null>(null)
  const [submitting, setSubmitting] = useState(false)
  const [mustCheckOrder, setMustCheckOrder] = useState(false)
  const inFlight = useRef(false)
  const selected = items.find((item) => item.id === selectedId)
  const categories = [...new Set(items.map((item) => item.category))]
  const visibleItems = items.filter((item) =>
    (card === null || item.card === card) &&
    (category === null || item.category === category))

  useEffect(() => {
    const controller = new AbortController()
    let active = true
    const timer = setTimeout(() => controller.abort(), 10000)
    async function loadMenu() {
      try {
        const response = await fetch('/api/menuitems', { signal: controller.signal })
        if (!response.ok) throw new Error('Unable to load the menu. Please retry.')
        const data: MenuItem[] = await response.json()
        if (active) setItems(data)
      } catch {
        if (active) setLoadError('Unable to load the menu. Check your connection and retry.')
      } finally {
        clearTimeout(timer)
        if (active) setLoading(false)
      }
    }
    void loadMenu()
    return () => { active = false; clearTimeout(timer); controller.abort() }
  }, [reloadCount])

  async function addItem(event: SubmitEvent<HTMLFormElement>) {
    event.preventDefault()
    if (inFlight.current || mustCheckOrder || !selected) return
    const count = Number(quantity)
    if (!Number.isInteger(count) || count < 1 || count > selected.stock) {
      setError('Enter a whole quantity between 1 and the available stock.')
      return
    }
    inFlight.current = true
    setSubmitting(true)
    setError(null)
    try {
      const response = await fetch(`/api/orders/${orderId}/items`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ menuItemId: selected.id, count, comment: comment.trim() }),
        signal: AbortSignal.timeout(10000),
      })
      if (response.ok) { onAdded(); return }
      if (response.status === 400) {
        setError('The item could not be added. Check the quantity and try again.')
        return
      }
      setMustCheckOrder(true)
      setError(response.status === 409
        ? 'Stock may have changed, or this order has been paid. Return to the order and reopen the menu before trying again.'
        : response.status === 404
          ? 'The order or menu item is no longer available. Return to the order to refresh.'
          : 'The result could not be confirmed. Return to the order and check its items before trying again.')
    } catch {
      // A lost response does not mean the server failed to save the item.
      setMustCheckOrder(true)
      setError('The result could not be confirmed. Return to the order and check its items before trying again.')
    } finally {
      inFlight.current = false
      setSubmitting(false)
    }
  }

  return (
    <main className="tables-page">
      <div className="employee-bar">
        <button type="button" className="secondary-button" onClick={onBack} disabled={submitting}>Back to order</button>
        <p>Table {tableNumber} · Order #{orderId}</p>
      </div>
      <header className="page-header"><div><p className="brand">CHAPEAU</p><h1>Add item</h1><p>Select an item, then enter the quantity and any notes.</p></div></header>
      {loading && <p role="status">Loading menu...</p>}
      {loadError && <div className="order-panel"><p role="alert" className="error">{loadError}</p><button type="button" onClick={() => { setLoadError(null); setLoading(true); setReloadCount((value) => value + 1) }}>Retry</button></div>}
      {!loading && !loadError && (
        <div className="menu-layout">
          <section aria-label="Menu">
            <div className="menu-filters">
              <fieldset disabled={submitting || mustCheckOrder}>
                <legend>Menu</legend>
                <div className="filter-buttons">
                  {[...new Set(items.map((item) => item.card))].map((name) => (
                    <button key={name} type="button" className={`filter-button${card === name ? ' is-active' : ''}`} aria-pressed={card === name} onClick={() => { setCard((current) => current === name ? null : name); setSelectedId(null); setError(null) }}>{name}</button>
                  ))}
                </div>
              </fieldset>
              <fieldset disabled={submitting || mustCheckOrder}>
                <legend>Category</legend>
                <div className="filter-buttons">
                  {categories.map((name) => (
                    <button key={name} type="button" className={`filter-button${category === name ? ' is-active' : ''}`} aria-pressed={category === name} onClick={() => { setCategory((current) => current === name ? null : name); setSelectedId(null); setError(null) }}>{categoryNames[name] ?? name}</button>
                  ))}
                </div>
              </fieldset>
            </div>
            {visibleItems.length === 0 && <p>No items match these filters. Tap a selected filter to clear it.</p>}
            <ul className="menu-list">
              {visibleItems.map((item) => (
                <li key={item.id}>
                  <button type="button" className={`menu-choice${selectedId === item.id ? ' is-selected' : ''}`} aria-pressed={selectedId === item.id} disabled={submitting || mustCheckOrder || item.stock < 1} onClick={() => { setSelectedId((current) => current === item.id ? null : item.id); setQuantity('1'); setComment(''); setError(null) }}>
                    <span className="menu-name">{item.name}</span>
                    <span className="menu-meta">{item.card} · {categoryNames[item.category] ?? item.category}</span>
                    <span className="menu-price">${item.price.toFixed(2)}</span>
                    <span className="menu-meta">{item.stock > 0 ? `${item.stock} available` : 'Out of stock'}</span>
                  </button>
                </li>
              ))}
            </ul>
          </section>
          <section className="order-panel item-editor" aria-labelledby="item-heading">
            <h2 id="item-heading">{selected?.name ?? 'Choose an item'}</h2>
            {!selected && <p>Select an item from the menu to add it to this order.</p>}
            {selected && (
              <form onSubmit={addItem}>
                <p>${selected.price.toFixed(2)} each · {selected.stock} available</p>
                <label htmlFor="item-quantity">Quantity</label>
                <div className="quantity-stepper">
                  <button type="button" className="secondary-button" aria-label="Decrease quantity" disabled={submitting || mustCheckOrder || Number(quantity) <= 1} onClick={() => setQuantity(String(Math.max(1, Math.min(selected.stock, Math.trunc(Number(quantity) || 1) - 1))))}>−</button>
                  <input id="item-quantity" type="number" inputMode="numeric" min={1} max={selected.stock} step={1} required value={quantity} onChange={(event) => setQuantity(event.target.value)} disabled={submitting || mustCheckOrder} />
                  <button type="button" className="secondary-button" aria-label="Increase quantity" disabled={submitting || mustCheckOrder || Number(quantity) >= selected.stock} onClick={() => setQuantity(String(Math.min(selected.stock, Math.max(1, Math.trunc(Number(quantity) || 0) + 1))))}>+</button>
                </div>
                <label htmlFor="item-note">Notes (optional)</label>
                <textarea id="item-note" rows={3} placeholder="e.g. No onion" value={comment} onChange={(event) => setComment(event.target.value)} disabled={submitting || mustCheckOrder} />
                <button className="primary-action" type="submit" disabled={submitting || mustCheckOrder}>{submitting ? 'Adding...' : 'Add to order'}</button>
              </form>
            )}
            {error && <p role="alert" className="error menu-error">{error}</p>}
          </section>
        </div>
      )}
    </main>
  )
}

export default AddItemPage
