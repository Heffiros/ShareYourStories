<template>
    <v-container class="bg-surface-variant">
      <app-event-creator @created="backToEventDashboard"/>
    </v-container>
  </template>
  
  <script>
    import AppEventCreator from '~/components/form/AppEventCreator'
    export default {
      layout: 'admin',
      components: {
        AppEventCreator
      },
      methods: {
        backToEventDashboard () {
          this.$router.push('/app/admin/event')
        }
      }
    }
    </script>