<template>
  <div
    class="flex h-screen bg-slate-50 dark:bg-slate-900 text-slate-900 dark:text-slate-100 transition-colors duration-300">

    <!-- Sidebar -->
    <UiSidebar :full="full" />
    <!-- Contenu principal -->
    <div class="flex flex-col flex-1">
      <!-- Navbar -->
      <UiNavbar @toggle="toggleSidebar" />

      <!-- Contenu dynamique -->
      <main class="flex-1 overflow-y-auto px-6 py-8 bg-slate-50 dark:bg-slate-900">
        <slot />
      </main>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const full = ref(true)
const toggleSidebar = () => {
  full.value = !full.value
}
</script>
