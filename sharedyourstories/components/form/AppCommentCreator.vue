<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <v-text-field
          v-model="message"
          :append-icon="message ? 'mdi-send' : ''"
          variant="filled"
          clear-icon="mdi-close-circle"
          clearable
          label="Partagez vos impressions"
          type="text"
          @click:append="sendMessage"
          @click:clear="clearMessage"
        ></v-text-field>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
  export default {
    data: () => ({
      message: null,
    }),
    methods: {
      sendMessage () {
        if (this.message && this.message.length) {
          this.$emit('send', this.message)
          this.clearMessage()
        }
      },
      clearMessage () {
        this.message = ''
      }
    }
  }
</script>
