import type { User } from './user'
import type { Event } from './event'
import type { Page } from './page'
import type { StoryHistory } from './storyHistory'
import type { StoryVote } from './storyVote'
import type { StoryTag } from './storyTag'
import type { Team } from './team'

export interface Story {
  id: number
  title: string
  coverUrl: string | null
  summary: string | null
  status: 'Pending' | 'Active' | 'Completed' | 'Rejected'
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
  createdAt: string
  updatedAt: string
}

// Types pour les réponses API
export interface StoriesResponse {
  stories: Story[]
  total: number
  page: number
  limit: number
}

// Types pour la création d'une nouvelle histoire
export interface CreateStoryDto {
  title: string
  coverUrl?: string
  summary?: string
  eventId?: number
  teamId?: number
}

// Types pour la mise à jour d'une histoire
export interface UpdateStoryDto {
  title?: string
  coverUrl?: string
  summary?: string
  status?: 'Pending' | 'Active' | 'Completed' | 'Rejected'
}