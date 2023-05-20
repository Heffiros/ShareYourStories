<template>
  <div>
    Hello
  </div>
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
