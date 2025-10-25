<template>
  <section class="w-full">
    <div class="flex justify-between items-start">
      <div>
        <h1 class="text-3xl font-bold text-slate-900 dark:text-slate-100">Ma bibliothèque</h1>
        <p class="text-slate-600 dark:text-slate-400 mt-1">Gérez vos histoires et suivez vos performances</p>
      </div>
      <button
        class="bg-gradient-to-r from-yellow-400 to-yellow-600 hover:from-yellow-500 hover:to-yellow-700 text-white dark:text-slate-900 font-semibold py-3 px-6 rounded-lg transition-all duration-200 flex items-center gap-2"
        @click="navigateToNewStory">
        <Plus class="w-5 h-5" />
        Nouvelle histoire
      </button>
    </div>
  </section>
</template>

<script setup lang="ts">
import { Plus } from 'lucide-vue-next'

const navigateToNewStory = () => {
  navigateTo('/write')
}
</script>