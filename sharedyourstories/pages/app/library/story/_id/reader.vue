<template>
  <div>
    <app-reader :story="story" ></app-reader>
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
  }
}
</script>
