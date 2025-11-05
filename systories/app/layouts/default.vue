<template>
  <div
    class="flex h-screen bg-slate-50 dark:bg-slate-900 text-slate-900 dark:text-slate-100 transition-colors duration-300">
    <UiSidebar :full="full" />
    <div class="flex flex-col flex-1">
      <UiNavbar @toggle="toggleSidebar" />
      <main class="flex-1 overflow-y-auto px-6 py-8 bg-slate-50 dark:bg-slate-900">
        <slot />
      </main>
    </div>

    <CommonAppPopup />
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const full = ref(true)
const toggleSidebar = () => {
  full.value = !full.value
}
</script>
