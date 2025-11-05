<template>
  <Teleport to="body">
    <Transition name="popup-fade">
      <div v-if="popupStore.isOpen" class="fixed inset-0 z-50 flex items-center justify-center p-4"
        @click="closeOnOverlay">
        <div class="absolute inset-0 bg-black/50 backdrop-blur-sm"></div>

        <div ref="popupContent"
          class="relative bg-white dark:bg-gray-800 rounded-lg shadow-xl w-full max-h-[80vh] overflow-hidden"
          :class="sizeClass" @click.stop>
          <div class="flex items-center justify-center p-4 border-b border-gray-200 dark:border-gray-700 relative">
            <slot name="header">
              <h3 class="text-lg font-medium text-gray-900 dark:text-gray-100 text-center">
                {{ effectiveTitle }}
              </h3>
            </slot>

            <button @click="closePopup"
              class="absolute right-4 p-1 text-gray-400 hover:text-gray-600 dark:hover:text-gray-200 transition-colors">
              <X class="w-5 h-5" />
            </button>
          </div>

          <div class="p-4 overflow-y-auto relative"
            :class="{ 'bg-cover bg-center bg-no-repeat': effectiveBackgroundUrl }"
            :style="effectiveBackgroundUrl ? { backgroundImage: `url('${effectiveBackgroundUrl}')` } : {}">
            <div v-if="effectiveBackgroundUrl" class="absolute inset-0 bg-black/30 backdrop-blur-[1px]"></div>
            <div class="relative z-10" :class="{ 'text-white': effectiveBackgroundUrl }">
              <slot name="content">
                <component v-if="currentComponent" :is="currentComponent" v-bind="popupStore.props" />
              </slot>
            </div>
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
  confetti?: boolean
  backgroundUrl?: string
  size?: 'sm' | 'md' | 'lg' | 'xl' | '2xl'
}

const props = withDefaults(defineProps<Props>(), {
  closeOnOverlayClick: true,
  confetti: false,
  backgroundUrl: '',
  size: 'lg'
})

const popupStore = usePopupStore()
const popupContent = ref<HTMLElement>()

const effectiveTitle = computed(() => popupStore.popupOptions?.title || props.title || 'Popup')
const effectiveCloseOnOverlay = computed(() =>
  popupStore.popupOptions?.closeOnOverlayClick !== undefined
    ? popupStore.popupOptions.closeOnOverlayClick
    : props.closeOnOverlayClick
)
const effectiveConfetti = computed(() =>
  popupStore.popupOptions?.confetti !== undefined
    ? popupStore.popupOptions.confetti
    : props.confetti
)
const effectiveBackgroundUrl = computed(() =>
  popupStore.popupOptions?.backgroundUrl || props.backgroundUrl || ''
)
const effectiveSize = computed(() =>
  popupStore.popupOptions?.size || props.size || 'lg'
)

const sizeClass = computed(() => {
  const sizeMap: Record<string, string> = {
    'sm': 'max-w-sm',
    'md': 'max-w-md',
    'lg': 'max-w-lg',
    'xl': 'max-w-xl',
    '2xl': 'max-w-2xl'
  }
  return sizeMap[effectiveSize.value] || 'max-w-lg'
})

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

const createConfetti = () => {
  const colors = ['#ff6b6b', '#4ecdc4', '#45b7d1', '#f7b731', '#5f27cd', '#00d2d3', '#ff9ff3', '#54a0ff']
  const confettiCount = 50

  for (let i = 0; i < confettiCount; i++) {
    setTimeout(() => {
      const confetti = document.createElement('div')
      confetti.className = 'confetti-piece'
      confetti.style.cssText = `
        position: fixed;
        width: 10px;
        height: 10px;
        background: ${colors[Math.floor(Math.random() * colors.length)]};
        left: ${Math.random() * 100}vw;
        top: -10px;
        z-index: 9999;
        border-radius: ${Math.random() > 0.5 ? '50%' : '0'};
        animation: confetti-fall ${2 + Math.random() * 3}s linear forwards;
        transform: rotate(${Math.random() * 360}deg);
      `

      document.body.appendChild(confetti)

      setTimeout(() => {
        if (confetti.parentNode) {
          confetti.parentNode.removeChild(confetti)
        }
      }, 5000)
    }, i * 20)
  }
}

watch(() => popupStore.isOpen, (newValue, oldValue) => {
  if (newValue && !oldValue && effectiveConfetti.value) {
    nextTick(() => {
      createConfetti()
    })
  }
})

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

<style>
@keyframes confetti-fall {
  0% {
    transform: translateY(-10px) rotate(0deg);
    opacity: 1;
  }

  100% {
    transform: translateY(100vh) rotate(720deg);
    opacity: 0;
  }
}
</style>