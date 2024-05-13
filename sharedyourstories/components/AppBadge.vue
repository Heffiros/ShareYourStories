<template>
  <v-card
    class="mx-auto"
    max-width="344"
  >
    <v-img
      height="200px"
      :src="badge.userBadges ? badge.badgeUrl : badge.emptyBadgeUrl"
      cover
    ></v-img>

    <v-card-title class="badge-title">
      {{  badge.label }}
    </v-card-title>
  </v-card>
</template>

<script>
export default {
  props: {
    badge: {
      type: Object,
      default: null
    }
  },
  data () {
    return {
      show: false
    }
  }
}
</script>

<style>
.badge-title {
  text-align: center;
}
</style>