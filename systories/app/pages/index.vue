<!-- pages/index.vue -->
<template>
  <div>
    <h1 class="text-3xl font-bold mb-4">Accueil</h1>
    <NuxtLink to="/auth/login" class="underline">Aller au login</NuxtLink>
  </div>
</template>

<script setup lang="ts">
definePageMeta({
  middleware: 'auth',
})
</script>