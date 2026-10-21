import { useEffect, useId, useRef, useState } from 'react'
import type { ReactNode, SubmitEvent } from 'react'
import { cartTotalCents, formatMoney, MAX_QUANTITY } from '../lib/customer-cart'
import type { CartLine, CustomerMenuItem } from '../lib/customer-cart'
import type { CustomerConfirmation } from '../api/customer-orders'

function CustomerDialog({ title, onClose, children, busy = false }: { title: string; onClose: () => void; children: ReactNode; busy?: boolean }) {
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
    <dialog className="customer-dialog" ref={dialog} aria-labelledby={titleId} aria-busy={busy} onCancel={(event) => { event.preventDefault(); if (!busy) onClose() }}>
      <header className="customer-dialog-header"><h2 id={titleId}>{title}</h2><button type="button" className="customer-control" onClick={onClose} disabled={busy}>Close</button></header>
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

export function CustomerBasketDialog({ lines, onClose, onEdit, onQuantity, onRemove, submission }: {
  lines: CartLine[]
  onClose: () => void
  onEdit: (line: CartLine) => void
  onQuantity: (key: string, quantity: number) => void
  onRemove: (key: string) => void
  submission: {
    tableNumber: string
    busy: boolean
    blocked: boolean
    error: string | null
    confirmation: CustomerConfirmation | null
    onTableNumber: (value: string) => void
    onSubmit: () => void
  }
}) {
  const totalQuantity = lines.reduce((sum, line) => sum + line.quantity, 0)
  const tableId = useId()
  const errorId = useId()
  const locked = submission.busy || submission.blocked
  const tableNumberValid = /^\d+$/.test(submission.tableNumber) && Number(submission.tableNumber) > 0 && Number(submission.tableNumber) <= 2147483647
  if (submission.confirmation) return (
    <CustomerDialog title="Order received" onClose={onClose}>
      <div className="customer-order-confirmation" role="status">
        <p className="customer-confirmed-mark" aria-hidden="true">✓</p>
        <h3>Thank you for your order.</h3>
        <p>Table <strong>{submission.confirmation.tableNumber}</strong> · Order <strong>#{submission.confirmation.orderId}</strong></p>
        <p>Your items have been sent to the team. To add more items, please ask your server.</p>
        <button className="customer-primary" type="button" onClick={onClose}>Back to menu</button>
      </div>
    </CustomerDialog>
  )
  return (
    <CustomerDialog title="Your basket" onClose={onClose} busy={submission.busy}>
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
                    <button className="customer-control" type="button" aria-label={`Decrease ${line.name} quantity`} disabled={locked || line.quantity <= 1} onClick={() => onQuantity(line.key, line.quantity - 1)}>−</button>
                    <output aria-live="polite">{line.quantity}</output>
                    <button className="customer-control" type="button" aria-label={`Increase ${line.name} quantity`} disabled={locked || line.quantity >= MAX_QUANTITY} onClick={() => onQuantity(line.key, line.quantity + 1)}>+</button>
                  </div>
                  <div className="customer-cart-links"><button className="customer-reset" type="button" disabled={locked} aria-label={`Edit ${line.name}`} onClick={() => onEdit(line)}>Edit</button><button className="customer-reset customer-remove" type="button" disabled={locked} aria-label={`Remove ${line.name}`} onClick={() => onRemove(line.key)}>Remove</button></div>
                </div>
              </li>
            ))}
          </ul>
          <div className="customer-cart-summary"><span>Estimated total · {totalQuantity} {totalQuantity === 1 ? 'item' : 'items'}</span><strong>{formatMoney(cartTotalCents(lines))}</strong></div>
          <p className="customer-cart-disclaimer">Prices and availability are confirmed when your order is accepted. Further additions are handled by your server.</p>
          <p className="customer-cart-disclaimer">Your basket is kept on this device until you close this tab or your order is sent.</p>
          <form className="customer-submit-form" onSubmit={(event) => { event.preventDefault(); submission.onSubmit() }}>
            <label htmlFor={tableId}>Table number</label>
            <input id={tableId} type="text" inputMode="numeric" pattern="[0-9]+" required maxLength={10} placeholder="e.g. 3" value={submission.tableNumber} disabled={locked} onChange={(event) => submission.onTableNumber(event.target.value)} aria-describedby={submission.error ? errorId : undefined} />
            <p className="customer-cart-disclaimer">Check your table number before confirming. Your server must open the table first.</p>
            {submission.error && <p id={errorId} className="customer-cart-error" role="alert">{submission.error}</p>}
            <button className="customer-primary" type="submit" disabled={locked || !tableNumberValid}>{submission.busy ? 'Sending order...' : 'Confirm and send order'}</button>
            {submission.busy && <p role="status">Please wait while we confirm your order.</p>}
          </form>
          <button className="customer-control customer-continue" type="button" disabled={submission.busy} onClick={onClose}>Continue browsing</button>
        </>
      )}
    </CustomerDialog>
  )
}
