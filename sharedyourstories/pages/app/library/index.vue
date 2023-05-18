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
      <v-col v-for="(story, index) in stories" :key="index" cols="12" sm="6" md="4" lg="3">
        <v-card>
          <v-img :src="story.coverUrl" height="200"></v-img>
          <v-card-title>{{ story.title }}</v-card-title>
        </v-card>
      </v-col>
    </v-row>
  </div>
</template>

<script>
import createStory from '~/components/popups/CreateStory'

function fetch(context) {
  context.$store.dispatch('stories/FETCH_STORIES', { page: context.page })
  context.page++
}
export default {
  components: {
    createStory
  },
  data () {
    return {
      dialog: false,
      page: 0
    }
  },
  computed: {
    stories () {
      return this.$store.state.stories.stories
    }
  },
  mounted () {
    fetch(this)
  }
}
</script>

