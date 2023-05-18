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
      console.log(this.$store.state.stories.stories[2])
      return this.$store.state.stories.stories[2]
    }
  }
}
</script>
