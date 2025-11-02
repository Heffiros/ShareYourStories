export default defineNuxtConfig({
  ssr: false,
  compatibilityDate: "2025-07-15",
  devtools: { enabled: true },
  css: ["~/assets/css/main.css"],
  devServer: {
    https: false,
    port: 5000,
  },
  runtimeConfig: {
    public: {
      apiBase: process.env.NUXT_PUBLIC_API_BASE || 'http://localhost:3000',
      autoLoginEmail: process.env.NUXT_PUBLIC_AUTOLOGIN_EMAIL,
      autoLoginPassword: process.env.NUXT_PUBLIC_AUTOLOGIN_PASSWORD,
      environment: process.env.ENVIRONMENT || 'development'
    },
  },
  modules: [
    "nuxt-lucide-icons",
    '@pinia/nuxt',
    '@nuxtjs/tailwindcss'
  ]
});