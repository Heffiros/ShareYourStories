<template>
  <div class="flex h-screen">
    <UiSidebar :full="full"></UiSidebar>
    <div class="flex flex-col flex-1">
      <UiNavbar @toggle="toggle"></UiNavbar>
      <main class="flex-1 container mx-auto px-4 py-8">
        <slot />
      </main>
    </div>
  </div>
</template>

<script setup lang="ts">
const full = ref(true)
const toggle = () => {
  full.value = !full.value
}
</script>