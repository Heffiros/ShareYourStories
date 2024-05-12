export const state = () => ({
    badges: []
  })
  
  export const getters = {
  }
  
  export const mutations = {
    SET_BADGES (state, badges) {
      state.badges = state.badges.concat(badges)
    },
    SET_BADGE (state, badge) {
      state.badges = state.badges.concat(badge)
    },
    RESET_BADGES (state) {
      state.events = []
    }
  }
  
  export const actions = {
    async FETCH_BADGES (store, params) {
      const result = await this.$axios.get('badges', { params: { page: params.page,  } })
      store.commit('SET_BADGES', result.data)
    },
    async FETCH_EVENT (store, params) {
      const result = await this.$axios.get('badges/' + params.badgeId)
      store.commit('SET_BADGE', result.data)
    },
    async RESET_BADGES (store) {
      store.commit('RESET_BADGES')
    }
  }
  