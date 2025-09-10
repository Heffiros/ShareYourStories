import { Prisma, Status } from "@prisma/client"
import { IUserDto } from "./User"

export interface IStoryCommentDto {
  id: number
  text: string
  status: Status
  storyId: number
  userId: number
  user: IUserDto
  createdAt: Date
  updatedAt: Date
}

export type StoryCommentWithRelations = Prisma.StoryCommentGetPayload<{
  include: {
    user: true
  }
}>

export type IPostStoryCommentDto = Omit<IStoryCommentDto, "id" | "createdAt" | "updatedAt">
