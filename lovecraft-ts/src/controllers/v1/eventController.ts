import { FastifyReply, FastifyRequest } from 'fastify'
import { prisma } from '../../utils'
import { ERRORS } from '../../helpers/errors.helper'
import { STANDARD } from '../../constants/request'
import { toEventDto } from '../../helpers/event.helper'
import { IPostEventDto, EventWithRelations } from 'src/schemas/Event'

export const eventController = {
  getAll: async (
    request: FastifyRequest,
    reply: FastifyReply
  ) => {
    const { mode, page = 0 } = request.query as {
      mode?: string,
      page: number
    }
    const today = new Date()
    const currentUserId = request['authUser'].id

    const skip = page * 5
    const take = 5

    const dateFilter =
      mode === 'active'
        ? {
          dateBegin: { lte: today },
          dateEnd: { gte: today },
        }
        : {}

    const events: EventWithRelations[] = await prisma.event.findMany({
      where: dateFilter,
      orderBy: { dateBegin: 'desc' },
      skip,
      take,
      include: {
        stories: true
      },
    })
    const results = events.map(event => { return toEventDto(event, currentUserId) })
    return reply.code(STANDARD.OK.statusCode).send(results)
  },
  getById: async (
    request: FastifyRequest<{ Params: { id: string } }>,
    reply: FastifyReply
  ) => {
    const eventId = Number(request.params.id)
    const currentUserId = request['authUser'].id

    const event: EventWithRelations = await prisma.event.findUnique({
      where: { id: eventId },
      include: {
        stories: true
      },
    })

    if (event == null) {
      return reply
        .code(ERRORS.NotFound.statusCode)
        .send(ERRORS.NotFound.message)
    }
    return reply.code(STANDARD.OK.statusCode).send(toEventDto(event, currentUserId))
  },
  post: async (
    request: FastifyRequest<{
      Body: IPostEventDto
    }>,
    reply: FastifyReply
  ) => {
    const { title, rules, coverUrl, dateBegin, dateEnd } = request.body
    const currentUserId = request['authUser'].id
    const eventCreated: EventWithRelations = await prisma.event.create({
      data: {
        title: title,
        rules: rules,
        coverUrl: coverUrl,
        dateBegin: dateBegin,
        dateEnd: dateEnd,
        createdAt: new Date(),
        updatedAt: new Date(),
      },
      include: {
        stories: true
      }
    })
    return reply.code(STANDARD.OK.statusCode).send(toEventDto(eventCreated, currentUserId))
  }
}