<template>
  <button
    @click="toggle"
    class="
      p-2
      rounded-full
      bg-neutral-300 dark:bg-neutral-800
      hover:bg-neutral-400 dark:hover:bg-neutral-200
      transition
      focus:outline-none
    "
    aria-label="Toggle dark mode"
  >
    <svg v-if="isDark" xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 text-yellow-300 dark:text-stone-300" fill="currentColor" viewBox="0 0 24 24">
      <path d="M12 3.1a9 9 0 0 0 0 17.8c.6 0 1-.5 1-1s-.4-1-1-1a7 7 0 1 1 0-14c.6 0 1-.5 1-1s-.4-1-1-1z"/>
    </svg>
    <svg v-else xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 text-gray-700 dark:text-gray-300" fill="currentColor" viewBox="0 0 24 24">
      <path d="M6.76 4.84l-1.8-1.79-1.42 1.41 1.79 1.8 1.43-1.42zm10.48 10.48l1.79 1.8 1.42-1.42-1.8-1.79-1.41 1.41zM12 8a4 4 0 0 0 0 8c2.21 0 4-1.79 4-4s-1.79-4-4-4zm0-6h-1v3h1V2zm0 18h-1v3h1v-3zM4 11H1v1h3v-1zm20 0h-3v1h3v-1z"/>
    </svg>
  </button>
</template>

<script setup>
import { ref, onMounted } from 'vue'

const isDark = ref(false)

function applyClass(dark) {
  const el = document.documentElement
  if (dark) el.classList.add('dark')
  else el.classList.remove('dark')
}

onMounted(() => {
  // initialise d’après le pref système
  const prefers = window.matchMedia('(prefers-color-scheme: dark)').matches
  isDark.value = localStorage.getItem('dark') === 'true' || prefers
  applyClass(isDark.value)
})

function toggle() {
  isDark.value = !isDark.value
  localStorage.setItem('dark', isDark.value)
  applyClass(isDark.value)
}
</script>
