<template>
  <div class="p-6 space-y-6">
    <!-- Header Section -->
    <LibraryHeader />

    <!-- Stats Section - TODO: Implémenter les contrôleurs pour les statistiques -->
    <!-- 
    <section>
      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
        <div
          class="bg-white dark:bg-slate-800 rounded-lg p-4 border border-slate-200 dark:border-slate-700 flex items-center gap-4">
          <div class="text-yellow-600 dark:text-yellow-400">
            <BookOpen class="w-6 h-6" />
          </div>
          <div class="flex flex-col">
            <span class="text-2xl font-bold text-slate-900 dark:text-slate-100">6</span>
            <span class="text-sm text-slate-600 dark:text-slate-400">Histoires</span>
          </div>
        </div>

        <div
          class="bg-white dark:bg-slate-800 rounded-lg p-4 border border-slate-200 dark:border-slate-700 flex items-center gap-4">
          <div class="text-yellow-600 dark:text-yellow-400">
            <Heart class="w-6 h-6" />
          </div>
          <div class="flex flex-col">
            <span class="text-2xl font-bold text-slate-900 dark:text-slate-100">6</span>
            <span class="text-sm text-slate-600 dark:text-slate-400">Histoires</span>
          </div>
        </div>

        <div
          class="bg-white dark:bg-slate-800 rounded-lg p-4 border border-slate-200 dark:border-slate-700 flex items-center gap-4">
          <div class="text-yellow-600 dark:text-yellow-400">
            <MessageSquare class="w-6 h-6" />
          </div>
          <div class="flex flex-col">
            <span class="text-2xl font-bold text-slate-900 dark:text-slate-100">6</span>
            <span class="text-sm text-slate-600 dark:text-slate-400">Histoires</span>
          </div>
        </div>

        <div
          class="bg-white dark:bg-slate-800 rounded-lg p-4 border border-slate-200 dark:border-slate-700 flex items-center gap-4">
          <div class="text-yellow-600 dark:text-yellow-400">
            <FileEdit class="w-6 h-6" />
          </div>
          <div class="flex flex-col">
            <span class="text-2xl font-bold text-slate-900 dark:text-slate-100">2</span>
            <span class="text-sm text-slate-600 dark:text-slate-400">Brouillons</span>
          </div>
        </div>
      </div>
    </section>
    -->

    <!-- Stories Grid Section -->
    <LibraryStoryFeed />
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import {
  BookOpen,
  Heart,
  MessageSquare,
  FileEdit,
} from 'lucide-vue-next'

// Page title
definePageMeta({
  title: 'Ma bibliothèque'
})

// Reactive data
const stories = ref([])

// Computed properties
const sortedStories = computed(() => {
  // Logic for sorting stories will be implemented later
  return stories.value
})

</script>
