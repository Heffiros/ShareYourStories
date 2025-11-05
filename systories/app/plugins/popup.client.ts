import { usePopupStore } from '~/stores/popup'

export default defineNuxtPlugin((nuxtApp) => {
  const popupStore = usePopupStore()

  const popupEmitter = {
    emit(popupName: string, props?: Record<string, any>, options?: { title?: string, closeOnOverlayClick?: boolean, confetti?: boolean, backgroundUrl?: string, size?: 'sm' | 'md' | 'lg' | 'xl' | '2xl' }) {
      popupStore.openPopup(popupName, props, options)
    },

    close() {
      popupStore.closePopup()
    }
  }

  nuxtApp.provide('popup', popupEmitter)
})

declare module '#app' {
  interface NuxtApp {
    $popup: {
      emit(popupName: string, props?: Record<string, any>, options?: { title?: string, closeOnOverlayClick?: boolean, confetti?: boolean, backgroundUrl?: string, size?: 'sm' | 'md' | 'lg' | 'xl' | '2xl' }): void
      close(): void
    }
  }
}

declare module 'vue' {
  interface ComponentCustomProperties {
    $popup: {
      emit(popupName: string, props?: Record<string, any>, options?: { title?: string, closeOnOverlayClick?: boolean, confetti?: boolean, backgroundUrl?: string, size?: 'sm' | 'md' | 'lg' | 'xl' | '2xl' }): void
      close(): void
    }
  }
}