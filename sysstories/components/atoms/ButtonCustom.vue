<!-- components/atoms/ButtonCustom.vue -->
<template>
  <button
    :class="[
      baseClasses,
      variantClasses.bg,
      variantClasses.hover,
      variantClasses.darkBg,
      variantClasses.darkHover
    ]"
    @click="$emit('click')"
  >
    <slot/>
  </button>
</template>

<script setup>
import { computed } from 'vue'

const props = defineProps({
  variant: {
    type: String,
    default: 'primary'
  }
})
const emit = defineEmits(['click'])
const baseClasses = 'w-full px-6 py-3 text-white font-bold rounded-lg transition transform hover:-translate-y-0.5 focus:outline-none'

const variants = {
  primary: {
    bg:        'bg-primary-500',
    hover:     'hover:bg-primary-700',
    darkBg:    'dark:bg-primary-700',
    darkHover: 'dark:hover:bg-primary-500'
  },
  accent: {
    bg:        'bg-accent-500',
    hover:     'hover:bg-accent-700',
    darkBg:    'dark:bg-accent-700',
    darkHover: 'dark:hover:bg-accent-500'
  },
  neutral: {
    bg:        'bg-neutral-700',
    hover:     'hover:bg-neutral-900',
    darkBg:    'dark:bg-neutral-300',
    darkHover: 'dark:hover:bg-neutral-500'
  }
}
const variantClasses = computed(() => {
  return variants[props.variant] || variants.primary
})
</script>
