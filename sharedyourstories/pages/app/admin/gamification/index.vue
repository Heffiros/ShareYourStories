<template>
    <div>
      <v-row>
        <v-col class="text-right">
          <v-col cols="auto">
            <v-btn
              size="small"
              color="info"
              @click="goToBadgeCreation"
              >
              Ajouter un badge
            </v-btn>
          </v-col>
        </v-col>
      </v-row>
  
      <app-badge-feed />
    </div>
  </template>
  
  <script>
  import AppBadgeFeed from '~/components/AppBadgeFeed'
  
  export default {
    layout: 'admin',
    middleware: 'admin-middleware',
    components: {
      AppBadgeFeed
    },
    methods: {
        goToBadgeCreation () {
        this.$router.push('/app/admin/gamification/add')
      }
    }
  }
  </script>
  
  