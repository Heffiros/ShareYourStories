import { FastifyReply, FastifyRequest } from 'fastify'
import { storyCommentController } from '../src/controllers/v1/storyCommentController'
import { ERRORS } from '../src/helpers/errors.helper'
import * as storyCommentHelper from '../src/helpers/storyComment.helper'
import { IPostStoryCommentDto } from '../src/schemas/StoryComment'
import { prisma } from '../src/utils'

// ---------------- Mock Prisma ----------------
jest.mock('../src/utils', () => ({
  prisma: {
    storyComment: {
      findMany: jest.fn(),
      findUnique: jest.fn(),
      create: jest.fn()
    },
    story: {
      findUnique: jest.fn()
    }
  }
}))

// ---------------- Mock FastifyReply ----------------
const mockReply = () => {
  const reply: Partial<FastifyReply> = {}
  reply.send = jest.fn().mockReturnValue(reply)
  reply.code = jest.fn().mockReturnValue(reply)
  return reply as FastifyReply
}

// ---------------- Mock toStoryCommentDto ----------------
jest.spyOn(storyCommentHelper, 'toStoryCommentDto').mockImplementation((x) => x)

describe('storyCommentController', () => {
  afterEach(() => {
    jest.clearAllMocks()
  })

  describe('getAll', () => {
    it('should return mapped results', async () => {
      const req = { query: { storyId: 1, page: 0 } } as FastifyRequest
      const reply = mockReply()

      const fakeComments = [{ id: 1, text: 'hi', user: {} }]
        ; (prisma.storyComment.findMany as jest.Mock).mockResolvedValue(fakeComments)

      await storyCommentController.getAll(req, reply)

      expect(prisma.storyComment.findMany).toHaveBeenCalledWith({
        where: { storyId: 1 },
        skip: 0,
        take: 10,
        include: { user: true },
        orderBy: { createdAt: 'desc' }
      })
      expect(reply.send).toHaveBeenCalledWith(fakeComments)
    })
  })

  describe('getById', () => {
    it('should return comment if found', async () => {
      const req = { params: { id: '1' } } as FastifyRequest<{ Params: { id: string } }>
      const reply = mockReply()

      const fakeComment = { id: 1, text: 'hi', user: {} }
        ; (prisma.storyComment.findUnique as jest.Mock).mockResolvedValue(fakeComment)

      await storyCommentController.getById(req, reply)

      expect(prisma.storyComment.findUnique).toHaveBeenCalledWith({
        where: { id: 1 },
        include: { user: true }
      })
      expect(reply.send).toHaveBeenCalledWith(fakeComment)
    })

    it('should return 404 if not found', async () => {
      const req = { params: { id: '1' } } as FastifyRequest<{ Params: { id: string } }>
      const reply = mockReply()

        ; (prisma.storyComment.findUnique as jest.Mock).mockResolvedValue(null)

      await storyCommentController.getById(req, reply)

      expect(reply.code).toHaveBeenCalledWith(ERRORS.NotFound.statusCode)
      expect(reply.send).toHaveBeenCalledWith(ERRORS.NotFound.message)
    })
  })
  // ---------------- Helper ----------------
  function mockPostRequest<T>(body: T, authUserId: number) {
    return { body, authUser: { id: authUserId } } as unknown as FastifyRequest<{ Body: T }>
  }

  // ---------------- Tests post ----------------
  describe('post', () => {
    it('should create comment if story exists', async () => {
      const req = mockPostRequest({
        text: 'hi',
        status: 'Online',
        storyId: 1
      } as IPostStoryCommentDto, 1)

      const reply = mockReply()

      const fakeStory = { id: 1 }
      const fakeComment = { id: 10, text: 'hi', user: {} }

        ; (prisma.story.findUnique as jest.Mock).mockResolvedValue(fakeStory)
        ; (prisma.storyComment.create as jest.Mock).mockResolvedValue(fakeComment)

      await storyCommentController.post(req, reply)

      expect(prisma.storyComment.create).toHaveBeenCalledWith({
        data: {
          text: 'hi',
          status: 'Online',
          storyId: 1,
          userId: 1
        },
        include: { user: true }
      })

      expect(reply.send).toHaveBeenCalledWith(fakeComment)
    })

    it('should return 404 if story not found', async () => {
      const req = mockPostRequest({
        text: 'hi',
        status: 'Online',
        storyId: 1
      } as IPostStoryCommentDto, 1)

      const reply = mockReply()

        ; (prisma.story.findUnique as jest.Mock).mockResolvedValue(null)

      await storyCommentController.post(req, reply)

      expect(reply.code).toHaveBeenCalledWith(ERRORS.NotFound.statusCode)
      expect(reply.send).toHaveBeenCalledWith(ERRORS.NotFound.message)
    })
  })

})
