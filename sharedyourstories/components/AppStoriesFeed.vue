<template>
  <v-row>
      <v-row>
        <v-col v-for="(story, index) in stories" :key="index" cols="12" sm="6" md="4" lg="3">
          <app-story :story="story" :storyVotes="storyVotes" :eventId="eventId" @voted="vote" />
        </v-col>
      </v-row>

      <v-row>
        <v-col cols="auto">
          <v-btn
            :disabled="!hasMore"
            size="large"
            color="info"
            @click="loadMore"
            >
            Charger plus
          </v-btn>
        </v-col>
      </v-row>
    </v-row>
</template>

<script>
import AppStory from '~/components/AppStory'

async function fetch(context) {
  let data = { page: context.page }
  if (context.eventId) {
    data.eventId = context.eventId
  }
  await context.$store.dispatch('stories/FETCH_STORIES', data)
  const currentNbStories = context.stories.length
  if (currentNbStories % 5 != 0) {
    context.hasMore = false
  }
  context.page++
}
export default {
  components: {
    AppStory
  },
  data () {
    return {
      page: 0,
      hasMore: true
    }
  },
  props: {
    eventId: {
      type: Number,
      default: 0
    },
    storyVotes: {
      type: Array,
      default: null
    }
  },
  computed: {
    stories () {
      return this.$store.state.stories.stories
    }
  },
  mounted () {
    this.$store.dispatch('stories/RESET_STORIES')
    this.page = 0
    fetch(this)
  },
  methods: {
    loadMore () {
      fetch(this)
    },
    vote (storyId) {
      this.$emit('voted', storyId)
    }
  }
}
</script>
