import { FastifyInstance } from 'fastify'
import { storyTagController } from '../controllers/v1/storyTagController'

export default async function (server: FastifyInstance) {
  server.get('/', {},
    storyTagController.getAll)
}
