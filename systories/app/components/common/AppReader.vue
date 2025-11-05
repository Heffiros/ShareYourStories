<template>
  <div class="relative w-full h-full flex flex-col items-center justify-center p-8">
    <div class="relative w-full flex items-center justify-center flex-1">
      <button v-if="canGoBack" @click="goToPreviousPage"
        class="absolute left-4 z-10 p-3 rounded-full bg-yellow-500 hover:bg-yellow-600 text-white transition-colors duration-200 shadow-lg hover:shadow-xl">
        <ChevronLeft class="w-6 h-6" />
      </button>

      <div
        class="w-4/5 h-full max-h-[70vh] aspect-[3/4] bg-gray-100 dark:bg-slate-700 rounded-lg shadow-inner border border-gray-200 dark:border-slate-600 flex items-center justify-center">
        <div class="w-full h-full p-8 overflow-y-auto font-serif leading-relaxed">
          <slot>
            <div class="text-center text-gray-500 dark:text-slate-400">
              <div v-if="currentPage" class="text-lg leading-relaxed text-left">
                <div v-html="currentPage.content"></div>
              </div>
              <p v-else class="text-lg leading-relaxed">
                Aucune page à afficher
              </p>
            </div>
          </slot>
        </div>
      </div>

      <button @click="goToNextPage"
        class="absolute right-4 z-10 p-3 rounded-full bg-yellow-500 hover:bg-yellow-600 text-white transition-colors duration-200 shadow-lg hover:shadow-xl">
        <ChevronRight class="w-6 h-6" />
      </button>
    </div>

    <div class="w-4/5 mt-6">
      <div class="flex items-center justify-between text-sm text-gray-600 dark:text-gray-400 mb-2">
        <span>Page {{ currentPageIndex + 1 }} sur {{ pages.length }}</span>
        <span>{{ progressPercentage }}%</span>
      </div>
      <div class="w-full bg-gray-200 dark:bg-gray-700 rounded-full h-2">
        <div class="bg-yellow-500 h-2 rounded-full transition-all duration-300 ease-out"
          :style="{ width: progressPercentage + '%' }"></div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ChevronLeft, ChevronRight } from 'lucide-vue-next'
import type { Story } from '~/types/story'
const { $popup } = useNuxtApp()

interface Props {
  story?: Story
}

const props = defineProps<Props>()

const currentPageIndex = ref(0)
const pages = computed(() => props.story?.pages || [])

const currentPage = computed(() => {
  if (pages.value.length === 0) return null
  return pages.value[currentPageIndex.value] || null
})

const canGoBack = computed(() => currentPageIndex.value > 0)
const canGoForward = computed(() => currentPageIndex.value < pages.value.length - 1)

const progressPercentage = computed(() => {
  if (pages.value.length === 0) return 0
  return Math.round(((currentPageIndex.value + 1) / pages.value.length) * 100)
})

const initializeCurrentPage = () => {
  if (!props.story?.pages || props.story.pages.length === 0) {
    currentPageIndex.value = 0
    return
  }

  if (!props.story.storyHistory) {
    currentPageIndex.value = 0
    saveStoryHistory()
  } else {
    const pageIndex = props.story.pages.findIndex(page =>
      page.id === props.story?.storyHistory?.lastPageReadId
    )
    currentPageIndex.value = pageIndex >= 0 ? pageIndex : 0
  }
}

const saveStoryHistory = async () => {
  if (!props.story?.id || !currentPage.value?.id) return

  try {
    const { useAuthStore } = await import('~/stores/auth')
    const auth = useAuthStore()
    const config = useRuntimeConfig()

    const isFirstTime = !props.story.storyHistory

    const url = '/storyHistory'
    const method = isFirstTime ? 'POST' : 'PUT'

    await $fetch(url, {
      method: method,
      baseURL: config.public.apiBase,
      headers: { Authorization: `Bearer ${auth.token}` },
      body: {
        storyId: props.story.id,
        lastPageReadId: currentPage.value.id
      }
    })

    if (isFirstTime && props.story) {
      props.story.storyHistory = {
        id: 0,
        userId: auth.user?.id || 0,
        storyId: props.story.id,
        lastPageReadId: currentPage.value.id,
        reread: 0,
        historyStateValue: 'Reading',
        createdAt: new Date().toISOString(),
        updatedAt: new Date().toISOString()
      }
    } else if (props.story?.storyHistory) {
      props.story.storyHistory.lastPageReadId = currentPage.value.id
      props.story.storyHistory.updatedAt = new Date().toISOString()
    }

  } catch (error) {
    console.error('Error saving story history:', error)
  }
}

const goToPreviousPage = async () => {
  if (canGoBack.value) {
    currentPageIndex.value--
    await saveStoryHistory()
  }
}

const goToNextPage = async () => {
  if (canGoForward.value) {
    currentPageIndex.value++
    await saveStoryHistory()
  } else {
    $popup.emit('StoryEnded', {
      storyId: props.story?.id || 0,
      authorName: props.story?.user.authorName || 'Auteur'
    }, {
      title: '🎊 Vous avez terminé ' + props.story?.title + ' 🎊',
      closeOnOverlayClick: true,
      confetti: true,
      backgroundUrl: props.story?.coverUrl || '',
      size: 'xl'
    })
  }
}

watch(() => props.story, () => {
  if (props.story) {
    initializeCurrentPage()
  }
}, { immediate: true })
</script>