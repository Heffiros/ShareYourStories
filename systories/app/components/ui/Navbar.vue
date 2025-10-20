<template>
   <header
      class="h-18 border-b border-slate-200 dark:border-slate-800 bg-slate-50 dark:bg-slate-900 flex items-center justify-between px-4">
      <div class="w-32 h-8 rounded flex items-center justify-between">
         <CommonAppButton :iconName="Menu" @click="toggleMenu"></CommonAppButton>
      </div>
      <div class="w-1/2 h-10  rounded">
      </div>
      <div class="w-32 h-8 rounded flex items-center justify-between">
         <CommonAppButton :iconName="isDark ? Sun : Moon" @click="toggleTheme"></CommonAppButton>
         <CommonAppButton :iconName="Bell" :badgeCount="3"></CommonAppButton>
         <CommonAppButton :iconName="MessageSquare"></CommonAppButton>
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