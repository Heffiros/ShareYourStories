<template>
  <v-container>
    <v-row class="justify-center">
      <!-- Colonne 1 -->
      <v-col cols="2" class="d-flex justify-center align-center" color="red">

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
      <v-col cols="2" class="d-flex justify-center align-center">

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
    };
  },
  methods: {
    infiniteHandler($state) {
      this.$axios.get('storyComments', { params: { page: this.page, storyId: this.storyId } })
      .then(({ data }) => {
        if (data.length > 0) {
          this.page += 1;
          this.list.push(...data);
          $state.loaded();
        } else {
          $state.complete();
        }
      });
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
  mounted () {
    this.storyId = parseInt(this.$route.params.id)
  }
};
</script>

<style>

.scrollContainer {
  height: 750px;
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
</style>
