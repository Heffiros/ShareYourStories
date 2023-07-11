<template>
  <div>
    <v-row>
      <v-col class="text-right">
        <v-col cols="auto">
          <v-btn
            size="small"
            color="info"
            @click="dialog = true"
            >
            Créer une histoire
          </v-btn>
          <v-dialog v-model="dialog" max-width="40%">
            <create-story @created="dialog = false"/>
          </v-dialog>
        </v-col>
      </v-col>
    </v-row>

    <app-stories-feed/>
  </div>
</template>

<script>
import createStory from '~/components/popups/CreateStory'
import AppStoriesFeed from '~/components/AppStoriesFeed'

export default {
  components: {
    createStory,
    AppStoriesFeed
  },
  data () {
    return {
      dialog: false
    }
  }
}
</script>

