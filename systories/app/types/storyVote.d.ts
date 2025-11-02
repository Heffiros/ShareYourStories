export interface StoryVote {
  id: number
  userId: number
  storyId: number
  voteType: 'Like' | 'Dislike'
  createdAt: string
  updatedAt: string
}