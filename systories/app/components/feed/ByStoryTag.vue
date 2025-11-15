<template>
  <div class="space-y-4">
    <div class="flex items-center justify-between">
      <h2 class="text-xl font-bold text-gray-900 dark:text-gray-100">
        {{ tagData?.label || 'Chargement...' }}
      </h2>
    </div>

    <div v-if="stories.length === 0 && !loading" class="flex items-center justify-center py-16">
      <p class="text-lg text-gray-500 dark:text-gray-400 text-center">
        Il n'y a pas encore d'histoire, soyez le premier
      </p>
    </div>

    <div v-else class="relative group">
      <button @click="scrollLeft" :disabled="!canScrollLeft"
        class="absolute left-0 top-0 bottom-0 z-10 w-16 bg-black/50 hover:bg-black/70 flex items-center justify-center opacity-0 group-hover:opacity-100 transition-opacity duration-300 disabled:opacity-0 cursor-pointer">
        <ChevronLeft class="w-8 h-8 text-white" />
      </button>

      <button @click="scrollRight" :disabled="!canScrollRight"
        class="absolute right-0 top-0 bottom-0 z-10 w-16 bg-black/50 hover:bg-black/70 flex items-center justify-center opacity-0 group-hover:opacity-100 transition-opacity duration-300 disabled:opacity-0 cursor-pointer">
        <ChevronRight class="w-8 h-8 text-white" />
      </button>

      <div ref="scrollContainer" class="flex space-x-4 overflow-x-hidden scroll-smooth px-2"
        @scroll="updateScrollButtons">
        <div v-for="story in stories" :key="story.id" class="flex-shrink-0 cursor-pointer group px-2 py-2"
          @click="navigateToStory(story.id)">

          <div class="relative transform transition-all duration-300 group-hover:scale-105">
            <div
              class="w-80 h-48 bg-gradient-to-br from-blue-500 to-purple-600 rounded-lg group-hover:rounded-t-lg group-hover:rounded-b-none overflow-hidden shadow-lg relative">
              <img v-if="story.coverUrl" :src="story.coverUrl" :alt="story.title" class="w-full h-full object-cover" />

              <div class="absolute inset-0 bg-gradient-to-t from-black/80 via-transparent to-transparent"></div>

              <div class="absolute bottom-0 left-0 right-0 p-4">
                <h3 class="text-white font-bold text-xl leading-tight line-clamp-2">
                  {{ story.title }}
                </h3>
              </div>
            </div>

            <div
              class="w-80 bg-gray-800 rounded-b-lg shadow-lg opacity-0 group-hover:opacity-100 transition-all duration-300 p-4 space-y-3">
              <div class="flex items-center justify-between text-white/80 text-sm">
                <div class="flex items-center space-x-4">
                  <div class="flex items-center space-x-1">
                    <Heart class="w-4 h-4 text-red-400" />
                    <span>{{ story.storyVotes?.length || 0 }}</span>
                  </div>

                  <div class="flex items-center space-x-1">
                    <svg class="w-4 h-4" fill="currentColor" viewBox="0 0 20 20">
                      <path fill-rule="evenodd"
                        d="M18 10c0 3.866-3.582 7-8 7a8.841 8.841 0 01-4.083-.98L2 17l1.338-3.123C2.493 12.767 2 11.434 2 10c0-3.866 3.582-7 8-7s8 3.134 8 7zM7 9H5v2h2V9zm8 0h-2v2h2V9zM9 9h2v2H9V9z"
                        clip-rule="evenodd" />
                    </svg>
                    <span>{{ story.commentCount || 0 }}</span>
                  </div>

                  <div class="flex items-center space-x-1">
                    <svg class="w-4 h-4" fill="currentColor" viewBox="0 0 20 20">
                      <path fill-rule="evenodd"
                        d="M6 2a1 1 0 00-1 1v1H4a2 2 0 00-2 2v10a2 2 0 002 2h12a2 2 0 002-2V6a2 2 0 00-2-2h-1V3a1 1 0 10-2 0v1H7V3a1 1 0 00-1-1zm0 5a1 1 0 000 2h8a1 1 0 100-2H6z"
                        clip-rule="evenodd" />
                    </svg>
                    <span>2024</span>
                  </div>
                </div>
              </div>

              <div class="flex items-center space-x-2 text-sm text-white/60">
                <span>Science-fiction</span>
                <span class="text-white/40">•</span>
                <span>Fantastique</span>
              </div>
            </div>
          </div>
        </div>

        <div v-if="loading" v-for="n in 3" :key="`loading-${n}`" class="flex-shrink-0">
          <div class="w-80 h-48 bg-gray-300 dark:bg-gray-600 rounded-lg animate-pulse"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ChevronLeft, ChevronRight, Heart } from 'lucide-vue-next'
import type { Story } from '~/types/story'

const currentPage = ref(0)
const stories = ref<Story[]>([])
const tagData = ref<{ id: number, label: string } | null>(null)
const loading = ref(false)
const scrollContainer = ref<HTMLElement>()
const canScrollLeft = ref(false)
const canScrollRight = ref(true)

const props = defineProps<{
  storyTagId: number
}>()

const config = useRuntimeConfig()

const fetchStories = async (page = 0) => {
  try {
    loading.value = true
    const { useAuthStore } = await import('~/stores/auth')
    const auth = useAuthStore()

    if (!auth.token) {
      return
    }

    const response = await $fetch(`/feed/${props.storyTagId}`, {
      baseURL: config.public.apiBase,
      headers: { Authorization: `Bearer ${auth.token}` },
      query: { page, limit: 10 }
    }) as { stories: Story[], tag: { id: number, label: string }, pagination: any }

    if (page === 0) {
      stories.value = response.stories
      tagData.value = response.tag
    } else {
      stories.value.push(...response.stories)
    }

    nextTick(() => {
      updateScrollButtons()
    })

  } catch (error) {
    console.error('Erreur lors du chargement des stories:', error)
  } finally {
    loading.value = false
  }
}
const navigateToStory = (storyId: number) => {
  navigateTo(`/story/${storyId}`)
}

const scrollLeft = () => {
  if (scrollContainer.value) {
    scrollContainer.value.scrollBy({ left: -400, behavior: 'smooth' })
  }
}

const scrollRight = async () => {
  if (scrollContainer.value) {
    scrollContainer.value.scrollBy({ left: 400, behavior: 'smooth' })

    const { scrollLeft, scrollWidth, clientWidth } = scrollContainer.value
    if (scrollLeft + clientWidth >= scrollWidth - 200) {
      currentPage.value++
      await fetchStories(currentPage.value)
    }
  }
}

const updateScrollButtons = () => {
  if (scrollContainer.value) {
    const { scrollLeft, scrollWidth, clientWidth } = scrollContainer.value
    canScrollLeft.value = scrollLeft > 0
    canScrollRight.value = scrollLeft + clientWidth < scrollWidth - 10
  }
}

onMounted(async () => {
  await nextTick()
  await fetchStories()

  nextTick(() => {
    if (scrollContainer.value) {
      const resizeObserver = new ResizeObserver(() => {
        updateScrollButtons()
      })
      resizeObserver.observe(scrollContainer.value)

      onUnmounted(() => {
        resizeObserver.disconnect()
      })
    }
  })
})
</script>

<style scoped>
.line-clamp-2 {
  display: -webkit-box;
  -webkit-line-clamp: 2;
  line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
}

.line-clamp-3 {
  display: -webkit-box;
  -webkit-line-clamp: 3;
  line-clamp: 3;
  -webkit-box-orient: vertical;
  overflow: hidden;
}

.scroll-smooth {
  scroll-behavior: smooth;
}
</style>
