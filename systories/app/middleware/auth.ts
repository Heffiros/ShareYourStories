import { useAuthStore } from '~/stores/auth'

export default defineNuxtRouteMiddleware((to) => {
  const auth = useAuthStore()
  const config = useRuntimeConfig()

  if (!auth.token && config.public.environment !== 'development') {
    return navigateTo('/auth/login')
  }

  if (auth.token && (to.path === '/auth/login' || to.path === '/auth/register')) {
    return navigateTo('/dashboard')
  }
})
