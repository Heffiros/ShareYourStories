import Fastify from 'fastify'
import fastifyJwt from '@fastify/jwt'
import multipart from '@fastify/multipart'
import { PrismaClient } from '@prisma/client'

import authRoutes from './routes/auth'
import userRoutes from './routes/user'
import storyRoutes from './routes/story'
import uploadRoutes from './routes/upload'

const server = Fastify()
const prisma = new PrismaClient()

server.decorate('prisma', prisma)

server.register(fastifyJwt, {
  secret: 'supersecretkey'
})

// 👇 IMPORTANT : multipart en premier
server.register(multipart)

server.register(authRoutes, { prefix: '/auth' })
server.register(userRoutes, { prefix: '/users' })
server.register(storyRoutes, { prefix: '/stories' })
server.register(uploadRoutes, { prefix: '/upload' })

server.listen({ port: 3000 }, (err, address) => {
  if (err) throw err
  console.log(`Server listening on ${address}`)
})

export type AppInstance = typeof server
