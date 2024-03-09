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
    <v-col v-if="lastFinishEvent" cols="4">
      <v-card class="result-card first-place" color="#FBC02D">
        <div class="result-card-header">1er</div>
        <div class="result-card-content">
          <template>
            <div>{{ lastFinishEvent.title }}</div>
            <div>Découvrez le vainqueur</div>
          </template>
        </div>
      </v-card>
    </v-col>
  </div>
</template>

<script>
async function fetch(context) {
  await context.$store.dispatch('events/FETCH_EVENTS', { page: context.page, mode: 'active' })
  await context.$store.dispatch('events/FETCH_LAST_FINISH_EVENT')
  if (context.lastFinishEvent) {
    const result = await this.$axios.get('storyVote/podium/' + context.lastFinishEvent.id)
    context.winner = result.data[0]
  }
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
    },
    lastFinishEvent () {
      console.log(this.$store.state.events.lastFinishEvent)
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

</style>
