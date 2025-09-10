import { FastifyReply, FastifyRequest } from 'fastify'
import Joi from 'joi'

export const validateSchema = (schema: Joi.ObjectSchema) => {
  return (
    request: FastifyRequest,
    reply: FastifyReply,
    done: (err?: Error) => void,
  ) => {
    try {
      const { error } = schema.validate(request.body)
      if (error) {
        throw error
      }
      done()
    } catch (error) {
      done(error)
    }
  }
}
