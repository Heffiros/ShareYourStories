import type { User } from './user'
import type { Event } from './event'
import type { Page } from './page'
import type { StoryHistory } from './storyHistory'
import type { StoryVote } from './storyVote'
import type { StoryTag } from './storyTag'
import type { Team } from './team'
import type { StoryStatus } from './storyStatus'

export interface Story {
  id: number
  title: string
  coverUrl: string | null
  summary: string | null
  status: StoryStatus
  pages: Page[]
  userId: number
  user: User
  eventId: number | null
  event: Event | null
  teamId: number | null
  team: Team | null
  storyHistory: StoryHistory | null
  storyTags: StoryTag[] | null
  storyVotes: StoryVote[]
  commentCount: number
  createdAt: string
  updatedAt: string
}

export interface StoriesResponse {
  stories: Story[]
  total: number
  page: number
  limit: number
}

export interface CreateStoryDto {
  title: string
  coverUrl?: string
  summary?: string
  eventId?: number
  teamId?: number
}

export interface UpdateStoryDto {
  title?: string
  coverUrl?: string
  summary?: string
  status?: StoryStatus
}