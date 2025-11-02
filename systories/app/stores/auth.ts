import { defineStore } from 'pinia'
import type { LoginCredentials, LoginResponse, User } from '~/types/user'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    user: null as null | User,
    token: null as string | null,
  }),

  actions: {
    async login(credentials: LoginCredentials) {
      const config = useRuntimeConfig()

      try {
        const response = await $fetch<LoginResponse>('/auth/login', {
          method: 'POST',
          baseURL: config.public.apiBase,
          body: credentials,
        })
        this.user = response.user
        this.token = response.token

        if (this.token !== null) {
          localStorage.setItem('token', this.token)
        }
        return true
      } catch (err: any) {
        console.error('Erreur login:', err)
        return false
      }
    },
    async autoLogin() {
      if (this.token) return
      const config = useRuntimeConfig()
      try {
        const response = await $fetch<LoginResponse>('/auth/login', {
          method: 'POST',
          baseURL: config.public.apiBase,
          body: {
            email: config.public.autoLoginEmail,
            password: config.public.autoLoginPassword,
          },
          headers: { 'Content-Type': 'application/json' }
        })
        this.user = response.user
        this.token = response.token
        localStorage.setItem('token', this.token)
      } catch (err) {
        console.error('❌ Auto login failed:', err)
      }
    },
    async fetchCurrentUser() {
      if (!this.token) return
      const config = useRuntimeConfig()
      try {
        const response = await $fetch<User>('/users/me', {
          method: 'GET',
          baseURL: config.public.apiBase,
          headers: { Authorization: `Bearer ${this.token}` }
        })
        this.user = response
      } catch (err) {
        console.error('Erreur fetchCurrentUser:', err)
      }
    },
    logout() {
      this.user = null
      this.token = null
      localStorage.removeItem('token')
    },
    init() {
      const token = localStorage.getItem('token')
      if (token) {
        this.token = token
        this.fetchCurrentUser()
      }
    },
  },
})
