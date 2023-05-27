<template>
  <div>
    <v-tabs
      fixed-tabs
      bg-color="indigo-darken-2"
      v-model="activeTab"
    >
      <v-tab v-for="(tab, index) in tabs" :key="index" :disabled="tab.value === 1 && event && event.hasAlreadyParticipate">
          {{ tab.label }}
      </v-tab>
    </v-tabs>

    <div v-if="activeTab === 0" class="col-6">
      <h2 v-if="event" class="carousel-title">{{ event.title }}</h2>
    </div>
    <div v-if="activeTab === 1" class="col-12">
      <app-story-creator :event-id="event.id" @created="activeTab = 0"/>
    </div>
  </div>
</template>

<script>
import appStoryCreator from '~/components/form/AppStoryCreator'

export default {
  components: {
    appStoryCreator
  },
  data() {
    return {
      activeTab: 0,
      tabs: [
        { label: 'Découvrir les récits ', value: 0 },
        { label: this.event && this.event.hasAlreadyParticipate ? 'Créer mon histoire' : 'Vous avez déjà participé à cet event', value: 1 }
      ]
    }
  },
  computed: {
    event () {
      return this.$store.getters['events/getEventById'](parseInt(this.$route.params.eventId))
    }
  },
  async beforeMount() {
    if (!this.$store.getters['events/getEventById'](parseInt(this.$route.params.eventId))) {
      await this.$store.dispatch('events/FETCH_EVENT', { eventId: parseInt(this.$route.params.eventId) })
    }
  }
}
</script>
