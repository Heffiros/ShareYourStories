<template>
  <h2 v-if="event" class="carousel-title">{{ event.title }}</h2>
</template>

<script>
export default {
  computed: {
    event () {
      return this.$store.getters['events/getEventById'](parseInt(this.$route.params.eventId))
    }
  },
  async mounted () {
    if (!this.$store.getters['events/getEventById'](parseInt(this.$route.params.eventId))) {
      await this.$store.dispatch('events/FETCH_EVENT', {eventId : parseInt(this.$route.params.eventId)})
    }
  }
};
</script>


<style>

</style>
