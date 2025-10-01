<template>
  <div class="flex gap-1">
    <NotificationButton :icon="Bell" :showPastille="true" />
    <NotificationButton :icon="Moon" />
    <NotificationButton :icon="MessageSquare" :showPastille="true" />
  </div>
</template>
<script setup lang="ts">
  import NotificationButton from '~/components/common/NotificationButton.vue'
  import { Bell, Moon, MessageSquare } from "lucide-vue-next"
</script>