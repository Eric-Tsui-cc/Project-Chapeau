import { useEffect, useId, useRef, useState } from 'react'
import type { SubmitEvent } from 'react'
import { formatMoney } from '../lib/customer-cart'
import type { CustomerMenuItem } from '../lib/customer-cart'
import { getAssistantReply } from '../api/assistant'
import './CustomerAssistant.css'

type Message = { id: number; role: 'user' | 'assistant'; text: string; itemIds?: number[] }
const suggestions = ['A main and a drink', 'Show me desserts', 'Something to drink']
const welcomeMessage: Message = { id: 0, role: 'assistant', text: 'Hello! Tell me what you feel like eating and your budget, and I can suggest dishes from our menu. Please include your preferences in each question.' }

export function CustomerAssistant({ open, onClose, items, basketLocked, onAdd }: {
  open: boolean
  onClose: () => void
  items: CustomerMenuItem[]
  basketLocked: boolean
  onAdd: (item: CustomerMenuItem) => string | null
}) {
  const panel = useRef<HTMLDialogElement>(null)
  const messagesEnd = useRef<HTMLDivElement>(null)
  const input = useRef<HTMLInputElement>(null)
  const activeRequest = useRef<AbortController | null>(null)
  const nextMessageId = useRef(1)
  const titleId = useId()
  const inputId = useId()
  const [draft, setDraft] = useState('')
  const [busy, setBusy] = useState(false)
  const [feedback, setFeedback] = useState('')
  const [failure, setFailure] = useState<{ question: string; message: string } | null>(null)
  const [messages, setMessages] = useState<Message[]>([])
  const welcoming = messages.length === 0

  useEffect(() => {
    if (!open || !welcoming) return
    const timeout = setTimeout(() => {
      setMessages((current) => current.length === 0 ? [welcomeMessage] : current)
    }, 1000)
    return () => clearTimeout(timeout)
  }, [open, welcoming])

  useEffect(() => {
    if (!open) return
    const element = panel.current!
    const previousFocus = document.activeElement
    const previousOverflow = document.body.style.overflow
    element.showModal()
    document.body.style.overflow = 'hidden'
    return () => {
      element.close()
      document.body.style.overflow = previousOverflow
      if (previousFocus instanceof HTMLElement && previousFocus.isConnected) previousFocus.focus()
    }
  }, [open])

  useEffect(() => {
    if (open) messagesEnd.current?.scrollIntoView({ block: 'nearest' })
  }, [messages, busy, open])

  useEffect(() => () => {
    const controller = activeRequest.current
    activeRequest.current = null
    controller?.abort()
  }, [])

  async function send(text: string, retry = false) {
    const trimmed = text.trim()
    if (welcoming || !trimmed || trimmed.length > 500 || activeRequest.current || items.length === 0) return
    const controller = new AbortController()
    activeRequest.current = controller
    // The backend's Gemini timeout is 30 seconds; allow time for CSRF and transit.
    const timeout = setTimeout(() => controller.abort(), 45000)
    setBusy(true)
    setFeedback('')
    setFailure(null)
    if (!retry) {
      setDraft('')
      const question: Message = { id: nextMessageId.current++, role: 'user', text: trimmed }
      setMessages((current) => [...current, question])
    }
    try {
      const reply = await getAssistantReply(trimmed, controller.signal)
      if (activeRequest.current !== controller) return
      const answer: Message = { id: nextMessageId.current++, role: 'assistant', text: reply.reply, itemIds: reply.menuItemIds }
      setMessages((current) => [...current, answer])
    } catch (error) {
      if (activeRequest.current !== controller) return
      setFailure({ question: trimmed, message: error instanceof Error ? error.message : 'The assistant is unavailable. Please try again.' })
    } finally {
      clearTimeout(timeout)
      if (activeRequest.current === controller) {
        activeRequest.current = null
        setBusy(false)
        if (panel.current?.open) input.current?.focus()
      }
    }
  }

  function submit(event: SubmitEvent<HTMLFormElement>) {
    event.preventDefault()
    void send(draft)
  }

  return (
    <dialog ref={panel} className="customer-assistant" aria-labelledby={titleId} onCancel={(event) => { event.preventDefault(); onClose() }}>
      <header className="assistant-header">
        <div><p className="assistant-eyebrow">YOUR MENU COMPANION</p><h2 id={titleId}>Ask Chapeau</h2></div>
        <button type="button" className="customer-control" onClick={onClose} aria-label="Close menu assistant">Close</button>
      </header>
      <p className="assistant-preview">{import.meta.env.VITE_DEMO === '1' ? <><span>Demo</span> Sample replies · No AI calls</> : <><span>AI assistant</span> Powered by Gemini · One question at a time</>}</p>
      <div className="assistant-messages" role="log" aria-label="Conversation" aria-live="polite" aria-relevant="additions" tabIndex={0}>
        {messages.map((message) => (
          <div key={message.id} className={`assistant-message assistant-message-${message.role}${message.id === 0 ? ' assistant-welcome' : ''}`}>
            {message.role === 'assistant' && <span className="assistant-avatar" aria-hidden="true">C</span>}
            <div className="assistant-message-content">
              <p className="assistant-speaker">{message.role === 'assistant' ? 'Chapeau' : 'You'}</p>
              <div className="assistant-bubble"><p className="assistant-message-text">{message.text}</p></div>
              {message.itemIds && message.itemIds.length > 0 && <div className="assistant-picks">
              {message.itemIds.map((id) => {
              const item = items.find((candidate) => candidate.id === id)
              if (!item) return null
              return <div key={id} className="assistant-pick"><div><h3>{item.name}</h3><p>{formatMoney(Math.round(item.price * 100))} <span>· {item.card}</span></p></div>
                <button type="button" className="customer-control" disabled={basketLocked || !item.isAvailable} aria-label={`Add ${item.name} to basket`} onClick={() => {
                  const error = onAdd(item)
                  setFeedback(error ?? `${item.name} added to your basket. You can adjust quantities and notes in the basket.`)
                }}>{item.isAvailable ? 'Add to basket' : 'Sold out'}</button></div>
              })}
              </div>}
            </div>
          </div>
        ))}
        {(welcoming || busy) && <div className="assistant-message assistant-message-assistant assistant-pending" role="status">
          <span className="assistant-avatar" aria-hidden="true">C</span>
          <div className="assistant-message-content">
            <p className="assistant-speaker" aria-hidden="true">Chapeau</p>
            <div className="assistant-bubble assistant-typing">
              <span className="assistant-typing-dots" aria-hidden="true"><span /><span /><span /></span>
              <span>{welcoming ? 'Chapeau is typing…' : 'Looking through the menu…'}</span>
            </div>
          </div>
        </div>}
        {!welcoming && !busy && items.length === 0 && <p className="assistant-thinking" role="status">The menu has not loaded yet, so I cannot suggest dishes. Please try again shortly, or browse the menu once it appears.</p>}
        <div ref={messagesEnd} />
      </div>
      <div className="assistant-bottom">
        {messages.length === 1 && <div className="assistant-suggestions" aria-label="Try a question">{suggestions.map((suggestion) => <button key={suggestion} type="button" className="customer-control" disabled={busy || items.length === 0} onClick={() => { void send(suggestion) }}>{suggestion}</button>)}</div>}
        {failure && <div className="assistant-error"><p role="alert">{failure.message}</p><button type="button" className="customer-control" disabled={busy || items.length === 0} onClick={() => { void send(failure.question, true) }}>Try again</button></div>}
        {basketLocked && <p className="assistant-feedback">Please ask your server to check your order or add more items.</p>}
        <p className="assistant-feedback" role="status">{feedback}</p>
        <form className="assistant-compose" onSubmit={submit}>
          <label className="assistant-input-label" htmlFor={inputId}>Message Chapeau</label>
          <input ref={input} id={inputId} type="text" maxLength={500} value={draft} onChange={(event) => setDraft(event.target.value)} placeholder="What would you like to eat?" autoComplete="off" />
          <button type="submit" className="customer-primary" disabled={welcoming || busy || !draft.trim() || items.length === 0}>Send</button>
        </form>
        <p className="assistant-footnote">Nothing is ordered until you confirm your basket. For allergies, please ask your server.</p>
      </div>
    </dialog>
  )
}
