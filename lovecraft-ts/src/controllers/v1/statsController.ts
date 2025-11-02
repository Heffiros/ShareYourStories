import { Status } from '@prisma/client'
import { FastifyReply, FastifyRequest } from 'fastify'
import { STANDARD } from '../../constants/request'
import { ERRORS, handleServerError } from '../../helpers/errors.helper'
import { StoriesStatsDto } from '../../schemas/Stats'
import { prisma } from '../../utils'

export const statsController = {
  getStoriesStats: async (request: FastifyRequest, reply: FastifyReply) => {
    try {
      const { userId } = request.query as {
        userId?: string
      }

      const currentUserId = request['authUser'].id
      if (!currentUserId) {
        return reply
          .code(ERRORS.userNotExists.statusCode)
          .send(ERRORS.userNotExists.message)
      }

      if (!userId) {
        return reply
          .code(ERRORS.badRequest.statusCode)
          .send({ message: 'userId parameter is required' })
      }

      const targetUserId = Number(userId)
      const isOwnProfile = targetUserId === currentUserId

      if (isOwnProfile) {
        const [totalCount, draftCount, pendingCount, onlineCount] = await Promise.all([
          prisma.story.count({
            where: { userId: targetUserId }
          }),
          prisma.story.count({
            where: { userId: targetUserId, status: Status.Draft }
          }),
          prisma.story.count({
            where: { userId: targetUserId, status: Status.Pending }
          }),
          prisma.story.count({
            where: { userId: targetUserId, status: Status.Online }
          })
        ])

        const stats: StoriesStatsDto = {
          total: totalCount,
          draft: draftCount,
          pending: pendingCount,
          online: onlineCount
        }

        return reply.code(STANDARD.OK.statusCode).send(stats)
      } else {
        const onlineCount = await prisma.story.count({
          where: { userId: targetUserId, status: Status.Online }
        })

        const stats: StoriesStatsDto = {
          total: onlineCount,
          online: onlineCount
        }

        return reply.code(STANDARD.OK.statusCode).send(stats)
      }
    } catch (error) {
      console.error(error)
      return handleServerError(reply, error)
    }
  }
}