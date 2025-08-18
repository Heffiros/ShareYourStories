import { FastifyInstance } from 'fastify'
import { authController } from '../controllers/v1/authController'

export default async function (server: FastifyInstance) {
  server.post('/register', authController.register)
  server.post('/login', authController.login)
  //server.post('/refresh', authController.refresh)
}
