<template>
  <div v-if="tagConfig" class="absolute top-3 left-3 px-2 py-1 rounded text-xs font-medium z-10"
    :class="tagConfig.classes">
    {{ tagConfig.label }}
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import type { StoryStatus, StoryStatusConfigs } from '~/types/storyStatus'

const props = defineProps<{
  status: StoryStatus
}>()

const tagConfig = computed(() => {
  const configs: StoryStatusConfigs = {
    'Pending': {
      label: 'Brouillon',
      classes: 'bg-orange-100 text-orange-800 dark:bg-orange-900/30 dark:text-orange-300'
    },
    'Online': {
      label: 'Publiée',
      classes: 'bg-green-100 text-green-800 dark:bg-green-900/30 dark:text-green-300'
    }
  }

  return configs[props.status] ?? null
})
</script>