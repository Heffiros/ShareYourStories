<template>
  <div class="mx-auto px-6 py-12 relative w-full h-96">
    <div class="relative">
      <div class="overflow-hidden">
        <div class="flex transition-transform duration-500 ease-in-out"
          :style="{ transform: `translateX(-${currentIndex * slideWidth}%)` }">
          <div v-for="item in items" :key="item.id" class="flex-shrink-0 w-full">
            <div
              class="relative h-96 rounded-lg overflow-hidden shadow-md hover:shadow-lg transition-shadow group cursor-pointer"
              :style="{ backgroundImage: `url(${item.coverUrl})`, backgroundSize: '120%', backgroundPosition: 'center' }">
              <div class="absolute inset-0 bg-gradient-to-t from-black/80 via-black/30 to-transparent"></div>
              <div class="absolute inset-0 p-8 text-white flex flex-col justify-between">
                <div>
                  <h3 class="text-4xl font-bold">
                    {{ item.title }}
                  </h3>
                </div>
                <div class="flex-1 flex items-center">
                  <p class="text-lg text-white/90 max-w-4xl leading-relaxed">
                    {{ item.rules }}
                  </p>
                </div>
                <div v-if="item.dateEnd" class="text-base text-white/80">
                  Fin le {{ item.dateEnd }}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="flex justify-center gap-2 mt-6">
        <button v-for="(_, index) in totalSlides" :key="index" @click="goTo(index)"
          class="w-2 h-2 rounded-full transition-colors"
          :class="currentIndex === index ? 'bg-slate-900 dark:bg-slate-100' : 'bg-slate-300 dark:bg-slate-600'">
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import type { Event } from '~/types/event'

const props = defineProps<{
  items: Event[]
}>()

const slidesPerView = 1
const currentIndex = ref(0)
const slideWidth = computed(() => 100 / slidesPerView)
const totalSlides = computed(() => props.items.length)
const maxIndex = computed(() => Math.max(0, totalSlides.value - 1))
let autoPlayTimer: ReturnType<typeof setInterval> | null = null

const autoNext = () => {
  if (totalSlides.value <= 1) return

  if (currentIndex.value < maxIndex.value) {
    currentIndex.value++
  } else {
    currentIndex.value = 0
  }
}

const startAutoPlay = () => {
  if (totalSlides.value <= 1) return
  autoPlayTimer = setInterval(autoNext, 10000)
}

const stopAutoPlay = () => {
  if (autoPlayTimer) {
    clearInterval(autoPlayTimer)
    autoPlayTimer = null
  }
}

const goTo = (index: number) => {
  currentIndex.value = index
  stopAutoPlay()
  startAutoPlay()
}

onMounted(() => {
  startAutoPlay()
})

onUnmounted(() => {
  stopAutoPlay()
})
</script>

<style scoped>
.line-clamp-2 {
  display: -webkit-box;
  -webkit-line-clamp: 2;
  line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
}
</style>