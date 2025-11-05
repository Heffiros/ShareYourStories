<template>
  <div class="space-y-4">
    <div class="flex items-center space-x-4">
      <div class="w-16 h-16 bg-gray-200 dark:bg-gray-700 rounded-full flex items-center justify-center">
        <User class="w-8 h-8 text-gray-500" />
      </div>
      <div>
        <h4 class="text-lg font-medium text-gray-900 dark:text-gray-100">
          {{ user?.authorName || 'Utilisateur' }}
        </h4>
        <p class="text-sm text-gray-600 dark:text-gray-400">
          {{ user?.email }}
        </p>
      </div>
    </div>

    <div class="space-y-2">
      <div class="flex justify-between">
        <span class="text-sm text-gray-600 dark:text-gray-400">Membre depuis</span>
        <span class="text-sm text-gray-900 dark:text-gray-100">
          {{ formatDate(user?.createdAt) }}
        </span>
      </div>

      <div class="flex justify-between">
        <span class="text-sm text-gray-600 dark:text-gray-400">Statut</span>
        <span class="text-sm text-gray-900 dark:text-gray-100">
          {{ user?.isAdmin ? 'Administrateur' : 'Utilisateur' }}
        </span>
      </div>
    </div>

    <div class="flex space-x-2 pt-4">
      <button class="flex-1 bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-lg transition-colors">
        Voir le profil
      </button>
      <button
        class="flex-1 bg-gray-200 hover:bg-gray-300 dark:bg-gray-700 dark:hover:bg-gray-600 text-gray-900 dark:text-gray-100 px-4 py-2 rounded-lg transition-colors">
        Contacter
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { User } from 'lucide-vue-next'

interface Props {
  userId?: number
  user?: any
}

const props = defineProps<Props>()

const formatDate = (date: string | undefined) => {
  if (!date) return 'Inconnu'
  return new Date(date).toLocaleDateString('fr-FR', {
    year: 'numeric',
    month: 'long'
  })
}
</script>