<template>
  <div class="relative">
    <button v-if="!isSearchOpen" @click="openSearch"
      class="h-10 bg-white dark:bg-slate-800 border border-slate-200 dark:border-slate-700 rounded-lg px-4 py-2 flex items-center gap-2 text-slate-700 dark:text-slate-300 hover:bg-slate-50 dark:hover:bg-slate-700 transition-colors">
      <Search class="w-4 h-4" />
    </button>

    <div v-else
      class="h-10 bg-white dark:bg-slate-800 border border-slate-200 dark:border-slate-700 rounded-lg px-4 py-2 flex items-center gap-2 text-slate-700 dark:text-slate-300 transition-all duration-200 w-64">
      <Search class="w-4 h-4 text-slate-400" />
      <input ref="searchInput" v-model="searchTerm" @blur="handleBlur" @keydown.escape="closeSearch" type="text"
        placeholder="Rechercher une histoire..."
        class="flex-1 text-sm bg-transparent text-slate-700 dark:text-slate-300 placeholder-slate-400 focus:outline-none" />
      <button v-if="searchTerm" @click="clearSearch"
        class="text-slate-400 hover:text-slate-600 dark:hover:text-slate-300">
        <X class="w-4 h-4" />
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { Search, X } from 'lucide-vue-next'

const emit = defineEmits<{
  search: [term: string]
}>()

const isSearchOpen = ref(false)
const searchTerm = ref('')
const searchInput = ref<HTMLInputElement>()

const openSearch = async () => {
  isSearchOpen.value = true
  await nextTick()
  searchInput.value?.focus()
}

const closeSearch = () => {
  if (!searchTerm.value) {
    isSearchOpen.value = false
  }
}

const clearSearch = () => {
  searchTerm.value = ''
  emit('search', '')
  isSearchOpen.value = false
}

const handleBlur = () => {
  if (!searchTerm.value) {
    isSearchOpen.value = false
  }
}

watch(searchTerm, (newTerm) => {
  if (newTerm.length >= 3) {
    emit('search', newTerm)
  } else if (newTerm.length === 0) {
    emit('search', '')
  }
})
</script>