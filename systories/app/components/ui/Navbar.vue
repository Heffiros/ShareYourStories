<template>
   <header
      class="h-18 border-b border-slate-200 dark:border-slate-800 bg-slate-50 dark:bg-slate-900 flex items-center justify-between px-4">
      <div class="w-32 h-8 bg-gray-200 dark:bg-gray-700 rounded">
         <Menu class="icon" @click="toggleMenu"></Menu>
      </div>
      <div class="w-1/2 h-10 bg-gray-200 dark:bg-gray-700 rounded">
      </div>
      <div class="w-32 h-8 bg-gray-200 dark:bg-gray-700 rounded flex items-center justify-between">
         <component :is="isDark ? Sun : Moon" class="w-5 h-5 text-yellow-500" @click="toggleTheme" />
         <MessageSquare class="w-6 h-6 text-yellow-500 icon" />
         <Bell class="w-6 h-6 text-yellow-500 icon" />
      </div>
   </header>
</template>

<script setup>
import { Moon, Sun, MessageSquare, Bell, Menu } from 'lucide-vue-next'
const { isDark, toggleTheme } = useTheme()

const emit = defineEmits(['toggle'])

const toggleMenu = () => {
   emit('toggle', 0)
}
</script>

<style lang="stylus" scoped>
.icon
  cursor pointer
</style>