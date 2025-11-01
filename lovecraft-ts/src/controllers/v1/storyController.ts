import { MultipartFile, MultipartValue } from '@fastify/multipart'
import { Status } from '@prisma/client'
import { FastifyReply, FastifyRequest } from 'fastify'
import { STANDARD } from '../../constants/request'
import { ERRORS, handleServerError } from '../../helpers/errors.helper'
import { extractRawTextFromDocx, splitIntoPages, toStoryDto } from '../../helpers/story.helper'
import { IStoryDto, StoryWithRelations } from "../../schemas/Story"
import { prisma } from '../../utils'

export const storyController = {
  getAll: async (request: FastifyRequest, reply: FastifyReply) => {
    try {
      const { userId, teamsId, eventId, search, storyTagId, page = 0, order = 'desc', mode } = request.query as {
        userId?: string
        teamsId?: number
        eventId?: number
        search?: string
        storyTagId?: number
        page?: number
        order?: string
        mode?: string
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
        filters.status = 'Online'
      } else if (eventId) {
        filters.eventId = eventId
        filters.status = 'Online'
      } else if (userId && userId !== '') {
        const userIdNumber = Number(userId)
        filters.userId = userIdNumber
        if (userIdNumber === currentUserId) {
          if (mode === 'draft') {
            filters.status = 'Pending'
          }
        } else {
          filters.status = 'Online'
        }
      } else {
        filters.status = 'Online'
      }

      if (search) {
        filters.title = { contains: search, mode: 'insensitive' }
      }

      if (storyTagId) {
        filters.StoryStoryTags = {
          some: { storyTagId }
        }
      }

      const orderBy = { createdAt: order === 'asc' ? 'asc' as const : 'desc' as const }

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
          },
          _count: {
            select: {
              storyComments: true
            }
          }
        },
        skip: page * 5,
        take: 5,
        orderBy
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
        },
        _count: {
          select: {
            storyComments: true
          }
        }
      },
    })
    if (story == null) {
      return reply
        .code(ERRORS.NotFound.statusCode)
        .send(ERRORS.NotFound.message)
    }
    return reply.code(STANDARD.OK.statusCode).send(toStoryDto(story))
  },
  post: async (
    request: FastifyRequest<{ Body: { file: any; story: string } }>,
    reply: FastifyReply,
  ) => {
    try {
      const currentUserId = request['authUser']?.id
      if (!currentUserId) {
        return reply
          .code(ERRORS.userNotExists.statusCode)
          .send(ERRORS.userNotExists.message)
      }
      const parts = await request.parts()

      let fileBuffer: Buffer | null = null
      let fileName: string | null = null
      let mimeType: string | null = null
      let storyToCreate: IStoryDto | null = null
      let docxText: string
      for await (const part of parts) {
        if (part.type === 'file') {
          const filePart = part as MultipartFile
          fileBuffer = await filePart.toBuffer()
          fileName = filePart.filename
          mimeType = filePart.mimetype
          docxText = await extractRawTextFromDocx(fileBuffer)
        } else if (part.type === 'field') {
          const valuePart = part as MultipartValue
          if (valuePart.fieldname === 'story') {
            storyToCreate = JSON.parse(valuePart.value as string)
          }
        }
      }

      if (storyToCreate.eventId) {
        const eventLink = await prisma.event.findUnique({
          where: { id: storyToCreate.eventId }
        })
        if (!eventLink) {
          return reply.code(ERRORS.NotFound.statusCode).send('Event not found')
        }

        const today = new Date()
        if (eventLink.dateBegin > today || eventLink.dateEnd < today) {
          return reply.code(ERRORS.badRequest.statusCode).send('Event is not active')
        }

        const existingStory = await prisma.story.findFirst({
          where: { userId: currentUserId, eventId: storyToCreate.eventId }
        })
        if (existingStory) {
          return reply.code(ERRORS.badRequest.statusCode).send('Already participated in this event')
        }
      }

      const pages = await splitIntoPages(docxText, 250) // 250 mots par page

      const createdStory = await prisma.story.create({
        data: {
          title: storyToCreate.title,
          coverUrl: storyToCreate.coverUrl,
          userId: currentUserId,
          status: Status.Pending,
          eventId: storyToCreate.eventId
        }
      })

      const pagesData = pages.map((content, index) => ({
        storyId: createdStory.id,
        content,
        order: index,
        createdAt: new Date(),
        updatedAt: new Date()
      }))
      await prisma.page.createMany({ data: pagesData })

      if (storyToCreate.storyTags?.length) {
        const tagsData = storyToCreate.storyTags.map((tag: any) => ({
          storyId: createdStory.id,
          storyTagId: tag.id
        }))
        await prisma.storyStoryTag.createMany({ data: tagsData })
      }

      const finalStory: StoryWithRelations = await prisma.story.findUnique({
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
        where: { id: createdStory.id }
      })
      return reply.code(STANDARD.OK.statusCode).send(toStoryDto(finalStory))
    } catch (error) {
      console.error(error)
      return handleServerError(reply, error)
    }
  }
}