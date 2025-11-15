<template>
  <UiSimpleSlider :items="contests" />
</template>

<script setup lang="ts">
import type { Event } from '~/types/event'
import { useAuthStore } from '~/stores/auth'

const contests = ref<Event[]>([])
const auth = useAuthStore()

onMounted(async () => {
  const config = useRuntimeConfig()
  const response = await $fetch<Event[]>('/event?mode=active', {
    method: 'GET',
    baseURL: config.public.apiBase,
    headers: { Authorization: `Bearer ${auth.token}` }
  })
  contests.value = response
})
</script>