<!-- pages/login.vue -->
<template>
  <div class="w-full max-w-md p-8 bg-white rounded shadow-md">
    <h1 class="text-xl font-semibold mb-4">Connexion</h1>
    <form>
      <label class="block mb-2">Email
        <input class="w-full border p-2 rounded" type="email" />
      </label>
      <label class="block mb-4">Mot de passe
        <input class="w-full border p-2 rounded" type="password" />
      </label>
      <button class="w-full py-2 rounded bg-blue-600 text-white">Se connecter</button>
    </form>
  </div>
</template>

<script setup lang="ts">
definePageMeta({ layout: 'auth' })
</script>
