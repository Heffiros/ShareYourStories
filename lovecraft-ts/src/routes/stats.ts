import { FastifyInstance } from 'fastify'
import { statsController } from '../controllers/v1/statsController'
import { checkValidRequest, checkValidUser } from '../helpers/auth.helper'

export default async function (server: FastifyInstance) {
  server.get('/stories', {
    preHandler: [checkValidRequest, checkValidUser],
  },
    statsController.getStoriesStats)
}