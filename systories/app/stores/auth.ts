import { defineStore } from 'pinia'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    user: null as null | { id: number; email: string },
    token: null as string | null,
  }),

  actions: {
    async login(credentials: { email: string; password: string }) {
      const config = useRuntimeConfig()

      try {
        const response = await $fetch<{ user: { id: number; email: string }, token: string }>('/auth/login', {
          method: 'POST',
          baseURL: config.public.apiBase,
          body: credentials,
        })

        // On récupère le token et le user
        this.user = response.user
        this.token = response.token

        // On stocke le token (simple pour commencer)
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
      // Si déjà connecté, rien à faire
      if (this.token) return

      const config = useRuntimeConfig()
      try {
        // Login automatique avec paramètres en dur
        const response = await $fetch<{ user: { id: number; email: string }, token: string }>('/auth/login', {
          method: 'POST',
          baseURL: config.public.apiBase,
          body: {
            email: 'user@example.com',
            password: 'Password123',
          },
          headers: { 'Content-Type': 'application/json' }
        })
        console.log('response authoLogin', response)
        this.user = response.user
        this.token = response.token
        localStorage.setItem('token', this.token)
        console.log('✅ Auto login OK', this.user)
      } catch (err) {
        console.error('❌ Auto login failed:', err)
      }
    },
    logout() {
      this.user = null
      this.token = null
      localStorage.removeItem('token')
    },

    // Au démarrage, restaure le token
    init() {
      const token = localStorage.getItem('token')
      if (token) this.token = token
    },
  },
})
