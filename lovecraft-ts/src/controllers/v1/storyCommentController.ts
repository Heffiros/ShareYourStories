import { FastifyReply, FastifyRequest } from 'fastify'
import { IPostStoryCommentDto } from 'src/schemas/StoryComment'
import { ERRORS } from '../../helpers/errors.helper'
import { toStoryCommentDto } from '../../helpers/storyComment.helper'
import { prisma } from '../../utils'

export const storyCommentController = {
  getAll: async (
    request: FastifyRequest,
    reply: FastifyReply
  ) => {
    const { storyId, page = 0 } = request.query as {
      storyId: number
      page: number
    }
    const results = await prisma.storyComment.findMany({
      where: { storyId },
      skip: page * 10,
      take: 10,
      include: {
        user: true
      },
      orderBy: {
        createdAt: 'desc'
      }
    })

    reply.send(results.map(toStoryCommentDto))
  },
  getById: async (
    request: FastifyRequest<{ Params: { id: string } }>,
    reply: FastifyReply
  ) => {
    const storyCommentId = Number(request.params.id)

    const storyComment = await prisma.storyComment.findUnique({
      where: { id: storyCommentId },
      include: {
        user: true
      }
    })
    if (storyComment == null) {
      return reply
        .code(ERRORS.NotFound.statusCode)
        .send(ERRORS.NotFound.message)
    }
    return reply.send(toStoryCommentDto(storyComment))
  },
  post: async (
    request: FastifyRequest<{
      Body: IPostStoryCommentDto
    }>,
    reply: FastifyReply
  ) => {
    const body = request.body
    const currentUserId = request['authUser'].id

    const { text, status, storyId } = body

    const story = await prisma.story.findUnique({
      where: { id: storyId }
    })

    if (story == null) {
      return reply
        .code(ERRORS.NotFound.statusCode)
        .send(ERRORS.NotFound.message)
    }

    const newStoryComment = await prisma.storyComment.create({
      data: {
        text,
        status,
        storyId,
        userId: currentUserId
      },
      include: {
        user: true
      }
    })

    return reply.send(toStoryCommentDto(newStoryComment))
  }
}