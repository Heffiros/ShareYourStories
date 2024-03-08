<template>
    <v-container class="bg-surface-variant">
      Hello Event
    </v-container>
  </template>
  
  <script>
  export default {
    layout: 'admin',
    components: {
      
    },
    data() {
      return {
        page: 0
      }
    },
    computed: {
    },
    methods: {
    },
    mounted () {
      this.$store.dispatch('events/FETCH_EVENTS', { page: this.page, mode: 'full' })
    }
  }
  </script>
  
  <style>