<template>
   <header
      class="h-24 border-b border-slate-200 dark:border-slate-600 bg-slate-100 dark:bg-slate-800 flex items-center justify-between px-6 transition-colors duration-300">

      <div class="flex items-center gap-4">
         <CommonAppButton :iconName="Menu" @click="toggleMenu" />
      </div>

      <div class="flex-1 max-w-2xl mx-8">
         <div class="relative">
            <div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
               <Search class="h-4 w-4 text-slate-400" />
            </div>
            <input v-model="searchQuery" type="text" placeholder="Rechercher des histoires, auteurs et tags..."
               class="block w-full pl-10 pr-4 py-2.5 border border-slate-400 dark:border-slate-500 rounded-lg bg-slate-100 dark:bg-slate-700 text-slate-900 dark:text-slate-100 placeholder-slate-500 dark:placeholder-slate-400 focus:outline-none focus:ring-2 focus:ring-yellow-500 focus:border-transparent transition-colors duration-200"
               @keyup.enter="handleSearch" />
         </div>
      </div>

      <div class="flex items-center gap-2">
         <CommonAppButton :iconName="isDark ? Sun : Moon" @click="toggleTheme" />
         <CommonAppButton :iconName="MessageSquare" :badgeCount="2" @click="handleMessages" />
         <CommonAppButton :iconName="Bell" :badgeCount="notificationCount" @click="handleNotifications" />
      </div>
   </header>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import {
   Menu,
   Search,
   Sun,
   Moon,
   Settings,
   Bell,
   MessageSquare,
   User
} from 'lucide-vue-next'

const props = defineProps({
   notificationCount: { type: Number, default: 0 },
   messageCount: { type: Number, default: 0 },
})

// Emits
const emit = defineEmits(['toggle', 'search', 'notifications', 'messages'])

// Composables
const { isDark, toggleTheme } = useTheme()

// État local
const searchQuery = ref('')

// Méthodes
const toggleMenu = () => {
   emit('toggle')
}

const handleSearch = () => {
   if (searchQuery.value.trim()) {
      emit('search', searchQuery.value.trim())
   }
}

const handleNotifications = () => {
   emit('notifications')
}

const handleMessages = () => {
   emit('messages')
}

</script>

<style scoped>
.search-input:focus {
   box-shadow: 0 0 0 3px rgba(234, 179, 8, 0.1);
}
</style>