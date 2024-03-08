<template>
    <div>
      <v-row>
        <v-col v-for="(event, index) in events" :key="index" cols="12" sm="6" md="4" lg="3">
          <app-event :event="event" />
        </v-col>
      </v-row>
  
      <v-row>
        <v-col cols="auto">
          <v-btn
            :disabled="!hasMore"
            size="large"
            color="info"
            @click="loadMore"
            >
            Charger plus
          </v-btn>
        </v-col>
      </v-row>
    </div>
  </template>
  
  <script>
  import AppEvent from '~/components/AppEvent'
  
  async function fetch(context) {
    let data = { page: context.page, mode: "full"}
    await context.$store.dispatch('events/RESET_EVENTS', data)
    const currentNbEvents = context.events.length
    if (currentNbEvents % 5 != 0) {
      context.hasMore = false
    }
    context.page++
  }
  export default {
    components: {
        AppEvent
    },
    data () {
      return {
        page: 0,
        hasMore: true,
      }
    },
    computed: {
      events () {
        return this.$store.state.events.events
      }
    },
    async mounted () {
      this.$store.dispatch('events/RESET_EVENTS')
      this.page = 0
      fetch(this)
    },
    methods: {
      loadMore () {
        fetch(this)
      },
      async reset () {
        await this.$store.dispatch('events/RESET_EVENTS')
        this.page = 0
        await fetch(this)
      }
    }
  }
  </script>
  
  <style>
  .searchField {
    display: inline-block;
    padding-top: 10px !important;
  }
  
  .resetButton {
    display: inline-block;
  }
  
  .item {
    display: inline-block;
    margin: 8px;
    background-color: #4CAF50;
    border-radius: 10px;
    padding: 10px;
    margin-bottom: 10px;
    height: 45px;
  }
  
  .tmp {
    display: inline-block;
  }
  /*
  Le important là c'est parce que cette classe existe déjà dans AppStoryTagsResearcher.
  Todo Mettre stylus ou autre pour compilé le nom de classe
  */
  .item:hover {
    background-color: #22552b !important;
    cursor: pointer;
  }
  
  .item-content {
    color: white;
  }
  
  </style>
  