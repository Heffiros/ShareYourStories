<template>
    <v-card class="popup">
      <v-card>
        <v-card-title>
          Créer un nouveau concours
        </v-card-title>
        <v-card-text>
          <v-form @submit.prevent="submitForm">
            <v-text-field
              v-model="event.title"
              label="Titre"
              required
            />
            <v-textarea
              v-model="event.rules"
              label="Règles de l'event"
              required
            />
            <v-date-picker v-model="event.dateBegin" color="green lighten-1"></v-date-picker>
            <v-date-picker v-model="event.dateEnd" color="red lighten-1"></v-date-picker>
            <app-image-uploader v-model="event.coverUrl" :place="'event'"/>
            <!-- Todo rajouter ici le choix de l'équipe quand on pourra-->
            <v-btn type="submit" color="primary">Suivant</v-btn>
          </v-form>
        </v-card-text>
      </v-card>
    </v-card>
  </template>
  
  <script>
  import AppImageUploader from '~/components/form/AppImageUploader'
  
  export default {
    components : {
      AppImageUploader
    },
    data() {
      return {
        dialog: false,
        event: {
          title: null,
          coverUrl: 'https://png.pngtree.com/png-clipart/20200826/original/pngtree-colorful-warm-autumn-book-cover-png-image_5490983.jpg',
          rules: null,
          dateBegin: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
          dateEnd: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10)
        }
      }
    },
    methods: {
      async submitForm() {
        try {
          await this.$axios.post('events' , this.event)
          this.$emit('created')
        } catch (error) {
          console.error(error)
        }
      }
    }
  }
  </script>
  
  
  <style scoped>
  .popup {
    background-color: #fff;
    color: #000000 !important;
  }
  </style>
  