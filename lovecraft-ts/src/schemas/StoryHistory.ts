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

export type IPostStoryHistoryDto = Omit<IStoryHistoryDto, "id" | "createdAt" | "updatedAt">
export type IPutStoryHistoryDto = Omit<IStoryHistoryDto, "createdAt" | "updatedAt">
