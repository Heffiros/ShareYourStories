import { FastifyReply, FastifyRequest } from 'fastify'
import { prisma, utils } from '../../utils'
import { ERRORS, handleServerError } from '../../helpers/errors.helper'
import { IUserDto } from '../../schemas/User'
import { STANDARD } from '../../constants/request'

export const userController = {

  getAll : async () => {

  },
  getById: async (
    request: FastifyRequest<{ Params: { id: string } }>,
    reply: FastifyReply
  ) => {
    try {
      const paramId = request.params.id
      const id = paramId === "me" ? request['authUser'].id : Number(paramId)

      const user = await prisma.user.findUnique({
        where: { id: id }, // attention, id en base est un Int
      })
  
      if (!user) {
        return reply
        .code(ERRORS.userNotExists.statusCode)
        .send(ERRORS.userNotExists.message)
      }
      user.password = ''
      return reply.code(STANDARD.OK.statusCode).send({user})
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
    
  },
}