<template>
  <v-carousel v-if="events">
    <v-carousel-item class="carousel-item" v-for="(event, index) in events" :key="index" @click="goEventFeed(event.id)">
      <!-- Image du carrousel -->
      <v-img :src="event.coverUrl" :alt="event.title"></v-img>

      <!-- En-tête avec titre transparent -->
      <div class="carousel-header">
        <h2 class="carousel-title">{{ event.title }}</h2>
        <span class="carousel-date">{{ event.dateEnd }}</span>
      </div>
    </v-carousel-item>
  </v-carousel>
</template>

<script>
async function fetch(context) {
  await context.$store.dispatch('events/FETCH_EVENTS', { page: context.page })
}

export default {
  data () {
    return {
      page:0
    }
  },
  computed: {
    events () {
      return this.$store.state.events.events
    }
  },
  methods: {
    goEventFeed (eventId) {
      this.$router.push('/app/event/' + eventId)
    }
  },
  async mounted () {
    await this.$store.dispatch('events/RESET_EVENTS')
    fetch (this)
  }
}
</script>


<style>
.carousel-header {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  background-color: rgba(0, 0, 0, 0.3); /* Couleur de fond transparente */
  padding: 16px;
  color: #ffffff;
  width: 100%;
}

.carousel-item {
  cursor: pointer;
}
.carousel-title {
  display: inline-block;
  margin: 0;
  clear: both;
}

.carousel-date {
  display: inline-block;
  float: right;
  vertical-align: middle;
  clear: both;
}
</style>
