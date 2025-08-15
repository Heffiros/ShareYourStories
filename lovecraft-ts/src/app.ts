// app.ts
import Fastify from 'fastify'
import authRoutes from './routes/auth'

export function buildApp() {
  const app = Fastify({ logger: true })
  app.register(authRoutes)
  return app
}
