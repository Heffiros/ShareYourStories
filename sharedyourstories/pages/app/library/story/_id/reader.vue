<template>
  <div>
    <app-reader v-if="story" :story="story" ></app-reader>
  </div>
</template>

<script>
import AppReader from '~/components/AppReader'
export default {
  components: {
    AppReader
  },
  computed: {
    story () {
      return this.$store.getters['stories/getStoryById'](parseInt(this.$route.params.id))
    }
  },
  async mounted () {
    if (!this.$store.getters['stories/getStoryById'](parseInt(this.$route.params.id))) {
      await this.$store.dispatch('stories/FETCH_STORY', {id : parseInt(this.$route.params.id)})
    }
  }
}
</script>
