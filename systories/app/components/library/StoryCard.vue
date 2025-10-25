<template>
  <div class="bg-white dark:bg-slate-800 rounded-lg border border-slate-200 dark:border-slate-700 overflow-hidden">
    <div class="w-full h-48 bg-gradient-to-br from-slate-100 to-slate-200 dark:from-slate-700 dark:to-slate-800">
      <img v-if="story?.coverUrl" :src="story.coverUrl" alt="Story Cover" class="w-full h-full object-cover" />
    </div>
    <div class="p-4 space-y-3">
      <h3 class="font-semibold text-slate-900 dark:text-slate-100 line-clamp-2">
        {{ story?.title || 'Coeur entrelacés : Une histoire d\'amour' }}
      </h3>
      <p class="text-sm text-slate-600 dark:text-slate-400 line-clamp-3">
        {{ story?.summary || 'Lorsque l\'architecte Emma Collins rénove une vieille librairie, elle découvre l\'amour..'
        }}
      </p>
      <div class="flex gap-4 text-xs text-slate-500 dark:text-slate-500">
        <span>{{ story?.storyVotes?.length || 0 }} votes</span>
        <span>{{ story?.pages?.length || 0 }} pages</span>
        <span>Statut: {{ story?.status || 'Pending' }}</span>
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
import { BookOpenText, UsersRound } from 'lucide-vue-next'

interface Props {
  story?: Story
}

const props = defineProps<Props>()
</script>