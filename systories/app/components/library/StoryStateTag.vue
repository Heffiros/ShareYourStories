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
    'Draft': {
      label: 'Brouillon',
      classes: 'bg-orange-100 text-orange-800 dark:bg-orange-900/30 dark:text-orange-300'
    },
    'Pending': {
      label: 'En attente',
      classes: 'bg-blue-100 text-blue-800 dark:bg-blue-900/30 dark:text-blue-300'
    },
    'Online': {
      label: 'Publiée',
      classes: 'bg-green-100 text-green-800 dark:bg-green-900/30 dark:text-green-300'
    },
    'ModerateByAdmin': {
      label: 'Modérée par un admin',
      classes: 'bg-red-100 text-red-800 dark:bg-red-900/30 dark:text-red-300'
    },
    'ModerateAuto': {
      label: 'Modérée automatiquement',
      classes: 'bg-red-100 text-red-800 dark:bg-red-900/30 dark:text-red-300'
    }
  }

  return configs[props.status] ?? null
})
</script>