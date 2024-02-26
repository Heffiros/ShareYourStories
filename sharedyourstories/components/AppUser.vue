<template>
  <div class="userInfo">
    <v-avatar size="24">
      <v-img cover :src="user.profilePictureUrl" />
    </v-avatar>
    <span class="authorName">{{ user.authorName }}</span>
  </div>
</template>


<script>

export default {
  props: {
    user: {
      type: Object,
      default: null
    }
  }
}
</script>
