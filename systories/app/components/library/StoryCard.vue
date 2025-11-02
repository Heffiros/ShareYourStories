<template>
  <div
    class="bg-white dark:bg-slate-800 rounded-lg border border-slate-200 dark:border-slate-700 overflow-hidden relative flex flex-col">
    <LibraryStoryStateTag v-if="story?.status" :status="story.status" />

    <div
      class="w-full h-48 bg-gradient-to-br from-slate-100 to-slate-200 dark:from-slate-700 dark:to-slate-800 flex-shrink-0">
      <img v-if="story?.coverUrl" :src="story.coverUrl" alt="Story Cover" class="w-full h-full object-cover" />
    </div>
    <div class="p-4 flex flex-col h-full">
      <h3 class="font-semibold text-slate-900 dark:text-slate-100 line-clamp-2 mb-3">
        {{ story?.title || 'Coeur entrelacés : Une histoire d\'amour' }}
      </h3>
      <p class="text-sm text-slate-600 dark:text-slate-400 line-clamp-3 mb-3">
        {{ story?.summary || 'Lorsque l\'architecte Emma Collins rénove une vieille librairie, elle découvre l\'amour..'
        }}
      </p>

      <div class="min-h-[2rem] flex flex-wrap gap-2 mb-3">
        <LibraryStoryTag v-for="tag in story?.storyTags || []" :key="tag.id" :label="tag.label" />
      </div>

      <div class="flex-grow"></div>

      <div class="flex gap-4 text-xs text-slate-500 dark:text-slate-500 mb-3">
        <span class="flex items-center gap-1">
          <MessageCircle class="w-3 h-3" />
          {{ story?.commentCount || 0 }}
        </span>
        <span>{{ story?.createdAt ? new Date(story.createdAt).getFullYear() : 2024 }}</span>
      </div>
      <div class="flex gap-2">
        <button
          class="bg-gradient-to-r py-3 px-6 rounded-lg duration-200 flex items-center gap-2 transition-colors bg-yellow-600 hover:bg-yellow-700 text-white dark:text-slate-900">
          <BookOpenText class="w-5 h-5" />
          Lire
        </button>
        <button
          class="px-4 py-2 rounded-lg font-medium transition-colors flex items-center gap-2 bg-slate-100 dark:bg-slate-700 hover:bg-slate-200 dark:hover:bg-slate-600 text-slate-700 dark:text-slate-300">
          <UsersRound class="w-5 h-5" />
          Hubs
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import type { Story } from '~/types/story'
import { BookOpenText, UsersRound, MessageCircle } from 'lucide-vue-next'

interface Props {
  story?: Story
}

const props = defineProps<Props>()
</script>