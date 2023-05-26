<template>
  <app-story-creator :event-id="eventId" @created="$emit('created')"/>
</template>

<script>
import appStoryCreator from '~/components/form/AppStoryCreator'

export default {
  components: {
    appStoryCreator
  },
  props: {
    eventId: {
      type: Number,
      default: 0
    }
  }
}
</script>


<style scoped>
.popup {
  background-color: #fff;
  color: #000000 !important;
}
</style>
