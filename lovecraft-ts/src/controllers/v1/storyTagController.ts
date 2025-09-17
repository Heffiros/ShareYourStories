import { FastifyReply, FastifyRequest } from 'fastify'
import { toStoryTagDto } from '../../helpers/storyTag.helper'
import { prisma } from '../../utils'

export const storyTagController = {
  getAll: async (
    request: FastifyRequest<{ Querystring: { search: string } }>,
    reply: FastifyReply
  ) => {
    const { search } = request.query

    if (!search || search.length < 3) {
      return reply.status(400).send({ message: 'Search must be at least 3 characters long' })
    }

    const results = await prisma.storyTag.findMany({
      where: {
        label: {
          contains: search,
          mode: 'insensitive'
        }
      }
    })

    reply.send(results.map(toStoryTagDto))
  }
}