<template>
  <a href="">
  <div class="relative w-10 h-10 rounded-md bg-[#f3f4f6] flex items-center justify-center">
    <NotificationPastille v-if="showPastille" />

    <component :is="icon" class="w-5 h-5 text-gray-800" />
  </div>
  </a>
</template>

<script setup lang="ts">
import NotificationPastille from '~/components/common/NotificationPastille.vue'

interface Props {
  icon: string,
  showPastille?: boolean
}

withDefaults(defineProps<Props>(), {
  showPastille: false,
})
</script>