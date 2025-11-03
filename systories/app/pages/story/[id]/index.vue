<template>
  <div v-if="isLoading" class="flex justify-center items-center h-screen">
    <div class="animate-spin rounded-full h-12 w-12 border-b-2 border-yellow-600"></div>
  </div>
  <div v-else-if="error" class="flex justify-center items-center h-screen">
    <div class="text-center text-red-600">
      <p>Erreur lors du chargement de l'histoire</p>
    </div>
  </div>
  <CommonAppReader v-else-if="story" :story="story" />
</template>

<script setup lang="ts">
import { useStoryStore } from '~/stores/story'

const route = useRoute()
const storyStore = useStoryStore()

const isLoading = ref(false)
const error = ref(false)

const storyId = computed(() => {
  const id = route.params.id
  return typeof id === 'string' ? parseInt(id) : null
})

const story = computed(() => {
  if (!storyId.value) return null
  return storyStore.stories.find(s => s.id === storyId.value) || null
})

const fetchStory = async (id: number) => {
  if (isLoading.value) return

  try {
    isLoading.value = true
    error.value = false

    const fetchedStory = await storyStore.getStoryById(id)

    if (fetchedStory && !storyStore.stories.find(s => s.id === id)) {
      storyStore.stories.push(fetchedStory)
    }

    if (!fetchedStory) {
      error.value = true
    }
  } catch (err) {
    console.error('Error fetching story:', err)
    error.value = true
  } finally {
    isLoading.value = false
  }
}

watch(storyId, async (newId) => {
  if (newId && !story.value) {
    await fetchStory(newId)
  }
}, { immediate: true })
</script>
