<template>
  <Teleport to="body">
    <Transition name="popup-fade">
      <div v-if="popupStore.isOpen" class="fixed inset-0 z-50 flex items-center justify-center p-4"
        @click="closeOnOverlay">
        <div class="absolute inset-0 bg-black/50 backdrop-blur-sm"></div>

        <div ref="popupContent"
          class="relative bg-white dark:bg-gray-800 rounded-lg shadow-xl max-w-lg w-full max-h-[80vh] overflow-hidden"
          @click.stop>
          <div class="flex items-center justify-between p-4 border-b border-gray-200 dark:border-gray-700">
            <slot name="header">
              <h3 class="text-lg font-medium text-gray-900 dark:text-gray-100">
                {{ effectiveTitle }}
              </h3>
            </slot>

            <button @click="closePopup"
              class="p-1 text-gray-400 hover:text-gray-600 dark:hover:text-gray-200 transition-colors">
              <X class="w-5 h-5" />
            </button>
          </div>

          <div class="p-4 overflow-y-auto">
            <slot name="content">
              <component v-if="currentComponent" :is="currentComponent" v-bind="popupStore.props" />
            </slot>
          </div>

          <div v-if="$slots.footer" class="p-4 border-t border-gray-200 dark:border-gray-700">
            <slot name="footer"></slot>
          </div>
        </div>
      </div>
    </Transition>
  </Teleport>
</template>

<script setup lang="ts">
import { X } from 'lucide-vue-next'
import { usePopupStore } from '~/stores/popup'

interface Props {
  title?: string
  closeOnOverlayClick?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  closeOnOverlayClick: true
})

const popupStore = usePopupStore()
const popupContent = ref<HTMLElement>()

// Utiliser les options du store en priorité, puis les props par défaut
const effectiveTitle = computed(() => popupStore.popupOptions?.title || props.title || 'Popup')
const effectiveCloseOnOverlay = computed(() =>
  popupStore.popupOptions?.closeOnOverlayClick !== undefined
    ? popupStore.popupOptions.closeOnOverlayClick
    : props.closeOnOverlayClick
)

const currentComponent = computed(() => {
  if (!popupStore.component) return null

  return defineAsyncComponent({
    loader: () => import(`~/components/popup/${popupStore.component}.vue`),
    errorComponent: () => h('div', { class: 'text-red-500 p-4' }, 'Erreur lors du chargement du composant')
  })
})

const closePopup = () => {
  popupStore.closePopup()
}

const closeOnOverlay = () => {
  if (effectiveCloseOnOverlay.value) {
    closePopup()
  }
}

onMounted(() => {
  document.addEventListener('keydown', onEscapeKey)
})

onUnmounted(() => {
  document.removeEventListener('keydown', onEscapeKey)
})

const onEscapeKey = (event: KeyboardEvent) => {
  if (event.key === 'Escape' && popupStore.isOpen) {
    closePopup()
  }
}
</script>

<style scoped>
.popup-fade-enter-active,
.popup-fade-leave-active {
  transition: opacity 0.2s ease;
}

.popup-fade-enter-from,
.popup-fade-leave-to {
  opacity: 0;
}

.popup-fade-enter-active .relative,
.popup-fade-leave-active .relative {
  transition: transform 0.2s ease;
}

.popup-fade-enter-from .relative,
.popup-fade-leave-to .relative {
  transform: scale(0.95);
}
</style>