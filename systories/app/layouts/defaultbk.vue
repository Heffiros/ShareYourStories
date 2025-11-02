<template>
  <div class="flex h-screen bg-primary-light dark:bg-primary-dark text-gray-900 dark:text-gray-100">
    <UiSidebar :full="full" />
    <div class="flex flex-col flex-1">
      <UiNavbar @toggle="full = !full" />
      <main
        class="flex-1 overflow-y-auto container mx-auto px-6 py-8 bg-primary-light dark:bg-primary-dark text-gray-900 dark:text-gray-100">
        <slot />
      </main>
    </div>
  </div>
</template>


<script setup lang="ts">
const full = ref(true)

</script>