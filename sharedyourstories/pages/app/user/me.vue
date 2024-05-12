<template>
  <v-container class="bg-surface-variant">
    <v-row no-gutters>
      <v-col cols="12">
        <div class="d-flex flex-column align-center justify-start">
          <v-avatar size="80">
            <v-img cover :src="currentUser.profilePictureUrl"></v-img>
          </v-avatar>
        </div>
      </v-col>
      <v-row no-gutters>
        <v-col cols="12" class="col-12 mx-auto">
          <div class="d-flex flex-column align-center justify-center fill-height">
            <v-form @submit.prevent="submit" class="full-form">
              <v-text-field v-model="authorName" label="Nom d'auteur"></v-text-field>
              <v-text-field v-model="email" label="Adresse e-mail"></v-text-field>
              <app-image-uploader v-model="profilePictureUrl" :place="'profile'" @start="disabledSubmit = true" @end="disabledSubmit = false"/>
              <v-btn type="submit" color="success" class="mr-4" :disabled="disabledSubmit">Mettre à jour</v-btn>
            </v-form>
          </div>
        </v-col>
      </v-row>
      <v-col cols="12">
        <div class="d-flex flex-column align-center justify-start">
          <app-badge-feed />
        </div>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import AppImageUploader from '~/components/form/AppImageUploader'
import AppBadgeFeed from '~/components/AppBadgeFeed'

export default {
  components: {
    AppImageUploader,
    AppBadgeFeed
  },
  data() {
    return {
      email: '',
      authorName: '',
      errorMessage: '',
      profilePictureUrl: null,
      disabledSubmit:false
    }
  },
  computed: {
    currentUser () {
      return this.$auth.user
    }
  },
  methods: {
    async submit() {
      try {
        await this.$axios.put('/user/' + this.currentUser.id , {
          email: this.email,
          authorName: this.authorName,
          id: this.currentUser.id,
          profilePictureUrl: this.profilePictureUrl
        })
        await this.$auth.fetchUser()
        this.$toast.success('Profil enregistré', {  timeout: 2000 })
      } catch (e) {
        console.error(e)
        this.errorMessage = 'Il y a eu un soucis pendant la mise à jour de votre profil'
      }
    }
  },
  mounted () {
    if (this.currentUser) {
      this.email = this.currentUser.email
      this.authorName = this.currentUser.authorName
    }
  }
}
</script>

<style>
.sheet-with-padding {
  padding: 10px;
}

.full-form {
  width: 100%;
}
</style>
