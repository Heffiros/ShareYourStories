<template>
  <v-container>
    <v-card class="mx-auto mt-10" max-width="400">
      <v-card-title class="text-center">Inscription</v-card-title>
      <v-card-text>
        <v-form @submit.prevent="submit">
          <v-text-field v-model="authorName" label="Nom d'auteur"></v-text-field>
          <v-text-field v-model="email" label="Adresse e-mail"></v-text-field>
          <v-text-field v-model="password" label="Mot de passe" type="password"></v-text-field>
          <v-btn type="submit" color="primary" class="mr-4">S'inscrire</v-btn>
          <router-link :to="{ name: 'login' }">Se connecter</router-link>
        </v-form>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script>
export default {
  name: 'register',
  auth: false,
  data() {
    return {
      authorName: '',
      email: '',
      password: ''
    }
  },
  methods: {
    async submit() {
      try {
        const response = await this.$axios.post('/jwt/register', {
          email: this.email,
          password: this.password,
          authorName: this.authorName
          // Ajoutez d'autres propriétés selon votre modèle d'utilisateur
        })

        const responseLogin = await this.$auth.loginWith('local', {
          data: {
            email: this.email,
            password: this.password
          }
        });
        // Rediriger l'utilisateur vers une autre page
        this.$router.push('/app/dashboard')
      } catch (error) {
        // Gestion des erreurs d'inscription
        console.error(error.response.data)
      }
    }
  }
}
</script>
