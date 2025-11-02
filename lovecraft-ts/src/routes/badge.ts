import { FastifyInstance } from 'fastify'
import { badgeController } from '../controllers/v1/badgeController'
import { checkValidAdmin, checkValidRequest } from '../helpers/auth.helper'

export default async function (server: FastifyInstance) {
  server.get('/', {
    preHandler: [checkValidRequest],
  },
    badgeController.getAll)
  server.get(
    '/:id',
    {
      preHandler: [checkValidRequest],
    },
    badgeController.getById)
  server.post(
    '/',
    {
      preHandler: [checkValidRequest, checkValidAdmin],
    },
    badgeController.post
  )
}
