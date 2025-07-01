<!-- components/molecules/LoginFormMinimal.vue -->
<template>
  <form @submit.prevent="onSubmit" class="space-y-8">
    <TextInput
      v-model="email"
      placeholder="Email"
      type="email"
    />
    <TextInput
      v-model="password"
      placeholder="Mot de passe"
      type="password"
    />
      <ButtonCustom variant="neutral" @click="cancel">
        Connection
      </ButtonCustom>
  </form>
</template>

<script setup>
import { ref } from 'vue'
import TextInput from '@/components/atoms/TextInput.vue'
import ButtonCustom from '@/components/atoms/ButtonCustom.vue'
const email    = ref('')
const password = ref('')

const emit = defineEmits(['submit'])

function onSubmit() {
  emit('submit', { email: email.value, password: password.value })
}
</script>
