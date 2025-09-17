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
          user: { connect: { id: body.userId } },
          story: { connect: { id: body.storyId } },
          lastPageRead: body.lastPageReadId
            ? { connect: { id: body.lastPageReadId } }
            : undefined,
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
      const body = request.body

      const storyHistory = await prisma.storyHistory.findUnique({
        where: { id: body.id },
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
        p => p.id === body.lastPageReadId
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
        where: { id: body.id },
        data: {
          lastPageReadId: body.lastPageReadId,
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