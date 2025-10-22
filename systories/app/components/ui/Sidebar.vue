<template>
  <aside
    class="h-screen border-r border-slate-200 dark:border-slate-800 bg-white dark:bg-slate-900 flex flex-col transition-all duration-300"
    :class="full ? 'w-80 items-start' : 'w-16 items-center'">
    <div class="w-full border-b border-slate-200 dark:border-slate-700 p-6">
      <div class="flex items-center gap-4" :class="full ? 'justify-start' : 'justify-center'">
        <div class="flex-shrink-0">
          <img src="~/assets/images/logo.png" :alt="title" :class="full ? 'w-10 h-10' : 'w-8 h-8'"
            class="object-contain" />
        </div>
        <div v-if="full" class="flex flex-col">
          <h1 class="text-xl font-bold text-slate-900 dark:text-slate-100 leading-tight">
            SHARE YOUR STORIES
          </h1>
          <p class="text-sm text-slate-500 dark:text-slate-400 font-medium">
            Plateforme Littéraire
          </p>
        </div>
      </div>
    </div>
    <div v-if="full" class="w-full h-[70px] flex justify-center px-6 py-4">
      <button
        class="w-11/12 bg-gradient-to-r from-white to-yellow-500 hover:from-yellow-50 hover:to-yellow-600 text-slate-900 font-semibold py-3 px-4 rounded-lg transition-all duration-200 flex items-center justify-center gap-2"
        @click="navigateTo('/write')">
        <Edit class="w-5 h-5" />
        <span>Écrire</span>
      </button>
    </div>
    <nav class=" flex flex-col gap-1 w-full px-4 flex-1 overflow-y-auto">
      <template v-for="item in menuItemsCompiled" :key="item.title">
        <div v-if="item.type === 'menu' && full"
          class="px-3 py-2 text-xs font-bold text-slate-400 dark:text-slate-500 uppercase tracking-wider mt-6 first:mt-2">
          {{ item.title }}
        </div>
        <hr v-else-if="item.type === 'divider'" class="border-slate-300 dark:border-slate-600 my-3" />
        <NuxtLink v-else-if="item.type === 'link'" :to="item.to"
          class="flex items-center gap-3 text-slate-600 dark:text-slate-300 hover:text-slate-900 dark:hover:text-white hover:bg-slate-100 dark:hover:bg-slate-700 rounded-lg py-3 px-3 transition-colors group"
          :class="full ? 'justify-start' : 'justify-center'">
          <component v-if="item.icon" :is="icons[item.icon]" class="w-5 h-5 flex-shrink-0" />
          <span v-if="full" class="text-sm font-medium">{{ item.title }}</span>
        </NuxtLink>
      </template>
    </nav>
  </aside>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue'
import { useAuthStore } from '~/stores/auth'
import { Home, User, Library, ShieldHalf, FileSliders, Edit } from 'lucide-vue-next'

const title = ref('SharedYourStories')
const icons = { Home, User, Library, ShieldHalf, FileSliders, Edit }
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