<template>
  <div>Hello <span v-if="currentUser">{{ currentUser.authorName }}</span> </div>
</template>

<script>

export default {
  computed: {
    currentUser () {
      return this.$auth.user
    }
  }
}
</script>
