<template>
  <div>
    <div
      v-for="(badge, index) in badges"
      :key="badge.id" 
      :class="{ 'badge-card': index !== 0, 'block-inline': true }"
    >
      <app-badge :badge="badge" />
    </div>
  </div>  
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
.block-inline {
  display: inline-block;
  margin-top: 16px;
}

.badge-card {
  margin-left: 32px;
}
</style>