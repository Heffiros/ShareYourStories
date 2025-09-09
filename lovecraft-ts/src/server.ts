import Fastify from 'fastify'
import fastifyJwt from '@fastify/jwt'
import multipart from '@fastify/multipart'
import { PrismaClient } from '@prisma/client'

import authRoutes from './routes/auth'
import userRoutes from './routes/user'
import storyRoutes from './routes/story'
import uploadRoutes from './routes/upload'
import badgeRoutes from './routes/badge'

const server = Fastify()
const prisma = new PrismaClient()

server.decorate('prisma', prisma)

server.register(fastifyJwt, {
  secret: 'supersecretkey'
})

server.register(multipart, {
  limits: {
    fileSize: 10 * 1024 * 1024 // 10MB max
  }
})

server.register(authRoutes, { prefix: '/auth' })
server.register(userRoutes, { prefix: '/users' })
server.register(storyRoutes, { prefix: '/stories' })
server.register(uploadRoutes, { prefix: '/upload' })
server.register(badgeRoutes, { prefix: '/badges' })

server.listen({ port: 3000 }, (err, address) => {
  if (err) throw err
  console.log(`Server listening on ${address}`)
})

export type AppInstance = typeof server
