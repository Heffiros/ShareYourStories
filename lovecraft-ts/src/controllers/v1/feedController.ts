import { FastifyReply, FastifyRequest } from 'fastify'
import { STANDARD } from '../../constants/request'
import { ERRORS, handleServerError } from '../../helpers/errors.helper'
import { toStoryDto } from '../../helpers/story.helper'
import { StoryWithRelations } from "../../schemas/Story"
import { prisma } from '../../utils'

export const feedController = {
  feedHistory: async (request, reply) => {
    // Implementation for feed history
  },
  feedOngoing: async (request, reply) => {

  },
  feedStorysByTagId: async (
    request: FastifyRequest<{
      Params: { storyTagId: string },
      Querystring: { page?: number, limit?: number }
    }>,
    reply: FastifyReply
  ) => {
    try {
      const storyTagId = Number(request.params.storyTagId)
      const { page = 0, limit = 10 } = request.query
      const currentUserId = request['authUser'].id

      // Convertir les paramètres en nombres
      const pageNumber = Number(page)
      const limitNumber = Number(limit)

      if (!currentUserId) {
        return reply
          .code(ERRORS.userNotExists.statusCode)
          .send(ERRORS.userNotExists.message)
      }

      if (isNaN(storyTagId)) {
        return reply
          .code(ERRORS.badRequest.statusCode)
          .send('Invalid storyTagId')
      }

      // Vérifier que le tag existe
      const storyTag = await prisma.storyTag.findUnique({
        where: { id: storyTagId }
      })

      if (!storyTag) {
        return reply
          .code(ERRORS.NotFound.statusCode)
          .send('Story tag not found')
      }

      // Récupérer les histoires filtrées par tag
      const stories: StoryWithRelations[] = await prisma.story.findMany({
        where: {
          status: 'Online', // Seulement les histoires publiées
          storyStoryTags: {
            some: { storyTagId }
          }
        },
        include: {
          pages: true,
          user: true,
          team: true,
          event: true,
          storyVotes: true,
          storyStoryTags: {
            include: { storyTag: true }
          },
          storyHistories: {
            where: { userId: currentUserId },
            take: 1
          },
          _count: {
            select: {
              storyComments: true
            }
          }
        },
        skip: pageNumber * limitNumber,
        take: limitNumber,
        orderBy: { createdAt: 'desc' }
      })

      const results = stories.map(story => toStoryDto(story))

      return reply.code(STANDARD.OK.statusCode).send({
        stories: results,
        tag: storyTag,
        pagination: {
          page: pageNumber,
          limit: limitNumber,
          hasMore: results.length === limitNumber
        }
      })
    } catch (error) {
      console.error(error)
      return handleServerError(reply, error)
    }
  }
}