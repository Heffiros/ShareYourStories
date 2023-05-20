export const state = () => ({
  events: []
})

export const getters = {
  getEventById: (state) => (id) => {
    return state.events.find(m => m.id === id)
  }
}

export const mutations = {
  SET_EVENTS (state, events) {
    state.events = state.events.concat(events)
  },
  SET_EVENT (state, event) {
    state.events = state.events.concat(event)
  },
  RESET_EVENTS (state) {
    state.events = []
  }
}

export const actions = {
  async FETCH_EVENTS (store, params) {
    const result = await this.$axios.get('events', { params: { page: params.page } })
    store.commit('SET_EVENTS', result.data)
  },
  async FETCH_EVENT (store, params) {
    const result = await this.$axios.get('events/' + params.eventId)
    store.commit('SET_EVENT', result.data)
  },
  async RESET_EVENTS (store) {
    store.commit('RESET_STORIES')
  }
}
