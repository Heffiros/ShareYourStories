import { IStoryDto, StoryWithRelations } from '../schemas/Story'
import { toUserDto } from '../helpers/user.helper'

export const toStoryDto = (story: StoryWithRelations): IStoryDto => ({
  title: story.title,
  coverUrl: story.coverUrl,
  summary: story.summary,
  status: story.status,
  pages: story.pages,
  userId: story.userId,
  user: toUserDto(story.user),
  eventId: story.eventId,
  event: story.event,
  teamId: story.teamId,
  team: story.team,
  storyHistory: story.storyHistories ? story.storyHistories[0] : null,
  storyVotes: story.storyVotes,
  createdAt: story.createdAt,
  updatedAt: story.updatedAt
})