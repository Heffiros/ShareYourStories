<template>
  <div class="p-6 space-y-6">
    <LibraryHeader />
    <section>
      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
        <div
          class="bg-white dark:bg-slate-800 rounded-lg p-4 border border-slate-200 dark:border-slate-700 flex items-center gap-4">
          <div class="flex items-center gap-2">
            <div class="w-3 h-3 bg-yellow-500 rounded-full"></div>
            <span class="text-sm text-slate-600 dark:text-slate-400">Total</span>
          </div>
          <div class="ml-auto">
            <span class="text-2xl font-bold text-slate-900 dark:text-slate-100">{{ stats?.total || 0 }}</span>
          </div>
        </div>

        <div
          class="bg-white dark:bg-slate-800 rounded-lg p-4 border border-slate-200 dark:border-slate-700 flex items-center gap-4">
          <div class="flex items-center gap-2">
            <div class="w-3 h-3 bg-green-500 rounded-full"></div>
            <span class="text-sm text-slate-600 dark:text-slate-400">Publiées</span>
          </div>
          <div class="ml-auto">
            <span class="text-2xl font-bold text-slate-900 dark:text-slate-100">{{ stats?.online || 0 }}</span>
          </div>
        </div>

        <div
          class="bg-white dark:bg-slate-800 rounded-lg p-4 border border-slate-200 dark:border-slate-700 flex items-center gap-4">
          <div class="flex items-center gap-2">
            <div class="w-3 h-3 bg-orange-500 rounded-full"></div>
            <span class="text-sm text-slate-600 dark:text-slate-400">Brouillons</span>
          </div>
          <div class="ml-auto">
            <span class="text-2xl font-bold text-slate-900 dark:text-slate-100">{{ stats?.draft || 0 }}</span>
          </div>
        </div>

        <div
          class="bg-white dark:bg-slate-800 rounded-lg p-4 border border-slate-200 dark:border-slate-700 flex items-center gap-4">
          <div class="flex items-center gap-2">
            <div class="w-3 h-3 bg-blue-500 rounded-full"></div>
            <span class="text-sm text-slate-600 dark:text-slate-400">En attente</span>
          </div>
          <div class="ml-auto">
            <span class="text-2xl font-bold text-slate-900 dark:text-slate-100">{{ stats?.pending || 0 }}</span>
          </div>
        </div>
      </div>
    </section>
    <LibraryStoryFeed />
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { useAuthStore } from '~/stores/auth'

// Types
interface StoriesStats {
  total: number
  draft?: number
  pending?: number
  online?: number
}

// Page title
definePageMeta({
  title: 'Ma bibliothèque'
})

// Stores
const auth = useAuthStore()

// Reactive data
const stories = ref([])
const stats = ref<StoriesStats | null>(null)
const isLoadingStats = ref(false)

// Computed properties
const sortedStories = computed(() => {
  // Logic for sorting stories will be implemented later
  return stories.value
})

// Methods
const fetchStats = async () => {
  if (!auth.user?.id) return

  try {
    isLoadingStats.value = true
    const config = useRuntimeConfig()

    const response = await $fetch<StoriesStats>(`/stats/stories?userId=${auth.user.id}`, {
      method: 'GET',
      baseURL: config.public.apiBase,
      headers: { Authorization: `Bearer ${auth.token}` }
    })

    stats.value = response
  } catch (error) {
    console.error('Error loading stats:', error)
  } finally {
    isLoadingStats.value = false
  }
}

// Lifecycle
onMounted(() => {
  fetchStats()
})
</script>
