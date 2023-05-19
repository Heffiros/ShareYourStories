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
  }
}

export const actions = {
  async FETCH_STORIES (store, params) {
    const result = await this.$axios.get('stories', { params: { userId: params.userId, page: params.page } })
    store.commit('SET_STORIES', result.data)
  }
}
