<template>
  <v-card class="popup">
    <v-card v-if="step === 1">
      <v-card-title>
        Étape 1
      </v-card-title>
      <v-card-text>
        <v-form @submit.prevent="nextStep">
          <v-text-field
            v-model="story.title"
            label="Nom"
            required
          />
          <!-- Todo rajouter ici le choix de l'équipe quand on pourra-->
          <v-btn type="submit" color="primary">Suivant</v-btn>
        </v-form>
      </v-card-text>
    </v-card>
    <v-card v-if="step === 2">
      <v-card-title>
        Étape 2
      </v-card-title>
      <v-card-text>
        <v-form @submit.prevent="submitForm">
          <v-file-input
            v-model="file"
            accept=".docx"
            label="Sélectionner un fichier"
            outlined
          />
          <v-btn type="submit" color="primary">Créer</v-btn>
        </v-form>
      </v-card-text>
    </v-card>
  </v-card>
</template>

<script>
export default {
  data() {
    return {
      dialog: false,
      step: 1,
      story: {
        title: null
      },
      file: null
    }
  },
  methods: {
    nextStep() {
      this.step = 2;
    },
    async submitForm() {
      try {
        const formData = new FormData();
        formData.append("file", this.file);
        formData.append("title", this.story.title);

        const response = await this.$axios.post("stories", formData, {
          headers: {
            "Content-Type": "multipart/form-data"
          }
        });

        console.log(response.data);
      } catch (error) {
        console.error(error);
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
