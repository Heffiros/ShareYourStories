import { FastifyInstance } from 'fastify'
import { feedController } from '../controllers/v1/feedController'
import { checkValidUser } from '../helpers/auth.helper'

export default async function (server: FastifyInstance) {
  server.get('/history', {
    preHandler: [checkValidUser],
  },
    feedController.feedHistory)
  server.get(
    '/ongoing',
    {
      preHandler: [checkValidUser],
    },
    feedController.feedOngoing)
  server.get(
    '/:storyTagId',
    {
      preHandler: [checkValidUser],
    },
    feedController.feedStorysByTagId
  )
}