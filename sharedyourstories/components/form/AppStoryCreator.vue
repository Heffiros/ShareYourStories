<template>
  <v-card class="popup">
    <v-card v-if="step === 1">
      <v-card-title>
        Étape 1: Information
      </v-card-title>
      <v-card-text>
        <v-form @submit.prevent="nextStep">
          <v-text-field
            v-model="story.title"
            label="Nom"
            required
          />
          <app-image-uploader v-model="story.coverUrl"/>
          <!-- Todo rajouter ici le choix de l'équipe quand on pourra-->
          <v-btn type="submit" color="primary">Suivant</v-btn>
        </v-form>
      </v-card-text>
    </v-card>
    <v-card v-if="step === 2">
      <v-card-title>
        Étape 2 : Ajouter vos tags
      </v-card-title>
      <v-card-text>
        <v-form @submit.prevent="nextStep">
          <app-story-tags-researcher v-model="story.storyTags"/>
          <v-btn type="submit" color="primary">Suivant</v-btn>
        </v-form>
      </v-card-text>
    </v-card>
    <v-card v-if="step === 3">
      <v-card-title>
        Étape 3: Il est temps de nous partager votre récit
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
import AppStoryTagsResearcher from '~/components/AppStoryTagsResearcher'
import AppImageUploader from '~/components/form/AppImageUploader'

export default {
  components : {
    AppStoryTagsResearcher,
    AppImageUploader
  },
  props: {
    eventId: {
      type: Number,
      default: 0
    }
  },
  data() {
    return {
      dialog: false,
      step: 1,
      story: {
        title: null,
        coverUrl: 'https://png.pngtree.com/png-clipart/20200826/original/pngtree-colorful-warm-autumn-book-cover-png-image_5490983.jpg',
        eventId: null,
        storyTags: null
      },
      file: null
    }
  },
  methods: {
    nextStep() {
      this.step += 1
    },
    async submitForm() {
      try {
        const formData = new FormData()
        formData.append("file", this.file)
        if (this.eventId) {
          this.story.eventId = this.eventId
        }
        formData.append("story", JSON.stringify(this.story))

        await this.$axios.post("stories", formData, {
          headers: {
            "Content-Type": "multipart/form-data"
          }
        })
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
