<template>
  <span
    class="inline-flex items-center px-2 py-1 rounded text-xs font-medium bg-slate-100 text-slate-700 dark:bg-slate-700 dark:text-slate-300">
    {{ label }}
  </span>
</template>

<script setup lang="ts">
interface Props {
  label: string
}

defineProps<Props>()
</script>