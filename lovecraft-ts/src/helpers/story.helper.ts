import { Story } from "@prisma/client";
import { IStoryDto, StoryWithRelations } from '../schemas/Story'

export const toStoryDto = (story: StoryWithRelations): IStoryDto => ({
  title: story.title,
  coverUrl: story.coverUrl,
  summary: story.summary,
  status: story.status,
  pages: story.pages,
  userId: story.userId,
  user: story.user,
  eventId: story.eventId,
  event: story.event,
  teamId: story.teamId,
  team: story.team,
  storyHistories: story.storyHistories,
  storyVotes: story.storyVotes,
  createdAt: story.createdAt,
  updatedAt: story.updatedAt
})