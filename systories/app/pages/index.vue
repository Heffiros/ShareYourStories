<template>
  <div class="min-h-screen bg-slate-50 dark:bg-slate-900">
    <section class="relative">
      <EventOngoingSlider />
    </section>

    <div class="container mx-auto px-4 pt-16 pb-8 space-y-12">
      <section class="relative">
        <FeedByStoryTag :story-tag-id="2" />
      </section>

      <section class="relative">
        <FeedByStoryTag :story-tag-id="3" />
      </section>

      <section class="relative">
        <FeedByStoryTag :story-tag-id="4" />
      </section>

      <section class="relative">
        <FeedByStoryTag :story-tag-id="5" />
      </section>

      <section class="relative">
        <FeedByStoryTag :story-tag-id="6" />
      </section>
    </div>
  </div>
</template>

<script setup>
import { useTheme } from '~/composables/useTheme'

const { isDark } = useTheme()

definePageMeta({
  title: 'Accueil'
})
</script>