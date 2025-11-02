import { FastifyReply, FastifyRequest } from 'fastify'
import { uploadToS3 } from '../../helpers/upload.helper'
export const uploadController = {
  post: async (
    request: FastifyRequest<{
      Params: { place: string },
      Body: { file: any }
    }>,
    reply: FastifyReply
  ) => {
    try {
      const place = request.params.place
      const file = await request.file()
      if (!file) {
        return reply.code(400).send('No file provided')
      }

      const imageName = crypto.randomUUID()
      const extension =
        file.mimetype === 'image/jpeg' ? '.jpg' : '.png'

      const imageInfo = {
        name: imageName,
        stream: await file.toBuffer(),
        mimeType: file.mimetype,
        place: place,
        size: file.file.bytesRead,
        extension
      }

      const response = await uploadToS3(imageInfo)
      return reply.code(200).send({ data: response })
    } catch (err) {
      console.error(err)
      return reply.code(500).send(err)
    }
  }

}