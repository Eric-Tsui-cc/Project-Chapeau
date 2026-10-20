export type Table = {
  id: number
  number: number
  /** Not guaranteed by every backend response; render a fallback when missing. */
  capacity?: number
  status: 'Free' | 'Occupied'
}
