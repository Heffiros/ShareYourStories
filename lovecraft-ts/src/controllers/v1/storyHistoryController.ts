import { HistoryState } from '@prisma/client'
import { FastifyReply, FastifyRequest } from 'fastify'
import { toStoryHistoryDto } from '../../helpers/storyHistory.helper'
import { IPostStoryHistoryDto, IPutStoryHistoryDto } from '../../schemas/StoryHistory'
import { prisma } from '../../utils'

export const storyHistoryController = {
  getAll: async (
    request: FastifyRequest<{ Querystring: { page?: string } }>,
    reply: FastifyReply
  ) => {
    try {
      const currentUserId = request['authUser'].id

      const page = parseInt(request.query.page ?? '0', 10)
      const pageSize = 5

      const histories = await prisma.storyHistory.findMany({
        where: { userId: currentUserId },
        orderBy: { updatedAt: 'desc' },
        skip: page * pageSize,
        take: pageSize
      })

      reply.send(histories.map(toStoryHistoryDto))
    } catch (err) {
      request.log.error(err)
      return reply.status(500).send({ error: 'Internal server error' })
    }
  },
  post: async (
    request: FastifyRequest<{
      Body: IPostStoryHistoryDto
    }>,
    reply: FastifyReply
  ) => {
    try {
      const currentUserId = request['authUser'].id
      const body = request.body
      const { storyId, lastPageReadId } = body

      // Vérifier si un historique existe déjà pour cette story et cet utilisateur
      const exists = await prisma.storyHistory.findFirst({
        where: {
          userId: currentUserId,
          storyId: storyId
        }
      })

      if (exists) {
        return reply.status(409).send({ error: 'Resource already exists' })
      }

      await prisma.storyHistory.create({
        data: {
          userId: currentUserId,
          storyId: storyId,
          lastPageReadId: lastPageReadId,
          reread: 0,
          historyStateValue: 'Reading',
          createdAt: new Date(),
          updatedAt: new Date()
        }
      })

      return reply.status(200).send({ success: true })
    } catch (err) {
      request.log.error(err)
      return reply.status(500).send({ error: 'Internal server error' })
    }
  },
  put: async (
    request: FastifyRequest<{ Body: IPutStoryHistoryDto }>,
    reply: FastifyReply
  ) => {
    try {
      const currentUserId = request['authUser'].id
      const body = request.body
      const { storyId, lastPageReadId } = body

      // Trouver l'historique existant par storyId + userId
      const storyHistory = await prisma.storyHistory.findFirst({
        where: {
          storyId: storyId,
          userId: currentUserId
        },
        include: {
          story: {
            include: { pages: true }
          }
        }
      })

      if (!storyHistory) {
        return reply.status(404).send({ error: 'StoryHistory not found' })
      }

      let reread = storyHistory.reread
      let historyStateValue = storyHistory.historyStateValue

      const maxNbPages = storyHistory.story.pages.length
      const currentPage = storyHistory.story.pages.find(
        p => p.id === lastPageReadId
      )

      if (!currentPage) {
        return reply.status(400).send({ error: 'Invalid lastPageReadId' })
      }

      const currentPagesIndex = currentPage.order

      if (maxNbPages === currentPagesIndex + 1) {
        reread += 1
        historyStateValue = HistoryState.Endend
      }

      await prisma.storyHistory.update({
        where: { id: storyHistory.id },
        data: {
          lastPageReadId: lastPageReadId,
          reread,
          historyStateValue,
          updatedAt: new Date()
        }
      })

      return reply.status(200).send({ success: true })
    } catch (err) {
      request.log.error(err)
      return reply.status(500).send({ error: 'Internal server error' })
    }
  }
}