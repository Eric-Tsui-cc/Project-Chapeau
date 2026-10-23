import { Link } from 'react-router'

export default function RouteMessage({ title, message, backTo, backLabel }: {
  title: string
  message: string
  backTo: string
  backLabel: string
}) {
  return (
    <main className="session-screen">
      <section className="session-card">
        <h1>{title}</h1>
        <p>{message}</p>
        <Link className="button primary" style={{ textDecoration: 'none' }} to={backTo}>{backLabel}</Link>
      </section>
    </main>
  )
}
