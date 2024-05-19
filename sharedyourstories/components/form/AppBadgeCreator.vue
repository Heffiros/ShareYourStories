<template>
    <v-card class="popup">
      <v-card>
        <v-card-title>
          Créer un nouveau concours
        </v-card-title>
        <v-card-text>
          <v-form @submit.prevent="submitForm">
            <v-text-field
              v-model="badge.title"
              label="Titre"
              required
            />
            <v-textarea
              v-model="badge.rules"
              label="Description"
              required
            />
            <app-image-uploader v-model="badge.badgeUrl" :place="'badge'"/>
            <app-image-uploader v-model="badge.emptyBadgeUrl" :place="'badge'"/>
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
        badge: {
          label: null,
          badgeUrl: null,
          emptyBadgeUrl: null,
          description: null
        }
      }
    },
    methods: {
      async submitForm() {
        try {
          await this.$axios.post('badges' , this.badge)
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
  