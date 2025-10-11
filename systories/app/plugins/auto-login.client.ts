import { useAuthStore } from '~/stores/auth'

export default defineNuxtPlugin(async () => {
  console.log('Auto login plugin running...')
  const auth = useAuthStore()
  auth.init()
  if (!auth.token) {
    await auth.autoLogin()
  }
})
