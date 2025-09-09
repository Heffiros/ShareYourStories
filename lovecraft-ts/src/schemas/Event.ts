import { Prisma } from '@prisma/client'

export interface IEventDto {
  id:               number
  title:            string
  rules:            string
  coverUrl:         string
  dateBegin:        Date
  dateEnd:          Date
  hasAlreadyParticipate: Boolean
  createdAt:        Date
  updatedAt:        Date
}

export interface IPostEventDto {
  title:            string
  rules:            string
  coverUrl:         string
  dateBegin:        Date
  dateEnd:          Date
}

export type EventWithRelations = Prisma.EventGetPayload<{
  include: {
    stories: true
  }
}>