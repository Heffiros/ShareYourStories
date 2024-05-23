<template>
  <v-card
    class="mx-auto"
  >
    <v-toolbar
      color="grey darken-3"
      dark
    >
      <v-toolbar-title>Reprennez vos lectures</v-toolbar-title>
      <v-spacer></v-spacer>
    </v-toolbar>

    <v-list three-line>
      <template v-for="(item, index) in list">
        <v-list-item
          :key="item.title"
          @click="$router.push('/app/story/' + item.id + '/reader')"
        >
          <v-list-item-avatar>
            <v-img :src="item.cover"></v-img>
          </v-list-item-avatar>

          <v-list-item-content>
            <v-list-item-title v-html="item.title"></v-list-item-title>
            <v-progress-linear :value="item.progression"></v-progress-linear>
          </v-list-item-content>
        </v-list-item>
      </template>
      <infinite-loading :identifier="infiniteId" @infinite="infiniteHandler"></infinite-loading>
    </v-list>
  </v-card>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      page: 0,
      list: [],
      infiniteId: + new Date()
    }               
  },
  methods: {
    infiniteHandler($state) {
      this.$axios.get('storyHistories', { params: { page: this.page } })
      .then(({ data }) => {
        if (data.length > 0) {
          this.page += 1
          const mappedData =  data.map(item => {
            return {
              cover: item.story.coverUrl,
              title: item.story.title,
              id: item.storyId,
              progression: (item.progression.currentPagesIndex + 1 )* 100 / item.progression.maxNbPages
            }
          })
          this.list.push(...mappedData)
          $state.loaded()
        } else {
          $state.complete()
        }
      })
    },
  }
}
</script>
