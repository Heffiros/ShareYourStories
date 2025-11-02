<template>
  <div class="min-h-screen bg-slate-50 dark:bg-slate-900">
    <UiSimpleSlider />
  </div>
</template>

<script setup>
import { useTheme } from '~/composables/useTheme'

const { isDark } = useTheme()

definePageMeta({
  title: 'Accueil'
})
</script>