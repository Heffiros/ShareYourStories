<template>
  <div>
    <v-row>
      <v-col class="text-right">
        <v-col cols="auto">
          <v-btn
            size="small"
            color="info"
            @click="goToEventCreation"
            >
            Créer un événement
          </v-btn>
        </v-col>
      </v-col>
    </v-row>

    <app-event-feed />
  </div>
</template>

<script>
import createStory from '~/components/popups/CreateStory'
import AppEventFeed from '~/components/AppEventFeed'

export default {
  components: {
    createStory,
    AppEventFeed
  },
  data () {
    return {
      page: 0
    }
  },
  mounted () {
    this.$store.dispatch('events/RESETS_EVENT')
  },
  methods: {
    goToEventCreation () {
      this.$router.push('/app/admin/event/add')
    }
  }
}
</script>

