import Fastify from 'fastify'
import fastifyJwt from '@fastify/jwt'
import { PrismaClient } from '@prisma/client'
import authRoutes from './routes/auth'

const server = Fastify()
const prisma = new PrismaClient()

server.decorate('prisma', prisma)

server.register(fastifyJwt, {
  secret: 'supersecretkey' // À mettre dans .env
})

server.register(authRoutes, { prefix: '/auth' })

server.listen({ port: 3000 }, (err, address) => {
  if (err) throw err
  console.log(`Server listening on ${address}`)
})

export type AppInstance = typeof server
