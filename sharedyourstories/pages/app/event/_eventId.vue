<template>
  <div>
    <v-tabs
      fixed-tabs
      bg-color="indigo-darken-2"
      v-model="activeTab"
    >
      <v-tab v-for="(tab, index) in tabs" :key="index" :disabled="event && event.hasAlreadyParticipate && index === 1">
          {{ tab.label }}
      </v-tab>
    </v-tabs>

    <template v-if="event">
      <div v-if="activeTab === 0" class="col-12">
        <div class="eventTitle text-center">
          <h2 class="carousel-title">{{ event.title }}</h2>
        </div>
        <app-stories-feed :event-id="event.id" @voted="vote"/>
      </div>
      <div v-if="activeTab === 1" class="col-12">
        <app-story-creator :event-id="event.id" @created="activeTab = 0"/>
      </div>
      <div v-if="activeTab === 2" class="col-12">
        <div class="eventTitle text-center">
          <h2 class="carousel-title">{{ event.title }}</h2>
        </div>
        <div>
          <app-podium :event-id="event.id" :event-date-end="event.dateEnd"/>
        </div>
      </div>
    </template>
  </div>
</template>

<script>
import appStoryCreator from '~/components/form/AppStoryCreator'
import AppPodium from '~/components/AppPodium'

export default {
  components: {
    appStoryCreator,
    AppPodium
  },
  data() {
    return {
      activeTab: 0,
      tabs: [],
      storyVotes: [],
      storyVotesAvaiable: 0
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

    if (this.$auth.loggedIn) {
      const result = await this.$axios.get('storyVote/event/'+ parseInt(this.$route.params.eventId) +'/avaiable')
      if (result.data)
      {
        this.storyVotes = result.data
        this.storyVotesAvaiable = 3 - this.storyVotes.length
      }
    }

    this.tabs = [
      { label: 'Découvrir les récits ', value: 0 },
      { label: this.event && !this.event.hasAlreadyParticipate ? 'Créer mon histoire' : 'Vous avez déjà participé à cet event', value: 1 },
      { label: 'Suivez les résultats', value: 2 }
    ]
  },
  methods: {
    async vote (storyId) {
      try {
        await this.$axios.post('storyVote/event/'+ this.event.id + '/story/' + parseInt(storyId))
        this.storyVotesAvaiable--
        this.storyVotes = this.storyVotes.concat({userId: this.$auth.user.id, storyId: storyId})
      } catch (error) {
        this.$toast.error('Vous avez déjà voté pour ce récit', {  timeout: 2000 })
      }

    }
  }
}
</script>

<style>
  .eventTitle {
    margin: 16px;
    font-size: 24px;
  }
</style>
