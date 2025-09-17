import { StoryHistory } from "@prisma/client"
import { IStoryHistoryDto } from "../schemas/StoryHistory"

export const toStoryHistoryDto = (storyHistory: StoryHistory): IStoryHistoryDto => ({
  id: storyHistory.id,
  userId: storyHistory.userId,
  storyId: storyHistory.storyId,
  lastPageReadId: storyHistory.lastPageReadId,
  reread: storyHistory.reread,
  historyStateValue: storyHistory.historyStateValue,
  createdAt: storyHistory.createdAt,
  updatedAt: storyHistory.updatedAt
})