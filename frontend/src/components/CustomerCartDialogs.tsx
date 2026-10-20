import { useEffect, useId, useRef, useState } from 'react'
import type { ReactNode, SubmitEvent } from 'react'
import { cartTotalCents, formatMoney, MAX_QUANTITY } from '../lib/customer-cart'
import type { CartLine, CustomerMenuItem } from '../lib/customer-cart'

function CustomerDialog({ title, onClose, children }: { title: string; onClose: () => void; children: ReactNode }) {
  const dialog = useRef<HTMLDialogElement>(null)
  const titleId = useId()
  useEffect(() => {
    const element = dialog.current!
    const previousFocus = document.activeElement
    element.showModal()
    const previousOverflow = document.body.style.overflow
    document.body.style.overflow = 'hidden'
    return () => {
      element.close()
      document.body.style.overflow = previousOverflow
      if (previousFocus instanceof HTMLElement && previousFocus.isConnected) previousFocus.focus()
    }
  }, [])
  return (
    <dialog className="customer-dialog" ref={dialog} aria-labelledby={titleId} onCancel={(event) => { event.preventDefault(); onClose() }}>
      <header className="customer-dialog-header"><h2 id={titleId}>{title}</h2><button type="button" className="customer-control" onClick={onClose}>Close</button></header>
      {children}
    </dialog>
  )
}

export function CustomerItemDialog({ item, line, onClose, onSave }: {
  item: CustomerMenuItem
  line?: CartLine
  onClose: () => void
  onSave: (quantity: number, comment: string) => string | null
}) {
  const [quantity, setQuantity] = useState(String(line?.quantity ?? 1))
  const [comment, setComment] = useState(line?.comment ?? '')
  const [error, setError] = useState<string | null>(null)
  const quantityId = useId()
  const commentId = useId()
  const numericQuantity = Number(quantity)
  function submit(event: SubmitEvent<HTMLFormElement>) {
    event.preventDefault()
    setError(onSave(numericQuantity, comment))
  }
  return (
    <CustomerDialog title={line ? 'Edit basket item' : 'Add to basket'} onClose={onClose}>
      <form className="customer-item-form" onSubmit={submit}>
        <div className="customer-dish-top"><h3>{item.name}</h3><strong>{formatMoney(Math.round(item.price * 100))} each</strong></div>
        <label htmlFor={quantityId}>Quantity</label>
        <div className="customer-quantity">
          <button className="customer-control" type="button" aria-label="Decrease quantity" disabled={numericQuantity <= 1} onClick={() => setQuantity(String(Math.max(1, Math.trunc(numericQuantity || 1) - 1)))}>−</button>
          <input id={quantityId} type="number" inputMode="numeric" min={1} max={MAX_QUANTITY} step={1} required value={quantity} onChange={(event) => { setQuantity(event.target.value); setError(null) }} />
          <button className="customer-control" type="button" aria-label="Increase quantity" disabled={numericQuantity >= MAX_QUANTITY} onClick={() => setQuantity(String(Math.min(MAX_QUANTITY, Math.max(1, Math.trunc(numericQuantity || 0) + 1))))}>+</button>
        </div>
        <label htmlFor={commentId}>Notes (optional)</label>
        <textarea id={commentId} rows={3} value={comment} onChange={(event) => { setComment(event.target.value); setError(null) }} placeholder="e.g. No onion" />
        {error && <p className="customer-cart-error" role="alert">{error}</p>}
        <button className="customer-primary" type="submit">{line ? 'Save changes' : 'Add to basket'}</button>
      </form>
    </CustomerDialog>
  )
}

export function CustomerBasketDialog({ lines, onClose, onEdit, onQuantity, onRemove }: {
  lines: CartLine[]
  onClose: () => void
  onEdit: (line: CartLine) => void
  onQuantity: (key: string, quantity: number) => void
  onRemove: (key: string) => void
}) {
  const totalQuantity = lines.reduce((sum, line) => sum + line.quantity, 0)
  return (
    <CustomerDialog title="Your basket" onClose={onClose}>
      {lines.length === 0 ? <div className="customer-empty-basket"><p>Your basket is empty.</p><button className="customer-primary" type="button" onClick={onClose}>Browse the menu</button></div> : (
        <>
          <ul className="customer-cart-lines">
            {lines.map((line) => (
              <li key={line.key}>
                <div className="customer-dish-top"><h3>{line.name}</h3><strong>{formatMoney(line.unitPriceCents * line.quantity)}</strong></div>
                <p className="customer-cart-unit">{formatMoney(line.unitPriceCents)} each</p>
                {line.comment && <p className="customer-cart-note"><strong>Notes:</strong> {line.comment}</p>}
                <div className="customer-cart-actions">
                  <div className="customer-quantity customer-cart-quantity" role="group" aria-label={`Quantity for ${line.name}${line.comment ? `, ${line.comment}` : ''}`}>
                    <button className="customer-control" type="button" aria-label={`Decrease ${line.name} quantity`} disabled={line.quantity <= 1} onClick={() => onQuantity(line.key, line.quantity - 1)}>−</button>
                    <output aria-live="polite">{line.quantity}</output>
                    <button className="customer-control" type="button" aria-label={`Increase ${line.name} quantity`} disabled={line.quantity >= MAX_QUANTITY} onClick={() => onQuantity(line.key, line.quantity + 1)}>+</button>
                  </div>
                  <div className="customer-cart-links"><button className="customer-reset" type="button" aria-label={`Edit ${line.name}`} onClick={() => onEdit(line)}>Edit</button><button className="customer-reset customer-remove" type="button" aria-label={`Remove ${line.name}`} onClick={() => onRemove(line.key)}>Remove</button></div>
                </div>
              </li>
            ))}
          </ul>
          <div className="customer-cart-summary"><span>Estimated total · {totalQuantity} {totalQuantity === 1 ? 'item' : 'items'}</span><strong>{formatMoney(cartTotalCents(lines))}</strong></div>
          <p className="customer-cart-disclaimer">Your selection has not been sent. Please ask your server to place the order. Prices and availability will be confirmed when ordering.</p>
          <p className="customer-cart-disclaimer">Your basket stays while you browse this page. Refreshing or leaving the page clears it.</p>
          <button className="customer-primary" type="button" onClick={onClose}>Continue browsing</button>
        </>
      )}
    </CustomerDialog>
  )
}
