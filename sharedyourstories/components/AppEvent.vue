<template>
    <div v-if="event">
        <span>{{ event.title }}</span>
    </div>
</template>

<script>

export default {
  props: {
    event: {
      type: Object,
      default: null
    }
  },
  methods: {
  }
}
</script>