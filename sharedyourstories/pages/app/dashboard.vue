<template>
  <v-carousel>
    <v-carousel-item v-for="(event, index) in events" :key="index">
      <!-- Image du carrousel -->
      <v-img :src="event.coverUrl" :alt="event.title"></v-img>

      <!-- En-tête avec titre transparent -->
      <div class="carousel-header">
        <h2 class="carousel-title">{{ event.title }}</h2>
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
  mounted () {
    fetch (this)
  }
};
</script>


<style>
.carousel-header {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  background-color: rgba(0, 0, 0, 0.3); /* Couleur de fond transparente */
  padding: 16px;
}

.carousel-title {
  color: #ffffff; /* Couleur du texte de titre */
  margin: 0;
}
</style>
