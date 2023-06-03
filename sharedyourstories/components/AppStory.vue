<template>
  <v-card>
    <v-img :src="story.coverUrl" height="200"></v-img>
    <v-card-title>{{ story.title }}</v-card-title>
    <v-card-actions>
      <nuxt-link :to="`/app/story/${story.id}/reader`">
        <v-btn color="info" icon>
          <v-icon>mdi-book-open</v-icon>
        </v-btn>
      </nuxt-link>

      <v-btn v-if="eventId" color="info" @click="vote" icon>
        <v-icon v-if="currentUserHasVotedForThisStory">mdi-star</v-icon>
        <v-icon v-else>mdi-star-outline</v-icon>
      </v-btn>

      <nuxt-link :to="`/app/story/${story.id}`" class="ml-auto">
        <v-btn color="info" icon>
          <v-icon>mdi-magnify</v-icon>
        </v-btn>
      </nuxt-link>
    </v-card-actions>
  </v-card>
</template>

<script>

export default {
  props: {
    eventId: {
      type: Number,
      default: 0
    },
    story: {
      type: Object,
      default: 0
    },
    storyVotes: {
      type: Array,
      default: null
    }
  },
  computed: {
    currentUserHasVotedForThisStory () {
      if (this.storyVotes) {
        return this.storyVotes.find(sv => sv.storyId == this.story.id)
      }
    }
  },
  methods: {
    vote () {
      if (!this.currentUserHasVotedForThisStory) {
        this.$emit('voted', this.story.id)
      }
    }
  }
}
</script>
