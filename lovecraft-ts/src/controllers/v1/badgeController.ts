import { Badge } from '@prisma/client'
import { FastifyReply, FastifyRequest } from 'fastify'
import { IPostBadgeDto } from 'src/schemas/Badge'
import { STANDARD } from '../../constants/request'
import { toBadgeDto } from '../../helpers/badge.helper'
import { handleServerError } from '../../helpers/errors.helper'
import { prisma } from '../../utils'

export const badgeController = {
  getAll: async (request: FastifyRequest, reply: FastifyReply) => {
    const badges: Badge[] = await prisma.badge.findMany()
    const results = badges.map(badge => { return toBadgeDto(badge) })
    return reply.code(STANDARD.OK.statusCode).send(results)
  },
  getById: async (
    request: FastifyRequest<{ Params: { id: string } }>,
    reply: FastifyReply
  ) => {
    try {
      const paramId = request.params.id
      const badge: Badge = await prisma.badge.findUnique({
        where: { id: Number(paramId) }
      })
      return reply.code(STANDARD.OK.statusCode).send(toBadgeDto(badge))
    } catch (err) {
      return handleServerError(reply, err)
    }
  },
  post: async (
    request: FastifyRequest<{ Body: IPostBadgeDto }>,
    reply: FastifyReply
  ) => {
    const { label, emptyBadgeUrl, badgeUrl, description } = request.body

    const badgeCreated = await prisma.badge.create({
      data: {
        label: label,
        emptyBadgeUrl: emptyBadgeUrl,
        badgeUrl: badgeUrl,
        description: description,
        createdAt: new Date(),
        updatedAt: new Date()
      }
    })
    return reply.code(STANDARD.OK.statusCode).send(toBadgeDto(badgeCreated))
  }
}