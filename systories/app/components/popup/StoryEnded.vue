<template>
  <div class="space-y-4">
    <div class="flex items-center space-x-4">

    </div>
    <div class="flex space-x-2 pt-4">
      <button class="flex-1 bg-yellow-500 hover:bg-yellow-600 text-white px-4 py-2 rounded-lg transition-colors"
        @click="goToHub()">
        Partager votre avis
      </button>
      <button
        class="flex-1 bg-gray-200 hover:bg-gray-300 dark:bg-gray-700 dark:hover:bg-gray-600 text-gray-900 dark:text-gray-100 px-4 py-2 rounded-lg transition-colors">
        Bibliothèque de {{ authorName }}
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { usePopupStore } from '~/stores/popup'

const popupStore = usePopupStore()
interface Props {
  storyId: number,
  authorName: string
}

const props = defineProps<Props>()

const goToHub = () => {
  popupStore.closePopup()
  navigateTo('/story/' + props.storyId + '/hub')
}
</script>