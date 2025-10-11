import { defineNuxtPlugin } from '#app'

export default defineNuxtPlugin((nuxtApp) => {
  const config = useRuntimeConfig()

  // Création d'une instance fetch
  const apiFetch = $fetch.create({
    baseURL: config.public.apiBase,
    async onRequest({ options }) {
      // Récupérer le token depuis le store ou localStorage
      const token = localStorage.getItem('token')
      console.log('Attaching token to request:', token)
      if (token) {
        options.headers = {
          ...options.headers,
          Authorization: `Bearer ${token}`,
        }
      }
    },
    onResponseError({ response }) {
      console.error('Erreur API', response.status, response._data)
    },
  })

  // Injecter globalement pour usage comme nuxtApp.$api
  nuxtApp.provide('api', apiFetch)
})
