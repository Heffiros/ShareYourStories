<template>
  <v-container>
    <v-card class="mx-auto mt-10" max-width="400">
      <v-card-title class="text-center">Connexion</v-card-title>
      <v-card-text>
        <v-form @submit.prevent="submit">
          <v-text-field v-model="email" label="Adresse e-mail"></v-text-field>
          <v-text-field v-model="password" label="Mot de passe" type="password"></v-text-field>
          <v-btn type="submit" color="primary" class="mr-4">Se connecter</v-btn>
          <router-link :to="{ name: 'register' }">S'inscrire</router-link>
        </v-form>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script>
export default {
  auth: false,
  data() {
    return {
      email: '',
      password: '',
      errorMessage: ''
    }
  },
  methods: {
    async submit() {
      try {
        await this.$auth.loginWith('local', {
          data: {
            email: 'vasax.levy@gmail.com',//this.email,
            password: 'alex1994'//this.password
          }
        })
        this.$router.push('/app/dashboard')
      } catch (e) {
        console.error(e)
        this.errorMessage = 'Invalid email or password.'
      }
    }
  }
}
</script>
