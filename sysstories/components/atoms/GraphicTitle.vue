<template>
  <div class="text-center mb-12">
    <div class="inline-block transform -rotate-2">
      <h1
        class="font-bold text-[5rem] md:text-[6rem] leading-tight
               text-black dark:text-white"
      >
        {{ line1 }}
      </h1>
    </div>
    <div class="inline-block transform rotate-1">
      <h1
        class="font-bold text-[4rem] md:text-[6rem] sm:text-[4rerm] leading-tight
               text-black dark:text-white"
      >
        {{ line2 }}
      </h1>
    </div>
    <p
      v-if="tagline"
      class="mt-4 text-sm text-black/70 dark:text-white/70
             relative inline-block"
    >
      {{ tagline }}
      <span
        class="
          absolute left-0 bottom-0 block w-full h-[2px] bg-current
          transform -rotate-2 origin-left
          transition-all duration-200 ease-in-out
          group-hover:rotate-0 group-hover:translate-y-1
        "
      ></span>
    </p>
  </div>
</template>

<script setup>
defineProps({
  line1:   { type: String, required: true },
  line2:   { type: String, default: '' },
  tagline: { type: String, default: '' }
})
</script>
