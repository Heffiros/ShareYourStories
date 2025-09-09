import { FastifyReply, FastifyRequest } from 'fastify'
import { prisma, utils } from '../../utils'
import { ERRORS, handleServerError } from '../../helpers/errors.helper'
import * as JWT from 'jsonwebtoken'
import { STANDARD } from '../../constants/request'
import { IUserLoginDto, IUserSignupDto } from '../../schemas/User'

export const authController = {
  login : async (
    request: FastifyRequest<{
      Body: IUserLoginDto
    }>,
    reply: FastifyReply,
  ) => {
    try {
      const { email, password } = request.body
      const user = await prisma.user.findUnique({ where: { email } })
      if (!user) {
        return reply
          .code(ERRORS.userNotExists.statusCode)
          .send(ERRORS.userNotExists.message)
      }
  
      const checkPass = await utils.compareHash(user.password, password)
      if (!checkPass) {
        return reply
          .code(ERRORS.userCredError.statusCode)
          .send(ERRORS.userCredError.message)
      }
  
      const token = JWT.sign(
        {
          id: user.id,
          email: user.email,
        },
        process.env.APP_JWT_SECRET as string,
      )
      user.password = ''
      return reply.code(STANDARD.OK.statusCode).send({
        token,
        user,
      })
    } catch (err) {
      return handleServerError(reply, err)
    }
  },
  register: async (
    request: FastifyRequest<{
      Body: IUserSignupDto
    }>,
    reply: FastifyReply,
  ) => {
    try {
      const { email, password, authorName, } = request.body
      const user = await prisma.user.findUnique({ where: { email } })
      if (user) {
        return reply.code(ERRORS.userExists.statusCode).send(ERRORS.userExists)
      }
  
      const hashPass = await utils.genSalt(10, password)
      const createUser = await prisma.user.create({
        data: {
          email,
          authorName: authorName.trim(),
          password: String(hashPass),
        },
      })
  
      const token = JWT.sign(
        {
          id: createUser.id,
          email: createUser.email,
        },
        process.env.APP_JWT_SECRET as string,
      )
  
      createUser.password = ''
  
      return reply.code(STANDARD.OK.statusCode).send({
        token,
        user: createUser,
      })
    } catch (err) {
      return handleServerError(reply, err)
    }
  }  
} 

