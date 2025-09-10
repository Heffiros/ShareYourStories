import { FastifyInstance } from 'fastify'
import { storyCommentController } from '../controllers/v1/storyCommentController'
import { checkValidRequest, checkValidUser } from '../helpers/auth.helper'

export default async function (server: FastifyInstance) {
  server.get('/', {
    preHandler: [checkValidUser],
  },
    storyCommentController.getAll)
  server.get(
    '/:id',
    {
      preHandler: [checkValidUser],
    },
    storyCommentController.getById)
  server.post(
    '/',
    {
      preHandler: [checkValidRequest, checkValidUser],
    },
    storyCommentController.post
  )
}
