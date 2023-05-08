<template>
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
          <create-story @uploaded="dialog = false"/>
        </v-dialog>
      </v-col>
    </v-col>
  </v-row>
</template>

<script>
import createStory from '~/components/popups/CreateStory'

export default {
  components: {
    createStory
  },
  data () {
    return {
      dialog: false
    }
  }
}
</script>

