<template>
  <aside
    class="h-screen border-r border-slate-200 dark:border-slate-800 bg-slate-50 dark:bg-slate-900 flex flex-col pt-4 transition-all duration-300"
    :class="full ? 'w-80 items-start px-2' : 'w-14 items-center'">
    <nav class="flex flex-col gap-2 w-full">
      <NuxtLink v-for="item in menuItemsCompiled" :key="item.title" :to="item.to"
        class="flex items-center gap-3 text-gray-600 dark:text-gray-300 hover:text-gray-900 dark:hover:text-white hover:bg-gray-100 dark:hover:bg-gray-700 rounded p-2 transition-colors"
        :class="full ? 'justify-start' : 'justify-center'">
        <div v-if="item.type === 'menu' && full">
          <h2>{{ item.title }}</h2>
        </div>
        <div v-if="item.type === 'divider'">
          divider
        </div>
        <div v-if="item.type === 'link'"
          :class="{ ['flex items-center gap-2 cursor-pointer']: true, ['menu-full']: full, ['menu-compact']: !full }">
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
  for (const item of items.value) {
    if (!item.hasToBeAuth || item.type === 'menu' || item.type === 'divider') {
      computedMenu.push(item)
    } else if (item.hasToBeAuth && auth.token && !item.hasToBeAdmin) {
      computedMenu.push(item)
    } else if (item.hasToBeAdmin && auth.user?.isAdmin) {
      computedMenu.push(item)
    }
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
.menu-full
  padding-left 24px
</style>