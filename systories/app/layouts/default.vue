<template>
  <div class="flex h-screen">
    <Sidebar></Sidebar>
    <div class="flex flex-col flex-1">
      <Navbar></Navbar>
      <Main></Main>
    </div>
  </div>
</template>

<script setup lang="ts">
  import Navbar from '~/components/ui/Navbar.vue'
  import Main from '~/components/ui/Main.vue'
  import Sidebar from '~/components/ui/Sidebar.vue'
</script>