import { CustomerBasketDialog, CustomerItemDialog } from '../components/CustomerCartDialogs'
import { CustomerAssistant } from '../components/CustomerAssistant'
import { cartTotalCents, formatMoney, loadSavedBasket, saveBasket, saveCartLine, setCartQuantity } from '../lib/customer-cart'
import type { CartLine, CustomerMenuItem } from '../lib/customer-cart'
import { useEffect, useRef, useState } from 'react'
import { submitCustomerOrder } from '../api/customer-orders'
import type { CustomerConfirmation } from '../api/customer-orders'
import './CustomerMenuPage.css'

const categoryNames: Record<string, string> = {
  Starters: 'Starters', Mains: 'Mains', Entremet: 'Sides', Desserts: 'Desserts',
  Beers: 'Beers', Wines: 'Wines', Spirit: 'Spirits', CoffeeTea: 'Coffee, tea & cocktails',
}

function CustomerMenuPage() {
  const [items, setItems] = useState<CustomerMenuItem[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [attempt, setAttempt] = useState(0)
  const [card, setCard] = useState<string | null>(null)
  const [category, setCategory] = useState<string | null>(null)
  const [basket, setBasket] = useState<CartLine[]>(loadSavedBasket)
  const [notice, setNotice] = useState('')
  const [assistantOpen, setAssistantOpen] = useState(false)
  const [tableNumber, setTableNumber] = useState('')
  const [submitting, setSubmitting] = useState(false)
  const [submitError, setSubmitError] = useState<string | null>(null)
  const [submissionBlocked, setSubmissionBlocked] = useState(false)
  const [confirmation, setConfirmation] = useState<CustomerConfirmation | null>(null)
  const submissionInFlight = useRef(false)
  const basketLocked = submitting || submissionBlocked || confirmation !== null
  const [dialog, setDialog] = useState<
    { kind: 'basket' } | { kind: 'item'; item: CustomerMenuItem; line?: CartLine } | null
  >(null)
  const basketCount = basket.reduce((count, line) => count + line.quantity, 0)

  useEffect(() => {
    saveBasket(basket)
  }, [basket])

  useEffect(() => {
    document.title = 'Chapeau | Menu'
  }, [])

  async function sendOrder() {
    if (submissionInFlight.current || basketLocked) return
    submissionInFlight.current = true
    setSubmitting(true)
    setSubmitError(null)
    try {
      const result = await submitCustomerOrder(Number(tableNumber), basket)
      if (result.kind === 'submitted') {
        setConfirmation(result.confirmation)
        setBasket([])
        setNotice(`Order #${result.confirmation.orderId} received for table ${result.confirmation.tableNumber}. Please ask your server for further additions.`)
      } else {
        setSubmitError(result.message)
        setSubmissionBlocked(result.kind === 'uncertain')
      }
    } finally {
      submissionInFlight.current = false
      setSubmitting(false)
    }
  }

  function saveItem(quantity: number, comment: string): string | null {
    if (basketLocked) return 'Please ask your server to check your order before adding items.'
    if (dialog?.kind !== 'item') return 'Please select an item first.'
    try {
      const next = saveCartLine(basket, dialog.item, quantity, comment, dialog.line?.key)
      setBasket(next)
      setSubmitError(null)
      setNotice(dialog.line ? `${dialog.item.name} updated in your basket.` : `${quantity} × ${dialog.item.name} added to your basket.`)
      setDialog(dialog.line ? { kind: 'basket' } : null)
      return null
    } catch (error) {
      return error instanceof Error ? error.message : 'Unable to update your basket.'
    }
  }

  useEffect(() => {
    const controller = new AbortController()
    let active = true
    const timeout = setTimeout(() => controller.abort(), 10000)
    async function loadMenu() {
      try {
        // Public menu browsing does not require or restore an employee session.
        const response = await fetch('/api/menuitems/customer', {
          credentials: 'omit', signal: controller.signal,
        })
        if (!response.ok) throw new Error('Unable to load menu')
        const data: CustomerMenuItem[] = await response.json()
        if (active) setItems(data)
      } catch {
        if (active) setError('We could not load the menu. Please try again.')
      } finally {
        clearTimeout(timeout)
        if (active) setLoading(false)
      }
    }
    void loadMenu()
    return () => { active = false; clearTimeout(timeout); controller.abort() }
  }, [attempt])

  const cards = [...new Set(items.map((item) => item.card))]
  const categories = [...new Set(items.map((item) => item.category))]
  const visible = items.filter((item) =>
    (card === null || item.card === card) && (category === null || item.category === category))
  const groups = [...new Set(visible.map((item) => item.category))]

  return (
    <div className="customer-menu">
      <header className="customer-header">
        <a className="customer-brand" href="/menu">CHAPEAU<span>Good food. Good company.</span></a>
                <div className="customer-header-actions">
          <button className="customer-control" type="button" onClick={() => setDialog({ kind: 'basket' })} aria-label={confirmation ? `Order #${confirmation.orderId}, view details` : `Basket (${basketCount}), view`}>{confirmation ? `Order #${confirmation.orderId}` : `Basket (${basketCount})`}</button>
          <a className="customer-staff-link" href="/">Staff sign in</a>
        </div>
      </header>
      <main className="customer-main">
        <section className="customer-intro" aria-labelledby="menu-title">
          <p className="customer-eyebrow">WELCOME TO CHAPEAU</p>
          <h1 id="menu-title">Something for every appetite.</h1>
          <p>Choose your favourites, then enter your table number to send your order. Please ask your server to open your table first.</p>
        </section>
        <p className="customer-cart-notice" role="status" aria-live="polite">{notice}</p>
        {loading && <p className="customer-message" role="status">Loading the menu...</p>}
        {error && <div className="customer-message"><p role="alert">{error}</p><button type="button" className="customer-control" onClick={() => { setError(null); setLoading(true); setAttempt((value) => value + 1) }}>Try again</button></div>}
        {!loading && !error && items.length === 0 && <p className="customer-message">The menu is not available yet. Please ask your server.</p>}
        {!loading && !error && items.length > 0 && (
          <>
            <div className="customer-filters">
              <fieldset><legend>Menu</legend><div className="customer-filter-row">
                {cards.map((name) => <button key={name} type="button" className="customer-control" aria-pressed={card === name} onClick={() => setCard((current) => current === name ? null : name)}>{name}</button>)}
              </div></fieldset>
              <fieldset><legend>Category</legend><div className="customer-filter-row">
                {categories.map((name) => <button key={name} type="button" className="customer-control" aria-pressed={category === name} onClick={() => setCategory((current) => current === name ? null : name)}>{categoryNames[name] ?? name}</button>)}
              </div></fieldset>
              {(card !== null || category !== null) && <button type="button" className="customer-reset" onClick={() => { setCard(null); setCategory(null) }}>Reset filters</button>}
            </div>
            <p className="customer-results" role="status">{visible.length} {visible.length === 1 ? 'item' : 'items'}{card ? ` · ${card}` : ''}{category ? ` · ${categoryNames[category] ?? category}` : ''}</p>
            {visible.length === 0 && <p className="customer-message">No items match this combination. Clear a selected filter to see more.</p>}
            {groups.map((group) => (
              <section className="customer-section" key={group} aria-labelledby={`category-${group}`}>
                <h2 id={`category-${group}`}>{categoryNames[group] ?? group}</h2>
                <ul className="customer-dishes">
                  {visible.filter((item) => item.category === group).map((item) => (
                    <li key={item.id} className={`customer-dish${item.isAvailable ? '' : ' is-sold-out'}`}>
                      <div className="customer-dish-top"><h3>{item.name}</h3><span className="customer-price">${item.price.toFixed(2)}</span></div>
                      <div className="customer-dish-meta"><span>{item.card}</span>{!item.isAvailable && <span className="customer-sold-out">Sold out</span>}</div>
                      {item.isAvailable && <button className="customer-control customer-add" type="button" disabled={basketLocked} aria-label={`Add ${item.name} to basket`} onClick={() => setDialog({ kind: 'item', item })}>Add</button>}
                    </li>
                  ))}
                </ul>
              </section>
            ))}
          </>
        )}
        <footer className="customer-footer">Questions about ingredients or dietary needs? Please speak to your server.</footer>
      </main>
      {(basketCount > 0 || confirmation) && <div className="customer-basket-dock"><button className="customer-primary" type="button" onClick={() => setDialog({ kind: 'basket' })}>{confirmation ? <span>View order confirmation · #{confirmation.orderId}</span> : <><span>View basket · {basketCount} {basketCount === 1 ? 'item' : 'items'}</span><span>{formatMoney(cartTotalCents(basket))}</span></>}</button></div>}
      <button type="button" className={`customer-assistant-launcher${basketCount > 0 || confirmation ? ' has-basket' : ''}`} aria-haspopup="dialog" aria-expanded={assistantOpen} onClick={() => setAssistantOpen(true)}>
        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="1.7" aria-hidden="true"><path d="M20 11.5a8 8 0 0 1-8 8H5l-3 2V11.5a9 9 0 0 1 18 0Z" /><path d="M7 10h8M7 14h5" /></svg>
        Ask Chapeau
      </button>
      <CustomerAssistant open={assistantOpen} onClose={() => setAssistantOpen(false)} items={items} basketLocked={basketLocked} onAdd={(item) => {
        if (basketLocked) return 'Please ask your server to check your order before adding items.'
        const currentItem = items.find((candidate) => candidate.id === item.id)
        if (!currentItem) return 'This item is no longer available.'
        try {
          setBasket(saveCartLine(basket, currentItem, 1, ''))
          setSubmitError(null)
          setNotice(`${currentItem.name} added to your basket.`)
          return null
        } catch (error) {
          return error instanceof Error ? error.message : 'Unable to add this item. Please try the menu.'
        }
      }} />
      {dialog?.kind === 'item' && <CustomerItemDialog key={dialog.line?.key ?? dialog.item.id} item={dialog.item} line={dialog.line} onClose={() => setDialog(dialog.line ? { kind: 'basket' } : null)} onSave={saveItem} />}
      {dialog?.kind === 'basket' && <CustomerBasketDialog lines={basket} onClose={() => { if (!submitting) setDialog(null) }}
        submission={{ tableNumber, busy: submitting, blocked: submissionBlocked, error: submitError, confirmation,
          onTableNumber: (value) => { if (!basketLocked) { setTableNumber(value); setSubmitError(null) } },
          onSubmit: () => { void sendOrder() },
        }}
        onEdit={(line) => {
          if (basketLocked) return
          const item = items.find((item) => item.id === line.menuItemId)
          if (item) setDialog({ kind: 'item', item, line })
        }}
        onQuantity={(key, quantity) => { if (!basketLocked) { setBasket((current) => setCartQuantity(current, key, quantity)); setSubmitError(null) } }}
        onRemove={(key) => {
          if (basketLocked) return
          setSubmitError(null)
          setBasket((current) => current.filter((line) => line.key !== key))
          setNotice('Item removed from your basket.')
        }} />}
    </div>
  )
}

export default CustomerMenuPage
