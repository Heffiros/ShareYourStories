import { FastifyReply, FastifyRequest } from 'fastify'
import { prisma } from '../../utils'
import {  handleServerError } from '../../helpers/errors.helper'
import { STANDARD } from '../../constants/request'
import { toPageDto } from '../../helpers/page.helper'
import { Page } from '@prisma/client'
import { IPostPageDto } from 'src/schemas/Page'

export const pageController = {
  getAll: async (request: FastifyRequest, reply: FastifyReply) => {
    
  },
  getById: async (
    request: FastifyRequest<{ Params: { id: string } }>,
    reply: FastifyReply
  ) => {
    
  }
}