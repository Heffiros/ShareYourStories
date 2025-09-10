// app.ts
import cors from '@fastify/cors';
import formbody from '@fastify/formbody';
import helmet from '@fastify/helmet';
import Fastify from 'fastify';

export function buildApp() {
  const app = Fastify({ logger: true })

  app.register(formbody)
  app.register(cors)
  app.register(helmet)

  return app
}
