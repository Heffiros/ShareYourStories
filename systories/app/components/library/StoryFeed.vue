<template>
  <section class="flex justify-between items-center">
    <div class="flex items-center gap-4">
      <button @click="toggleOrder"
        class="bg-white dark:bg-slate-800 border border-slate-200 dark:border-slate-700 rounded-lg px-4 py-2 flex items-center gap-2 text-slate-700 dark:text-slate-300 hover:bg-slate-50 dark:hover:bg-slate-700 transition-colors"
        :title="sortOrder === 'desc' ? 'Plus récentes en premier' : 'Plus anciennes en premier'">
        <span>{{ sortOrder === 'desc' ? 'Plus récentes' : 'Plus anciennes' }}</span>
        <ArrowUpNarrowWide class="w-4 h-4 transition-transform duration-200"
          :class="{ 'rotate-180': sortOrder === 'asc' }" />
      </button>
    </div>
    <div>
      <LibraryStorySearch @search="handleSearch" />
    </div>
  </section>
  <section>
    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
      <LibraryStoryCard v-for="story in stories" :key="story.id" :story="story" />
    </div>
    <div v-if="isLoading" class="flex justify-center py-4">
      <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-yellow-600"></div>
    </div>
    <InfiniteLoading :key="infiniteLoadingKey" @infinite="load" style="opacity: 0; pointer-events: none;" />
  </section>
</template>

<script setup lang="ts">
import { ArrowUpNarrowWide } from 'lucide-vue-next'
import { useStoryStore } from '~/stores/story'
import InfiniteLoading from "v3-infinite-loading"
import "v3-infinite-loading/lib/style.css"

interface InfiniteLoadState {
  loaded(): void;
  complete(): void;
  error(): void;
}

const storyStore = useStoryStore()
const infiniteLoadingKey = ref(0)

const stories = computed(() => storyStore.stories)
const sortOrder = computed(() => storyStore.sortOrder)
const isLoading = computed(() => storyStore.isLoading)

const toggleOrder = () => {
  const newOrder = storyStore.sortOrder === 'desc' ? 'asc' : 'desc'
  storyStore.updateFilters({ sortOrder: newOrder })
  infiniteLoadingKey.value++
}

const handleSearch = (term: string) => {
  storyStore.updateFilters({ searchTerm: term })
  infiniteLoadingKey.value++
}

watch(() => useRoute().query.mode, (newMode) => {
  storyStore.updateFilters({ mode: newMode as string || null })
  infiniteLoadingKey.value++
})

const load = async ($state: InfiniteLoadState) => {
  try {
    const response = await storyStore.loadStories()

    if (response && response.length > 0) {
      $state.loaded()
    } else {
      $state.complete()
    }
  } catch (error) {
    console.error('Error loading stories:', error)
    $state.error()
  }
}

onMounted(() => {
  const route = useRoute()
  storyStore.updateFilters({
    mode: route.query.mode as string || null
  })
})
</script>