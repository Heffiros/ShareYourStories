import { FastifyReply, FastifyRequest } from 'fastify'

export const pageController = {
  getAll: async (request: FastifyRequest, reply: FastifyReply) => {

  },
  getById: async (
    request: FastifyRequest<{ Params: { id: string } }>,
    reply: FastifyReply
  ) => {

  }
}