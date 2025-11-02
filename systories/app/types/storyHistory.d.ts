export interface StoryHistory {
  id: number
  userId: number
  storyId: number
  lastPageReadId: number
  reread: number
  historyStateValue: 'Reading' | 'Completed' | 'Paused'
  createdAt: string
  updatedAt: string
}