<template>
    <v-container class="bg-surface-variant">
      <app-stories-feed :is-moderation="true"/>
    </v-container>
  </template>
  
  <script>
  import AppStoriesFeed from '~/components/AppStoriesFeed'
  export default {
    layout: 'admin',
    middleware: 'admin-middleware',
    components: {
      AppStoriesFeed
    },
    data() {
      return {
      }
    },
    computed: {
    },
    methods: {
    },
    mounted () {
    }
  }
  </script>
  
  <style>