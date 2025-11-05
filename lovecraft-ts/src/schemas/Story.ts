import { Event, Page, Prisma, Status, StoryHistory, StoryTag, StoryVote, Team } from '@prisma/client'
import { IUserDto } from "../schemas/User"

export interface IStoryDto {
  id: number
  title: string
  coverUrl: string
  summary: string
  status: Status
  pages: Page[]
  userId: number
  user: IUserDto
  eventId: number
  event: Event
  teamId: number
  team: Team
  storyVotes: StoryVote[]
  storyTags: StoryTag[]
  storyHistory: StoryHistory | null
  commentCount: number
  createdAt: Date
  updatedAt: Date
}

export type StoryWithRelations = Prisma.StoryGetPayload<{
  include: {
    user: true
    team: true
    event: true
    pages: true
    storyVotes: true
    storyHistories: true
    storyStoryTags: { include: { storyTag: true } }
    _count: {
      select: {
        storyComments: true
      }
    }
  }
}>