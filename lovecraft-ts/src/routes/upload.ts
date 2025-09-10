import { FastifyInstance } from 'fastify'
import { uploadController } from '../controllers/v1/uploadController'
import { checkValidRequest, checkValidUser } from '../helpers/auth.helper'

export default async function (server: FastifyInstance) {
  server.post('/:place', {
    preHandler: [checkValidRequest, checkValidUser],
  },
    uploadController.post)
}