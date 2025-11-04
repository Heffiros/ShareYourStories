import { HistoryState } from "@prisma/client"

export interface IStoryHistoryDto {
  id: number
  userId: number
  storyId: number
  lastPageReadId: number
  reread: number
  historyStateValue: HistoryState
  createdAt: Date
  updatedAt: Date
}

export type IPostStoryHistoryDto = {
  storyId: number
  lastPageReadId: number
}

export type IPutStoryHistoryDto = {
  storyId: number
  lastPageReadId: number
}
