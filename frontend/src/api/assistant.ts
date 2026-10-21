import { csrfFetch } from './client'

export type AssistantReply = { reply: string; menuItemIds: number[] }

export async function getAssistantReply(message: string, signal: AbortSignal): Promise<AssistantReply> {
  const trimmed = message.trim()
  if (!trimmed || trimmed.length > 500) throw new Error('Enter a question of up to 500 characters.')

  let response: Response
  try {
    response = await csrfFetch('/api/assistant/chat', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ message: trimmed }),
      signal,
    })
  } catch {
    throw new Error(signal.aborted
      ? 'The assistant took too long to respond. Please try again.'
      : 'We could not reach the assistant. Check your connection and try again.')
  }

  if (!response.ok) {
    if (response.status === 400) throw new Error('Your question could not be accepted. Try again, or refresh the page if this continues.')
    if (response.status === 429) throw new Error('The assistant is receiving too many requests. Please wait a moment and try again.')
    if (response.status === 504) throw new Error('The assistant took too long to respond. Please try again.')
    throw new Error('The assistant is temporarily unavailable. You can still order from the menu.')
  }

  let data: unknown
  try {
    data = await response.json()
  } catch {
    throw new Error('The assistant returned an unreadable reply. Please try again.')
  }
  if (!data || typeof data !== 'object' || !('reply' in data) || !('menuItemIds' in data) ||
    typeof data.reply !== 'string' || !data.reply.trim() || !Array.isArray(data.menuItemIds) ||
    !data.menuItemIds.every((id: unknown) => typeof id === 'number' && Number.isSafeInteger(id) && id > 0)) {
    throw new Error('The assistant returned an invalid reply. Please try again.')
  }
  return { reply: data.reply.trim(), menuItemIds: [...new Set<number>(data.menuItemIds)] }
}
