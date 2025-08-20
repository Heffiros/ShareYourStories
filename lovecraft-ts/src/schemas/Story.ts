import { Status, Event, Team, StoryVote, StoryHistory, Page } from '@prisma/client'
import { IUserDto } from "../schemas/User";
import { Prisma } from '@prisma/client'

export interface IStoryDto {
  title:           string
  coverUrl:        string
  summary:         string
  status:          Status
  pages:           Page[]       
  userId:          Number
  user:            IUserDto
  eventId:         Number
  event:           Event
  teamId:          Number
  team:            Team
  storyVotes:      StoryVote[]
  storyHistory:  StoryHistory
  createdAt:       Date       
  updatedAt:       Date
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
  }
}>