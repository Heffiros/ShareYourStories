<template>
  <div>
    <v-row>
      <v-col cols="3" offset="9">
        <v-text-field
          v-model="searchText"
          class="searchField"
          label="Recherche"
          outlined
          dense
          @input="search"
        ></v-text-field>
        <v-btn class="resetButton" v-if="searchText" color="info" height="38" @click="reset">
          <v-icon>mdi-delete</v-icon>
        </v-btn>
      </v-col>
    </v-row>

    <v-row v-if="storyTags">
      <div v-for="item in storyTags" :key="item.id" class="item" @click="searchWithStoryTagsFilter(item.id)">
        <div class="item-content">
          {{ item.label }}
        </div>
      </div>
    </v-row>

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
  </div>
</template>

<script>
import AppStory from '~/components/AppStory'

async function fetch(context) {
  let data = { page: context.page }
  if (context.eventId) {
    data.eventId = context.eventId
  }

  if (context.storyTagSelected) {
    data.storyTagId = context.storyTagSelected
    context.storyTagSelected = null
  }

  data.search = context.searchText
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
      hasMore: true,
      searchText: '',
      storyTags: null,
      storyTagSelected: null
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
  async mounted () {
    this.$store.dispatch('stories/RESET_STORIES')
    const result = await this.$axios.get('storyTags/library')
    if (result.data) {
      this.storyTags = result.data
      this.storyTags.unshift({
        id: null,
        label: 'Tous'
      })
    }
    this.page = 0
    fetch(this)
  },
  methods: {
    loadMore () {
      fetch(this)
    },
    vote (storyId) {
      this.$emit('voted', storyId)
    },
    async search () {
      if (this.searchText && this.searchText.length >= 3) {
        await this.$store.dispatch('stories/RESET_STORIES')
        this.page = 0
        await fetch(this)
      }
    },
    async reset () {
      await this.$store.dispatch('stories/RESET_STORIES')
      this.page = 0
      this.searchText = ''
      await fetch(this)
    },
    async searchWithStoryTagsFilter (id) {
      await this.$store.dispatch('stories/RESET_STORIES')
      this.page = 0
      this.storyTagSelected = id
      await fetch(this)
    }
  }
}
</script>

<style>
.searchField {
  display: inline-block;
}

.resetButton {
  vertical-align: middle;
  display: inline-block;
}

.item {
  display: inline-block;
  margin: 8px;
  background-color: #4CAF50;
  border-radius: 10px;
  padding: 10px;
  margin-bottom: 10px;
}

/*
Le important là c'est parce que cette classe existe déjà dans AppStoryTagsResearcher.
Todo Mettre stylus ou autre pour compilé le nom de classe
*/
.item:hover {
  background-color: #22552b !important;
  cursor: pointer;
}

.item-content {
  color: white;
}

</style>
