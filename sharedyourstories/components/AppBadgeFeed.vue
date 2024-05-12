<template>
  <v-sheet class="d-flex justify-center align-center fill-height sheet-with-padding scrollContainer">
    <app-badge
      v-for="badge in badges"
      :key="badge.id"
    />
  </v-sheet>
</template>

<script>
import AppBadge from '~/components/AppBadge'

async function fetch(context) {
    let data = { page: context.page }
    await context.$store.dispatch('badges/FETCH_BADGES', data)
    const currentNbBadges = context.badges.length
    if (currentNbBadges % 5 != 0) {
      context.hasMore = false
    }
    context.page++
  }

export default {
  components: {
    AppBadge
  },
  data () {
    return {
        page: 0 
    }
  },
  computed: {
    currentUser () {
      return this.$auth.user
    },
    badges () {
        return this.$store.state.badges.badges
      }
  },
  methods: {
    
  },
  mounted () {
    this.$store.dispatch('badges/RESET_BADGES')
    this.page = 0
    fetch(this)
  }
}
</script>

<style>
.sheet-with-padding {
  padding: 10px;
}

.full-form {
  width: 100%;
}
</style>