import { FastifyInstance } from 'fastify'
import { storyHistoryController } from '../controllers/v1/storyHistoryController'
import { checkValidRequest, checkValidUser } from '../helpers/auth.helper'

export default async function (server: FastifyInstance) {
  server.get('/', {
    preHandler: [checkValidUser],
  },
    storyHistoryController.getAll)
  server.put(
    '/',
    {
      preHandler: [checkValidUser],
    },
    storyHistoryController.put)
  server.post(
    '/',
    {
      preHandler: [checkValidRequest, checkValidUser],
    },
    storyHistoryController.post
  )
}
