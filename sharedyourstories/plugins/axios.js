import axios from 'axios'

export default function ({ $axios, store, app }) {
  $axios.interceptors.request.use((config) => {
    const token = store.state.auth.token
    if (token) {
      config.headers.common.Authorization = `Bearer ${token}`
    }
    return config
  })
}

