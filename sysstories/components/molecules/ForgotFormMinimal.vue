<!-- components/molecules/ForgotFormMinimal.vue -->
<template>
  <form @submit.prevent="onSubmit" class="space-y-8">
    <TextInput
      v-model="email"
      placeholder="Email"
      type="email"
    />
     <ButtonCustom variant="accent" @click="cancel">
        Forgot
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
  emit('submit', { email: email.value})
}
</script>
