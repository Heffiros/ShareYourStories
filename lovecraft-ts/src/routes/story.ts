import { FastifyInstance } from 'fastify'
import { storyController } from '../controllers/v1/storyController'
import { checkValidRequest, checkValidUser } from '../helpers/auth.helper'

export default async function (server: FastifyInstance) {
  server.get('/', {
    preHandler: [checkValidRequest, checkValidUser],
  },
  storyController.getAll)
  server.get(
    '/:id',
    {
      preHandler: [checkValidRequest, checkValidUser],
    }, 
    storyController.getById)
  server.post(
    '/', 
    {
      preHandler: [checkValidRequest, checkValidUser],
    },
    storyController.post
  )
}
