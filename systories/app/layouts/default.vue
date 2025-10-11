<template>
  <div class="flex h-screen">
    <UiSidebar></UiSidebar>
    <div class="flex flex-col flex-1">
      <UiNavbar></UiNavbar>
      <main class="flex-1 container mx-auto px-4 py-8">
        <slot />
      </main>
    </div>
  </div>
</template>
