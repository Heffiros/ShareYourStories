import { FastifyInstance } from 'fastify'
import { eventController } from '../controllers/v1/eventController'
import { checkValidAdmin, checkValidRequest, checkValidUser } from '../helpers/auth.helper'

export default async function (server: FastifyInstance) {
  server.get('/', {
    preHandler: [checkValidUser],
  },
  eventController.getAll)
  server.get(
    '/:id',
    {
      preHandler: [checkValidUser],
    }, 
    eventController.getById)
  server.post(
    '/', 
    {
      preHandler: [checkValidRequest, checkValidAdmin],
    },
    eventController.post
  )
}
