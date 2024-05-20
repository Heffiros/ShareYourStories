<template>
  <v-container>
    <v-card class="badge-panel">
      <v-card-title class="badge-title">
        Badges
      </v-card-title>
      <v-card-subtitle class="badge-subtitle">
        Découvre ce qu'il te reste à débloquer
      </v-card-subtitle>
      <v-divider></v-divider>
      <div class="badge-content">
        <v-row>
          <v-col v-for="(badge, index) in badges" :key="badge.id" cols="12" sm="6">
            <app-badge :badge="badge" />
          </v-col>
        </v-row>
      </div>
    </v-card>
  </v-container>
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
  data() {
    return {
      page: 0
    }
  },
  computed: {
    currentUser() {
      return this.$auth.user
    },
    badges() {
      return this.$store.state.badges.badges
    }
  },
  mounted() {
    this.$store.dispatch('badges/RESET_BADGES')
    this.page = 0
    fetch(this)
  }
}
</script>

<style scoped>
.badge-panel {
  background-color: #3d4242;
  border-radius: 8px;
  padding: 16px;
  max-height: 500px;
  overflow-y: auto;
}

.badge-title {
  font-size: 24px;
  text-align: center;
  width: 100%;
}

.badge-subtitle {
  font-size: 18px;
  font-weight: bold;
  text-align: center;
  width: 100%;
}

.badge-content {
  padding-top: 16px;
}
</style>
