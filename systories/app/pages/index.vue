<template>
  <div class="h-screen flex flex-col items-center justify-center space-y-4">
    <p class="text-2xl">Mode : {{ isDark ? 'Sombre' : 'Clair' }}</p>

    <!-- Test avec classes Tailwind -->
    <div class="w-64 h-32 bg-white dark:bg-gray-900 border border-gray-300 dark:border-gray-700 p-4">
      <p class="text-gray-900 dark:text-gray-100">Test Tailwind Dark Mode</p>
    </div>

  </div>
</template>

<script setup>
import { useTheme } from '~/composables/useTheme'
const { isDark, toggleTheme } = useTheme()
</script>