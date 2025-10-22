<template>
  <div class="h-screen flex flex-col items-center justify-center space-y-8">
    <div class="text-center">
      <h1 class="text-4xl font-bold mb-4">SharedYourStories</h1>
      <p class="text-lg text-gray-600 dark:text-gray-400">
        Plateforme littéraire collaborative
      </p>
      <p class="text-2xl mt-4">Mode : {{ isDark ? 'Sombre' : 'Clair' }}</p>
    </div>

    <div class="w-64 h-32 bg-white dark:bg-gray-900 border border-gray-300 dark:border-gray-700 p-4 rounded-lg">
      <p class="text-gray-900 dark:text-gray-100">Test Tailwind Dark Mode</p>
    </div>
  </div>
</template>

<script setup>
import { useTheme } from '~/composables/useTheme'
const { isDark, toggleTheme } = useTheme()

definePageMeta({
  title: 'Accueil'
})
</script>