<template>
  <aside class="w-14 border-r border-gray-800 flex flex-col items-center pt-4 gap-4">
    <div class="w-10 h-10 bg-gray-700 rounded"></div>
    <div class="w-10 h-10 bg-gray-700 rounded"></div>
    <div class="w-10 h-10 bg-gray-700 rounded"></div>
    <div class="w-10 h-10 bg-gray-700 rounded"></div>
  </aside>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue'
import { useAuthStore } from '~/stores/auth'

const clipped = ref(false)
const drawer = ref(true)
const fixed = ref(false)
const miniVariant = ref(false)
const title = ref('SharedYourStories')

interface MenuItem {
  icon: string
  title: string
  hasToBeAuth: boolean
  hasToBeAdmin: boolean
  to: string
}

const items = ref<MenuItem[]>([
  {
    icon: 'mdi-apps',
    title: 'Dashboard',
    hasToBeAuth: false,
    hasToBeAdmin: false,
    to: '/app/dashboard'
  },
  {
    icon: 'mdi-book-open-variant',
    title: 'Ma Bibliothèque',
    hasToBeAuth: true,
    hasToBeAdmin: false,
    to: '/app/library'
  },
  {
    icon: 'mdi-account',
    title: 'Mon Profil',
    hasToBeAuth: true,
    hasToBeAdmin: false,
    to: '/app/user/me'
  },
  {
    icon: 'mdi-account',
    title: 'Admin',
    hasToBeAuth: true,
    hasToBeAdmin: true,
    to: '/app/admin'
  }
])

const auth = useAuthStore()
const menuItemsCompiled = computed(() => {
  const computedMenu: MenuItem[] = []
  computedMenu.push(...items.value.filter(i => !i.hasToBeAuth))
  if (auth.token) {
    computedMenu.push(...items.value.filter(i => i.hasToBeAuth && !i.hasToBeAdmin))
  }
  if (auth.user?.isAdmin) {
    computedMenu.push(...items.value.filter(i => i.hasToBeAdmin))
  }
  return computedMenu
})
const isAuth = computed(() => !!auth.token)
const isAdmin = computed(() => !!auth.user?.isAdmin)

const logout = async () => {
  try {
    await auth.logout()
  } catch (error) {
    console.error('Logout error:', error)
  }
}
</script>