<template>
    <v-container class="bg-surface-variant">
      <app-badge-creator @created="backToBadgeDashboard"/>
    </v-container>
  </template>
  
  <script>
    import AppBadgeCreator from '~/components/form/AppBadgeCreator'
    export default {
      layout: 'admin',
      middleware: 'admin-middleware',
      components: {
        AppBadgeCreator
      },
      methods: {
        backToBadgeDashboard () {
          this.$router.push('/app/admin/gamification')
        }
      }
    }
    </script>