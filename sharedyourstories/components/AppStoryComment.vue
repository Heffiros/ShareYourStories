<template>
   <v-sheet
    elevation="12"
    max-width="600"
    rounded="lg"
    width="100%"
    class="pa-4 text-center mx-auto"
  >
    <v-icon
      class="mb-5"
      color="success"
      icon="mdi-check-circle"
      size="112"
    ></v-icon>

    <p class="mb-4 text-medium-emphasis text-body-2">
      {{ storyComment.text }}
    </p>

    <v-divider class="mb-4"></v-divider>

    <div class="text-end date">
      <span>{{ storyCommentDisplayDate }}</span>
    </div>
  </v-sheet>
</template>

<script>
export default {
  props: {
    storyComment: {
      type: Object,
      default: null
    }
  },
  computed: {
    storyCommentDisplayDate () {
      const date = new Date(this.storyComment.dateCreated)
      const day = date.getDate().toString().padStart(2, '0')
      const month = (date.getMonth() + 1).toString().padStart(2, '0')
      const year = date.getFullYear().toString()
      return `${day}/${month}/${year}`
    }
  }
}
</script>

<style>
.date {
  padding: 4px;
  font-size: 10px
}
</style>
