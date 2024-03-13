<template>
  <div class="text-center">
    <v-menu
      v-model="menu"
      :close-on-content-click="false"
      :nudge-width="200"
      offset-x
    >
      <template v-slot:activator="{ on, attrs }">
        <v-btn
          color="indigo"
          dark
          v-bind="attrs"
          v-on="on"
        >
          Modération
        </v-btn>
      </template>

      <v-card>
        <v-list>
          <v-list-item>
            <v-list-item-avatar>
              <img
                :src="story.user.profilePictureUrl"
                :alt="story.user.authorName"
              >
            </v-list-item-avatar>

            <v-list-item-content>
              <v-list-item-title>{{ story.user.authorName }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list>
        <v-divider></v-divider>

        <v-list>
          <v-list-item>
            <v-list-item-action>
              <v-btn
                color="green"
                text
                @click="updateStatus(1)"
                >
                Valider
              </v-btn>
              <v-btn
                color="red"
                text
                @click="updateStatus(5)"
                >
                Refuser
              </v-btn>
            </v-list-item-action>
          </v-list-item>
        </v-list>
      </v-card>
    </v-menu>
  </div>
</template>

<script>
export default {
  data () {
    return {
      displayMenuModeration: false,
      menu: false
    }
  },
  props: {
    story: {
      type: Object,
      default: null
    }
  },
  methods: {
    async updateStatus (status) {
      try {
        let storyToUpdate = {...this.story}
        storyToUpdate.status = status
        await this.$axios.put('stories' , storyToUpdate)
        this.$emit('updated')
      } catch (error) {
        console.error(error)
      }
    }
  }
}
</script>