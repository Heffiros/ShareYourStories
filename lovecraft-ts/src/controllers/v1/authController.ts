import { FastifyReply, FastifyRequest } from 'fastify'
import { User } from '../../domain/user'
import jwt from 'jsonwebtoken'

const JWT_SECRET = 'supersecret' // plus tard mettre en env

export const authController = {
  register: async (request: FastifyRequest, reply: FastifyReply) => {
    const { email, password } = request.body as { email: string; password: string }
    try {
      const user = await User.register({ email, password })
      reply.send({ id: user.id, email: user.email })
    } catch (err: any) {
      reply.status(400).send({ error: err.message })
    }
  },

  login: async (request: FastifyRequest, reply: FastifyReply) => {
    const { email, password } = request.body as { email: string; password: string }
    try {
      const user = await User.login(email, password)
      const token = user.generateToken()
      const refreshToken = user.generateRefreshToken()
      reply.send({ token, refreshToken })
    } catch (err: any) {
      reply.status(401).send({ error: err.message })
    }
  },

  refresh: async (request: FastifyRequest, reply: FastifyReply) => {
    const { refreshToken } = request.body as { refreshToken: string }
    try {
      const payload = jwt.verify(refreshToken, JWT_SECRET) as any
      const user = await User.login(payload.email, '') // bypass password check car token déjà valide
      const token = user.generateToken()
      reply.send({ token })
    } catch (err: any) {
      reply.status(401).send({ error: 'Invalid refresh token' })
    }
  }
}
