<template>
  <div>
    <v-carousel v-if="events && events.length > 0">
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
    <div v-else>
      <span class="no-event">Il n'y a pas d'event en cours</span>
    </div>
    <v-col v-if="lastFinishEvent && winner" cols="2">
      <v-card class="winner">
        <nuxt-link :to="`/app/story/${winner.id}/reader`" class="ml-auto">
          <v-img :src="winner.coverUrl" height="200"></v-img>
          <v-card-subtitle>
            Découvrez notre dernier vainqueur
          </v-card-subtitle>
          <v-card-title>{{ winner.title }}</v-card-title>
        </nuxt-link>
      </v-card>
    </v-col>
  </div>
</template>

<script>
async function fetch(context) {
  await context.$store.dispatch('events/FETCH_EVENTS', { page: context.page, mode: 'active' })
  await context.$store.dispatch('events/FETCH_LAST_FINISH_EVENT')
  if (context.lastFinishEvent) {
    const result = await context.$axios.get('stories/winner/' + context.lastFinishEvent.id)
    context.winner = result.data
  }
}

export default {
  data () {
    return {
      page:0,
      winner: null
    }
  },
  computed: {
    events () {
      return this.$store.state.events.events
    },
    lastFinishEvent () {
      return this.$store.state.events.lastFinishEvent
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

.winner {
  height: 350px;
  cursor: pointer;
}

.winner a {
  text-decoration: none;
  color: #ffffff;
}

</style>
