import { defineStore } from 'pinia'
import type { Story } from '~/types/story'

interface StoryState {
  stories: Story[]
  currentPage: number
  hasFinished: boolean
  isLoading: boolean
  searchTerm: string
  sortOrder: 'asc' | 'desc'
  mode: string | null
}

export const useStoryStore = defineStore('story', {
  state: (): StoryState => ({
    stories: [],
    currentPage: 0,
    hasFinished: false,
    isLoading: false,
    searchTerm: '',
    sortOrder: 'desc',
    mode: null
  }),

  getters: {
    filteredStories: (state) => state.stories,
    canLoadMore: (state) => !state.hasFinished && !state.isLoading
  },

  actions: {
    resetStories() {
      this.stories = []
      this.currentPage = 0
      this.hasFinished = false
      this.isLoading = false
    },

    updateFilters(filters: { searchTerm?: string; sortOrder?: 'asc' | 'desc'; mode?: string | null }) {
      if (filters.searchTerm !== undefined) this.searchTerm = filters.searchTerm
      if (filters.sortOrder !== undefined) this.sortOrder = filters.sortOrder
      if (filters.mode !== undefined) this.mode = filters.mode
      this.resetStories()
    },

    async loadStories(userId?: number) {
      if (this.isLoading || this.hasFinished) return []

      try {
        this.isLoading = true
        const { useAuthStore } = await import('~/stores/auth')
        const auth = useAuthStore()
        const config = useRuntimeConfig()

        const params = new URLSearchParams({
          page: this.currentPage.toString(),
          order: this.sortOrder
        })

        if (userId && auth.user?.id) {
          params.append('userId', userId.toString())
        } else if (auth.user?.id) {
          params.append('userId', auth.user.id.toString())
        }

        if (this.searchTerm) {
          params.append('search', this.searchTerm)
        }

        if (this.mode) {
          params.append('mode', this.mode)
        }

        const response = await $fetch<Story[]>(`/stories?${params.toString()}`, {
          method: 'GET',
          baseURL: config.public.apiBase,
          headers: { Authorization: `Bearer ${auth.token}` }
        })

        if (response && response.length > 0) {
          this.stories.push(...response)
          this.currentPage++
          return response
        } else {
          this.hasFinished = true
          return []
        }
      } catch (error) {
        console.error('Error loading stories:', error)
        throw error
      } finally {
        this.isLoading = false
      }
    },

    async getStoryById(id: number): Promise<Story | null> {
      const existingStory = this.stories.find(story => story.id === id)
      if (existingStory) {
        return existingStory
      }

      try {
        const { useAuthStore } = await import('~/stores/auth')
        const auth = useAuthStore()
        const config = useRuntimeConfig()

        const story = await $fetch<Story>(`/stories/${id}`, {
          method: 'GET',
          baseURL: config.public.apiBase,
          headers: { Authorization: `Bearer ${auth.token}` }
        })

        return story
      } catch (error) {
        console.error('Error loading story:', error)
        return null
      }
    },

    async updateStoryHistory(storyId: number, pageId: number) {
      try {
        const { useAuthStore } = await import('~/stores/auth')
        const auth = useAuthStore()
        const config = useRuntimeConfig()

        const story = await this.getStoryById(storyId)
        if (!story?.storyHistory?.id) {
          throw new Error('Story history not found')
        }

        const response = await $fetch(`/storyHistory`, {
          method: 'PUT',
          baseURL: config.public.apiBase,
          headers: { Authorization: `Bearer ${auth.token}` },
          body: {
            id: story.storyHistory.id,
            userId: story.storyHistory.userId,
            storyId: story.storyHistory.storyId,
            lastPageReadId: pageId,
            reread: story.storyHistory.reread,
            historyStateValue: story.storyHistory.historyStateValue
          }
        })

        const storyIndex = this.stories.findIndex(story => story.id === storyId)
        if (storyIndex !== -1) {
          const story = this.stories[storyIndex]
          if (story?.storyHistory) {
            story.storyHistory.lastPageReadId = pageId
            story.storyHistory.updatedAt = new Date().toISOString()
          }
        }

        return response
      } catch (error) {
        console.error('Error updating story history:', error)
        throw error
      }
    }
  }
})