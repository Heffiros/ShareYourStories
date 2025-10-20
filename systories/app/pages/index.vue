<template>
  <div class="h-screen flex flex-col items-center justify-center space-y-8">
    <div class="text-center">
      <h1 class="text-4xl font-bold mb-4">SharedYourStories</h1>
      <p class="text-lg text-gray-600 dark:text-gray-400">
        Plateforme littéraire collaborative
      </p>
      <p class="text-2xl mt-4">Mode : {{ isDark ? 'Sombre' : 'Clair' }}</p>
    </div>

    <!-- Test avec classes Tailwind -->
    <div class="w-64 h-32 bg-white dark:bg-gray-900 border border-gray-300 dark:border-gray-700 p-4 rounded-lg">
      <p class="text-gray-900 dark:text-gray-100">Test Tailwind Dark Mode</p>
    </div>

    <!-- Test du composant AppTest nettoyé -->
    <div class="space-y-4">
      <h2 class="text-xl font-semibold text-center">Composant nettoyé :</h2>
      <div class="flex justify-center gap-4">
        <CommonAppTest :showLabel="true" :badgeCount="3" />
        <CommonAppTest :showLabel="false" :badgeCount="5" />
      </div>
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