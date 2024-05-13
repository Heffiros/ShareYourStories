<template>
  <v-container>
    <v-row class="justify-center">
      <!-- Colonne 1 -->
      <v-col cols="3">
        <v-list v-if="story && currentUser.id === story.userId" class="scrollList">
          <v-list-item
            v-for="(item, i) in items"
            :key="i"
            :to="item.to"
            router
            exact
          >
            <v-list-item-action>
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>{{ item.title }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list>
        <v-sheet v-if="story && story.user" class="pa-4 authorContainer d-flex flex-column align-center">
          <v-avatar size="40">
            <v-img cover :src="currentUser.profilePictureUrl"></v-img>
          </v-avatar>
          <div class="mt-2 authorName">{{ story.user.authorName }}</div>
        </v-sheet>
      </v-col>

      <!-- Colonne 2 -->
      <v-col cols="8">
        <app-comment-creator @send="sendComment"/>
        <div class="scrollContainer">
          <div v-for="(item, $index) in list" :key="$index">
            <app-story-comment class="comment" :storyComment="item"/>
          </div>

          <infinite-loading :identifier="infiniteId" @infinite="infiniteHandler"></infinite-loading>
        </div>
      </v-col>

      <!-- Colonne 3 -->
      <v-col cols="1" class="d-flex justify-center align-center">

      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import AppStoryComment from '~/components/AppStoryComment'
import AppCommentCreator from '~/components/form/AppCommentCreator'

export default {
  components: {
    AppStoryComment,
    AppCommentCreator
  },
  data() {
    return {
      page: 0,
      list: [],
      infiniteId: +new Date(),
      storyId: 0
    }
  },
  computed: {
    currentUser () {
      return this.$auth.user
    },
    story () {
      return this.$store.getters['stories/getStoryById'](parseInt(this.storyId))
    },
    items () {
      return  [
        {
          icon: 'mdi-chart-line',
          title: 'Statisques',
          to: '/app/story/' + this.storyId + '/statistics'
        },
        {
          icon: 'mdi-cog',
          title: 'Editer les informations',
          to: '/app/story/' + this.storyId + '/update'
        }
      ]
    }
  },
  methods: {
    infiniteHandler($state) {
      this.$axios.get('storyComments', { params: { page: this.page, storyId: this.storyId } })
      .then(({ data }) => {
        if (data.length > 0) {
          this.page += 1
          this.list.push(...data)
          $state.loaded()
        } else {
          $state.complete()
        }
      })
    },
    async sendComment (text) {
      const comment = {
          storyId: this.storyId,
          text: text
      }
      await this.$axios.post('storyComments' , comment).then(({ data }) => {
        comment.user = this.$auth.user
        comment.dateCreated = new Date()
        this.list.unshift(comment)
      })
    }
  },
  async mounted () {
    this.storyId = parseInt(this.$route.params.id)
    if (!this.$store.getters['stories/getStoryById'](parseInt(this.storyId))) {
      await this.$store.dispatch('stories/FETCH_STORY', {id : parseInt(this.storyId)})
    }
  }
}
</script>

<style>
.authorContainer {
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  margin-top: 24px
}

.authorName {
  padding-top: 4px
}

.scrollContainer {
  height: 700px;
  overflow: auto;
  padding-right: 8px;
}

.comment {
  margin-top: 24px;
}

.scrollContainer::-webkit-scrollbar {
  width: 4px;
  background-color: transparent;
}

.scrollContainer::-webkit-scrollbar-thumb {
  background-color: #424242;
  border-radius: 10px;
}

.scrollContainer::-webkit-scrollbar-track {
  background-color: transparent;
}

.scrollList {
  margin-top: 150px;
}
</style>
