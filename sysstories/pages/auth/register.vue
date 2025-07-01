<!-- pages/auth/login.vue -->
<template>
  <AuthLayout>
    <div class="w-full max-w-md mx-auto px-4">
         <div class="absolute top-4 right-4">
        <DarkModeToggle />
      </div>
      <GraphicTitle
        line1="Share"
        line2="YourStorie"
        tagline="Once upon a time in..."
      />
      <RegisterFormMinimal @submit="handleSubmit" />
    </div>
  </AuthLayout>
</template>

<script setup>
import AuthLayout        from '@/components/templates/AuthLayout.vue'
import GraphicTitle      from '@/components/atoms/GraphicTitle.vue'
import RegisterFormMinimal  from '~/components/molecules/RegisterFormMinimal.vue'
import DarkModeToggle   from '@/components/atoms/DarkModeToggle.vue'

function handleSubmit(payload) {
  console.log('formulaire envoyé', payload)
}
</script>
