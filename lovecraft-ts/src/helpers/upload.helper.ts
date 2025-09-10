import { PutObjectCommand, S3Client } from '@aws-sdk/client-s3'

const s3 = new S3Client({
  region: process.env.AWS_REGION || 'eu-west-3',
  credentials: {
    accessKeyId: process.env.AWS_ACCESS_KEY_ID!,
    secretAccessKey: process.env.AWS_SECRET_ACCESS_KEY!
  }
})

interface ImageInfo {
  name: string,
  stream: Buffer,
  mimeType: string,
  extension: string,
  place: string,
  size: number
}

export async function uploadToS3(image: ImageInfo) {
  const key = `wonderland/${image.place.toLowerCase()}/${image.name}${image.extension}`

  try {
    const command = new PutObjectCommand({
      Bucket: 's3.sharedyourstories',
      Key: key,
      Body: image.stream,
      ContentType: image.mimeType,
      ContentLength: image.size,
      ACL: 'private'
    })

    await s3.send(command)

    return {
      statusCode: 200,
      url: `https://s3.eu-west-3.amazonaws.com/s3.sharedyourstories/${key}`
    }
  } catch (err: any) {
    return {
      statusCode: 500,
      message: err.message
    }
  }
}
