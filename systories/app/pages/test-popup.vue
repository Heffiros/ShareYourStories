<template>
  <div class="space-y-4">
    <h1 class="text-2xl font-bold">Test du système de popup</h1>

    <div class="space-x-4">
      <button @click="openUserPopup" class="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-lg">
        Ouvrir popup User
      </button>

      <button @click="openUserPopupWithData" class="bg-green-600 hover:bg-green-700 text-white px-4 py-2 rounded-lg">
        Ouvrir popup User avec données
      </button>

      <button @click="openUserPopupWithOptions"
        class="bg-purple-600 hover:bg-purple-700 text-white px-4 py-2 rounded-lg">
        Popup avec titre personnalisé
      </button>

      <button @click="openUserPopupNoClose" class="bg-orange-600 hover:bg-orange-700 text-white px-4 py-2 rounded-lg">
        Popup sans fermeture overlay
      </button>

      <button @click="closePopup" class="bg-red-600 hover:bg-red-700 text-white px-4 py-2 rounded-lg">
        Fermer popup
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
const { $popup } = useNuxtApp()

const openUserPopup = () => {
  $popup.emit('User')
}

const openUserPopupWithData = () => {
  $popup.emit('User', {
    user: {
      authorName: 'Alexandre Test',
      email: 'alexandre@example.com',
      isAdmin: true,
      createdAt: '2023-01-15T10:30:00Z'
    }
  })
}

const openUserPopupWithOptions = () => {
  $popup.emit('User', {
    user: {
      authorName: 'Marie Dubois',
      email: 'marie@example.com',
      isAdmin: false,
      createdAt: '2024-03-20T14:15:00Z'
    }
  }, {
    title: 'Profil Utilisateur Personnalisé',
    closeOnOverlayClick: true
  })
}

const openUserPopupNoClose = () => {
  $popup.emit('User', {
    user: {
      authorName: 'Admin User',
      email: 'admin@example.com',
      isAdmin: true,
      createdAt: '2022-01-01T00:00:00Z'
    }
  }, {
    title: 'Popup Bloquée (ESC pour fermer)',
    closeOnOverlayClick: false
  })
}

const closePopup = () => {
  $popup.close()
}
</script>