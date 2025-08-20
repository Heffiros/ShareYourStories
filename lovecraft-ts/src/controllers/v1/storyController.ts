import fastify, { FastifyReply, FastifyRequest } from 'fastify'
import { prisma } from '../../utils'
import { ERRORS, handleServerError } from '../../helpers/errors.helper'
import { STANDARD } from '../../constants/request'
import { Status } from '@prisma/client'
import { StoryWithRelations } from "../../schemas/Story"
import { toStoryDto } from "../../helpers/story.helper"

export const storyController = {
  getAll: async (request: FastifyRequest, reply: FastifyReply) => {
    try {
      const { userId, teamsId, eventId, search, storyTagId, page = 0, isAdmin = false } = request.query as {
        userId?: number
        teamsId?: number
        eventId?: number
        search?: string
        storyTagId?: number
        page?: number
        isAdmin?: boolean
      }
      const currentUserId = request['authUser'].id
      if (!currentUserId) {
        return reply
        .code(ERRORS.userNotExists.statusCode)
        .send(ERRORS.userNotExists.message)
      }
      const filters: any = {}      
      if (teamsId) {
        filters.teamId = teamsId
      } else if (eventId) {
        filters.eventId = eventId
      } else if (userId) {
        filters.userId = currentUserId
      } 
      
      if (search) {
        filters.title = { contains: search }
      }  
      
      if (storyTagId) {
        filters.StoryStoryTags = {
          some: { storyTagId }
        }
      }
  
      if (isAdmin) {
        filters.status = true
      } else {
        filters.status = {
          notIn: [Status.ModerateAuto, Status.ModerateByAdmin]
        }
      }

      const stories: StoryWithRelations[] = await prisma.story.findMany({
        where: filters,
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
          }
        },
        skip: page * 5,
        take: 5,
        orderBy: { createdAt: 'desc' }
      })
      const results = stories.map(story => { return toStoryDto(story) })
      return reply.code(STANDARD.OK.statusCode).send(results)
    } catch (error) {
      console.error(error)
      return handleServerError(reply, error)
    }
  },
  getById: async (
    request: FastifyRequest<{ Params: { id: string } }>,
    reply: FastifyReply
  ) => {
    const storyId = Number(request.params.id)
    const currentUserId = request['authUser'].id
    if (!currentUserId) {
      return reply
      .code(ERRORS.userNotExists.statusCode)
      .send(ERRORS.userNotExists.message)
    }
    const story: StoryWithRelations = await prisma.story.findUnique({
      where: { id: storyId },
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
        }
      },
    })
    if (story == null) {
      return reply
        .code(ERRORS.NotFound.statusCode)
        .send(ERRORS.NotFound.message)
    }
    return reply.code(STANDARD.OK.statusCode).send(toStoryDto(story))
  }
}