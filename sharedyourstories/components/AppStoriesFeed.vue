<template>
  <v-row>
      <v-row>
        <v-col v-for="(story, index) in stories" :key="index" cols="12" sm="6" md="4" lg="3">
          <v-card>
            <v-img :src="story.coverUrl" height="200"></v-img>
            <v-card-title>{{ story.title }}</v-card-title>
            <v-card-actions>
              <nuxt-link :to="`/app/library/story/${story.id}/reader`">
                <v-btn color="info" icon>
                  <v-icon>mdi-book-open</v-icon>
                </v-btn>
              </nuxt-link>
              <nuxt-link :to="`/app/library/story/${story.id}`" class="ml-auto">
                <v-btn color="info" icon>
                  <v-icon>mdi-magnify</v-icon>
                </v-btn>
              </nuxt-link>
            </v-card-actions>
          </v-card>
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
    }
  }
}
</script>
