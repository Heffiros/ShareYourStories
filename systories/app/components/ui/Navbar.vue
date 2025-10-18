<template>
   <header class="h-18 border-b border-gray-800 flex items-center justify-between px-4">
      <div class="w-32 h-8 bg-gray-700 rounded">
         <Menu class="icon" @click="toggleMenu"></Menu>
      </div>
      <div class="w-1/2 h-10 bg-gray-700 rounded">
      </div>
      <div class="w-32 h-8 bg-gray-700 rounded flex items-center justify-between">
         <Moon class="w-6 h-6 text-yellow-500 icon" />
         <MessageSquare class="w-6 h-6 text-yellow-500 icon" />
         <Bell class="w-6 h-6 text-yellow-500 icon" />
      </div>
   </header>
</template>

<script setup>
import { Moon, MessageSquare, Bell, Menu } from 'lucide-vue-next'

const emit = defineEmits(['toggle'])

const toggleMenu = () => {
   emit('toggle', 0)
}
</script>

<style lang="stylus" scoped>
.icon
  cursor pointer
</style>