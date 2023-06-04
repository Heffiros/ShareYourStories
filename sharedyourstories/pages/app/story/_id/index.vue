<template>
  <v-container>
    <v-row class="justify-center">
      <!-- Colonne 1 -->
      <v-col cols="2" class="d-flex justify-center align-center" color="red">

      </v-col>

      <!-- Colonne 2 -->
      <v-col cols="8">
        <div v-for="(item, $index) in list" :key="$index">
          <app-story-comment :storyComment="item"/>
        </div>

        <infinite-loading :identifier="infiniteId" @infinite="infiniteHandler"></infinite-loading>
      </v-col>

      <!-- Colonne 3 -->
      <v-col cols="2" class="d-flex justify-center align-center">

      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import AppStoryComment from '~/components/AppStoryComment'

export default {
  components: {
    AppStoryComment
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
    }
  },
  mounted () {
    this.storyId = parseInt(this.$route.params.id)
  }
};
</script>
