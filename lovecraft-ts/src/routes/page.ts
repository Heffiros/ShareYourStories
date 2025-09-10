import { FastifyInstance } from 'fastify'
import { pageController } from '../controllers/v1/pageController'
import { checkValidAdmin, checkValidRequest } from '../helpers/auth.helper'

export default async function (server: FastifyInstance) {
  server.get('/', {
    preHandler: [checkValidRequest],
  },
    pageController.getAll)
  server.get(
    '/:id',
    {
      preHandler: [checkValidRequest],
    },
    pageController.getById)
  server.post(
    '/',
    {
      preHandler: [checkValidRequest, checkValidAdmin],
    },
    pageController.post
  )
}
