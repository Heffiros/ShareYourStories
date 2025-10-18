<template>
  <aside class="h-screen border-r border-gray-800 flex flex-col pt-4 transition-all duration-300"
    :class="full ? 'w-80 items-start px-2' : 'w-14 items-center'">
    <nav class="flex flex-col gap-2 w-full">
      <NuxtLink v-for="item in items" :key="item.title" :to="item.to"
        class="flex items-center gap-3 text-gray-300 hover:text-white hover:bg-gray-700 rounded p-2 transition-colors"
        :class="full ? 'justify-start' : 'justify-center'">
        <div v-if="item.type === 'menu' && full">
          <h2>{{ item.title }}</h2>
        </div>
        <div v-if="item.type === 'divider'">
          toto
        </div>
        <div v-if="item.type === 'link'" class="menu flex items-center gap-2">
          <component v-if="item.icon" :is="icons[item.icon]" class="w-6 h-6" />
          <span v-else class="w-6 h-6"></span>
          <span v-if="full">{{ item.title }}</span>
        </div>
      </NuxtLink>
    </nav>
  </aside>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue'
import { useAuthStore } from '~/stores/auth'
import { Home, User, Library, ShieldHalf, FileSliders } from 'lucide-vue-next'

const title = ref('SharedYourStories')
const icons = { Home, User, Library, ShieldHalf, FileSliders }
type IconName = keyof typeof icons
const props = defineProps({
  full: Boolean
})

const items = ref<MenuItem[]>([
  {
    title: 'NAVIGATION',
    type: 'menu'
  },
  {
    icon: 'Home',
    title: 'Accueil',
    hasToBeAuth: false,
    hasToBeAdmin: false,
    to: '/',
    type: 'link'
  },
  { title: 'DIVIDER', type: 'divider' },
  {
    title: 'ECRITURE',
    type: 'menu'
  },
  {
    icon: 'Library',
    title: 'Ma bibliothèque',
    hasToBeAuth: true,
    hasToBeAdmin: false,
    to: '/library',
    type: 'link'
  },
  {
    icon: 'FileSliders',
    title: 'Mes brouillons',
    hasToBeAuth: true,
    hasToBeAdmin: false,
    to: '/library?filter=draft',
    type: 'link'
  },
  {
    title: 'COMPTE',
    type: 'menu'
  },
  {
    icon: 'User',
    title: 'Mon Profil',
    hasToBeAuth: true,
    hasToBeAdmin: false,
    to: '/me',
    type: 'link'
  },
  {
    icon: 'ShieldHalf',
    title: 'Admin',
    hasToBeAuth: true,
    hasToBeAdmin: true,
    to: '/admin',
    type: 'link'
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

interface MenuItem {
  icon?: IconName
  title?: string
  hasToBeAuth?: boolean
  hasToBeAdmin?: boolean
  to?: string
  type: 'link' | 'button' | 'divider' | 'menu'
}
</script>

<style lang="stylus" scoped>
.menu
  cursor pointer
  padding-left 24px
  color red
  
</style>