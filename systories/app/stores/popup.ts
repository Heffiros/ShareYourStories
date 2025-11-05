import { defineStore } from 'pinia'

interface PopupState {
  isOpen: boolean
  component: string | null
  props?: Record<string, any>
  popupOptions?: {
    title?: string
    closeOnOverlayClick?: boolean
  }
}

export const usePopupStore = defineStore('popup', {
  state: (): PopupState => ({
    isOpen: false,
    component: null,
    props: {},
    popupOptions: {}
  }),

  actions: {
    openPopup(component: string, props?: Record<string, any>, options?: { title?: string, closeOnOverlayClick?: boolean }) {
      this.component = component
      this.props = props || {}
      this.popupOptions = options || {}
      this.isOpen = true
    },

    closePopup() {
      this.isOpen = false
      this.component = null
      this.props = {}
      this.popupOptions = {}
    }
  }
})