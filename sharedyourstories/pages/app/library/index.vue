<template>
  <div>
    <v-row>
      <v-col class="text-right">
        <v-col cols="auto">
          <v-btn
            size="small"
            color="info"
            @click="dialog = true"
            >
            Créer une histoire
          </v-btn>
          <v-dialog v-model="dialog" max-width="40%">
            <create-story @uploaded="dialog = false"/>
          </v-dialog>
        </v-col>
      </v-col>
    </v-row>

    <v-row>
      <v-row>
        <v-col v-for="(story, index) in stories" :key="index" cols="12" sm="6" md="4" lg="3">
          <v-card>
            <v-img :src="story.coverUrl" height="200"></v-img>
            <v-card-title>{{ story.title }}</v-card-title>
            <v-card-actions>
              <nuxt-link :to="`/app/library/story/${story.id}/reader`" class="ml-auto">
                <v-btn color="success" icon>
                  <v-icon>mdi-magnify</v-icon>
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
  </div>
</template>

<script>
import createStory from '~/components/popups/CreateStory'

async function fetch(context) {
  await context.$store.dispatch('stories/FETCH_STORIES', { page: context.page })
  const currentNbStories = context.stories.length
  if (currentNbStories % 5 != 0) {
    context.hasMore = false
  }
  context.page++
}
export default {
  components: {
    createStory
  },
  data () {
    return {
      dialog: false,
      page: 0,
      hasMore: true
    }
  },
  computed: {
    stories () {
      return this.$store.state.stories.stories
    }
  },
  mounted () {
    fetch(this)
  },
  methods: {
    loadMore () {
      fetch(this)
    }
  }
}
</script>

