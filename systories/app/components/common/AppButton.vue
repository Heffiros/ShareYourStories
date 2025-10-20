<template>
  <div class="settings-menu-item cursor-pointer">
    <div v-if="iconName"
      class="relative flex items-center gap-3 px-4 py-2 rounded-lg hover:bg-gray-200 dark:hover:bg-gray-700 transition-colors group"
      @click="handleClick">
      <component :is="iconName"
        class="w-5 h-5 text-gray-950 dark:text-gray-200 group-hover:text-yellow-500 dark:group-hover:text-yellow-500" />
      <span v-if="label" class="text-sm font-medium">
        {{ label }}
      </span>
      <div v-if="badgeCount"
        class="absolute -top-1 -right-1 bg-yellow-500 text-gray-950 dark:text-gray-950 text-xs font-bold rounded-full w-5 h-5 flex items-center justify-center">
        {{ badgeCount }}
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">

const emit = defineEmits(['click'])

const props = defineProps({
  label: { type: String, default: null },
  iconName: { type: String, default: null },
  badgeCount: { type: Number, default: null },
})

const handleClick = () => {
  console.log('Button clicked')
  emit('click')
}
</script>