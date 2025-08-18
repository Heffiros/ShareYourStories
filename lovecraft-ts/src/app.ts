// app.ts
import Fastify from 'fastify'
import authRoutes from './routes/auth'
import formbody from '@fastify/formbody';
import cors from '@fastify/cors';
import helmet from '@fastify/helmet';

export function buildApp() {
  const app = Fastify({ logger: true })

  app.register(formbody)
  app.register(cors)
  app.register(helmet)

  app.register(authRoutes)
  return app
}
