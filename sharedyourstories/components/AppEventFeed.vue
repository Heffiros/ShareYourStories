<template>
    <div>
      <v-row>
        <v-simple-table>
          <template v-slot:default>
            <thead>
              <tr>
                <th class="text-left">
                  Name
                </th>
                <th class="text-left">
                  Date de début
                </th>
                <th class="text-left">
                  Date de fin
                </th>
                <th class="text-left">
                  Nombre de participation
                </th>
                <th class="text-left">
                  Actions
                </th>
              </tr>
            </thead>
            <tbody>
              <tr
                v-for="item in events"
                :key="item.id"
                :class="new Date(item.dateEnd) < today ? 'tr-end' : 'tr-ongoing'"
                >
                <td>{{ item.title }}</td>
                <td>{{ item.dateBegin }}</td>
                <td>{{ item.dateEnd }}</td>
                <td>{{ item.nbStories }}</td>
                <td>
                  <v-btn
                    size="medium"
                    color="#1565C0"
                    @click="$router.push('/app/event/' + item.id)"
                    >
                    <v-icon>mdi-arrow-right-bold-outline</v-icon>
                  </v-btn>
                </td>
              </tr>
            </tbody>
          </template>
        </v-simple-table>
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
    await context.$store.dispatch('events/FETCH_EVENTS', data)
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
        today: new Date()
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
      }
    }
  }
  </script>
  
  <style>
  .tr-end {
    background-color: #EF5350;
  }

  .tr-end:hover {
    background-color: #E53935!important;
  }

  .tr-ongoing {
    background-color: #43A047;
  }

  .tr-ongoing:hover {
    background-color: #388E3C!important;
  }
  
  </style>
  