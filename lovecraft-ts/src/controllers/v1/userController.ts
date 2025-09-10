import { FastifyReply, FastifyRequest } from 'fastify'
import { STANDARD } from '../../constants/request'
import { ERRORS, handleServerError } from '../../helpers/errors.helper'
import { toUserDto } from '../../helpers/user.helper'
import { IUserDto } from '../../schemas/User'
import { prisma } from '../../utils'
export const userController = {

  getAll: async () => {

  },
  getById: async (
    request: FastifyRequest<{ Params: { id: string } }>,
    reply: FastifyReply
  ) => {
    try {
      const paramId = request.params.id
      const id = paramId === "me" ? request['authUser'].id : Number(paramId)

      const user = await prisma.user.findUnique({
        where: { id: id },
      })

      if (!user) {
        return reply
          .code(ERRORS.userNotExists.statusCode)
          .send(ERRORS.userNotExists.message)
      }
      return reply.code(STANDARD.OK.statusCode).send({ data: toUserDto(user) })
    } catch (err) {
      return handleServerError(reply, err)
    }
  },

  update: async (
    request: FastifyRequest<{
      Body: IUserDto
    }>,
    reply: FastifyReply,
  ) => {
    const currentUserId = request['authUser'].id
    const userToUpdate = await prisma.user.findUnique({ where: { id: currentUserId } })
    const body = request.body

    if (!userToUpdate) {
      return reply
        .code(ERRORS.userNotExists.statusCode)
        .send(ERRORS.userNotExists.message)
    }

    if (!userToUpdate.isAdmin && body.isAdmin) {
      return reply
        .code(ERRORS.permissionDenied.statusCode)
        .send(ERRORS.permissionDenied.message)
    }

    const updatedUser = await prisma.user.update({
      where: { id: currentUserId },
      data: {
        authorName: body.authorName,
        email: body.email,
        profilePictureUrl: body.profilePictureUrl,
        birthDate: body.birthDate,
        createdAt: body.createdAt,
        updatedAt: new Date()
      }
    })
    return reply
      .code(STANDARD.OK.statusCode)
      .send({ data: toUserDto(updatedUser) })
  },


}