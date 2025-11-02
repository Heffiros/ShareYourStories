import { useAuthStore } from '~/stores/auth'

export default defineNuxtPlugin(async () => {
  const auth = useAuthStore()
  const config = useRuntimeConfig()

  auth.init()
  if (!auth.token && config.public.environment === 'development') {
    await auth.autoLogin()
  }
})
