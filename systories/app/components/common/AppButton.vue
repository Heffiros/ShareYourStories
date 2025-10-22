<template>
  <div class="settings-menu-item cursor-pointer">
    <div v-if="iconName"
      class="relative flex items-center gap-3 px-4 py-2 rounded-lg hover:bg-slate-100 dark:hover:bg-slate-700 transition-colors group"
      @click="handleClick">
      <component :is="iconName" :class="primaryIconDesign" />
      <span v-if="label" class="text-sm font-medium">
        {{ label }}
      </span>
      <div v-if="badgeCount" :class="primaryChipDesign">
        {{ badgeCount }}
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">

const emit = defineEmits(['click'])

const props = defineProps({
  label: { type: String, default: null },
  iconName: { type: [Function], default: null },
  badgeCount: { type: Number, default: null },
})

const primaryIconDesign = computed(() => {
  return "w-5 h-5 text-slate-900 dark:text-slate-200 group-hover:text-yellow-500 dark:group-hover:text-yellow-500"
})
const primaryChipDesign = computed(() => {
  return "absolute -top-1 -right-1 bg-yellow-500 text-slate-900 dark:text-slate-900 text-xs font-bold rounded-full w-5 h-5 flex items-center justify-center"
})

const handleClick = () => {
  emit('click')
}
</script>