import { utils, prisma } from '../utils'
import { FastifyRequest, FastifyReply } from 'fastify'
import { ERRORS } from './errors.helper'
import { request } from 'http'
import user from 'src/routes/user'

export const checkValidRequest = async (
  request: FastifyRequest,
  reply: FastifyReply
) => {
  const token = utils.getTokenFromHeader(request.headers.authorization)
  if (!token) {
    return reply
      .code(ERRORS.unauthorizedAccess.statusCode)
      .send(ERRORS.unauthorizedAccess.message)
  }

  const decoded = utils.verifyToken(token)
  if (!decoded) {
    return reply
      .code(ERRORS.unauthorizedAccess.statusCode)
      .send(ERRORS.unauthorizedAccess.message)
  }

  // IMPORTANT : rien à renvoyer, Fastify continue vers le handler
}

export const checkValidUser = async (
  request: FastifyRequest,
  reply: FastifyReply,
) => {
  const token = utils.getTokenFromHeader(request.headers.authorization)
  if (!token) {
    return reply
      .code(ERRORS.unauthorizedAccess.statusCode)
      .send(ERRORS.unauthorizedAccess.message)
  }

  const decoded = utils.verifyToken(token)
  if (!decoded || !decoded.id) {
    return reply
      .code(ERRORS.unauthorizedAccess.statusCode)
      .send(ERRORS.unauthorizedAccess.message)
  }

  try {
    const userData = await prisma.user.findUnique({
      where: { id: decoded.id },
    })
    if (!userData) {
      return reply
        .code(ERRORS.unauthorizedAccess.statusCode)
        .send(ERRORS.unauthorizedAccess.message)
    }

    request['authUser'] = userData
  } catch (e) {
    return reply
      .code(ERRORS.unauthorizedAccess.statusCode)
      .send(ERRORS.unauthorizedAccess.message)
  }
}

export const checkValidAdmin = async (
  request: FastifyRequest,
  reply: FastifyReply,
) => {
  const token = utils.getTokenFromHeader(request.headers.authorization)
  if (!token) {
    return reply
      .code(ERRORS.unauthorizedAccess.statusCode)
      .send(ERRORS.unauthorizedAccess.message)
  }

  const decoded = utils.verifyToken(token)
  if (!decoded || !decoded.id) {
    return reply
      .code(ERRORS.unauthorizedAccess.statusCode)
      .send(ERRORS.unauthorizedAccess.message)
  }

  try {
    const userData = await prisma.user.findUnique({
      where: { id: decoded.id },
    })
    if (!userData) {
      return reply
        .code(ERRORS.unauthorizedAccess.statusCode)
        .send(ERRORS.unauthorizedAccess.message)
    }
    
    if (!userData.isAdmin) {
      return reply
      .code(ERRORS.unauthorizedAccess.statusCode)
      .send(ERRORS.unauthorizedAccess.message)
    }

    request['authUser'] = userData
  } catch (e) {
    return reply
      .code(ERRORS.unauthorizedAccess.statusCode)
      .send(ERRORS.unauthorizedAccess.message)
  }
}