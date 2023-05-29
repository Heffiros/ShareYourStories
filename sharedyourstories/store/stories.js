export const state = () => ({
  stories: []
})

export const getters = {
  getStoryById: (state) => (id) => {
    return state.stories.find(m => m.id === id)
  }
}

export const mutations = {
  SET_STORIES (state, stories) {
    state.stories = state.stories.concat(stories)
  },
  SET_STORY (state, story) {
    state.stories = state.stories.concat(story)
  },
  RESET_STORIES (state) {
    state.stories = []
  }
}

export const actions = {
  async FETCH_STORIES (store, params) {
    const result = await this.$axios.get('stories', { params: { userId: params.userId, page: params.page } })
    store.commit('SET_STORIES', result.data)
  },
  async FETCH_STORY (store, params) {
    const result = await this.$axios.get('stories/' + params.id)
    store.commit('SET_STORY', result.data)
  },
  async RESET_STORIES (store) {
    store.commit('RESET_STORIES')
  }
}
