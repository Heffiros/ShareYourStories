import { FastifyInstance } from 'fastify'
import { userController } from '../controllers/v1/userController'
import { checkValidRequest, checkValidUser } from '../helpers/auth.helper'

export default async function (server: FastifyInstance) {
  server.get('/', userController.getAll)
  server.get(
    '/:id',
    {
      preHandler: [checkValidRequest, checkValidUser],
    },
    userController.getById)
  server.put(
    '/',
    {
      preHandler: [checkValidRequest, checkValidUser],
    },
    userController.update
  )
}
