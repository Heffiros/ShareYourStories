<!-- components/atoms/TextInput.vue -->
<template>
  <input
    :type="type"
    :placeholder="placeholder"
    :value="modelValue"
    @input="$emit('update:modelValue', $event.target.value)"
    class="
      w-full
      text-base
      py-2
      border-0 border-b-2 border-neutral-800 dark:border-neutral-200
      bg-transparent
      text-neutral-800 dark:text-neutral-200
      focus:outline-none focus:border-primary-500 dark:focus:border-primary-700
      transition-colors
    "
  />
</template>

<script setup>
defineProps({
  modelValue: { type: String, default: '' },
  placeholder:{ type: String, default: '' },
  type:       { type: String, default: 'text' }
})
defineEmits(['update:modelValue'])
</script>
