import { EventWithRelations } from 'src/schemas/Event'
import { IEventDto } from "../schemas/Event"

export const toEventDto = (event: EventWithRelations, userIdClaim: Number): IEventDto => ({
  id: event.id,
  title: event.title,
  rules: event.rules,
  coverUrl: event.coverUrl,
  dateBegin: event.dateBegin,
  dateEnd: event.dateEnd,
  hasAlreadyParticipate: userIdClaim != null && event.stories.some(s => s.userId === Number(userIdClaim)),
  createdAt: event.createdAt,
  updatedAt: event.updatedAt
})