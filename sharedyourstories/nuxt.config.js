import colors from 'vuetify/es5/util/colors'

export default {
  // Disable server-side rendering: https://go.nuxtjs.dev/ssr-mode
  ssr: false,
  debug:true,
  server: {
    host: 'front.sharedyourstories.com',
    port: 3000 // ou tout autre port que vous souhaitez utiliser
  },
  // Global page headers: https://go.nuxtjs.dev/config-head
  head: {
    titleTemplate: '%s - sharedyourstories',
    title: 'sharedyourstories',
    htmlAttrs: {
      lang: 'en'
    },
    meta: [
      { charset: 'utf-8' },
      { name: 'viewport', content: 'width=device-width, initial-scale=1' },
      { hid: 'description', name: 'description', content: '' },
      { name: 'format-detection', content: 'telephone=no' }
    ],
    link: [
      { rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' }
    ]
  },
  router: {
    middleware: ['auth', 'admin-middleware']
  },
  // Global CSS: https://go.nuxtjs.dev/config-css
  css: [
  ],

  // Plugins to run before rendering page: https://go.nuxtjs.dev/config-plugins
  plugins: [
    '~/plugins/axios.js',
    '~/plugins/vuetify.js',
    '~/plugins/toastification',
    '~/plugins/vue-infinite-loading'
  ],

  // Auto import components: https://go.nuxtjs.dev/config-components
  components: true,

  // Modules for dev and build (recommended): https://go.nuxtjs.dev/config-modules
  buildModules: [
    // https://go.nuxtjs.dev/vuetify
    '@nuxtjs/vuetify',
    //'@nuxtjs/auth'
  ],

  // Modules: https://go.nuxtjs.dev/config-modules
  modules: [
    // https://go.nuxtjs.dev/axios
    '@nuxtjs/axios',
    '@nuxtjs/auth-next'
  ],


  auth: {
    redirect: {
      login: '/auth/login',
      home: '/app/dashboard'
    },
    strategies: {
      local: {
        scheme: 'local',
        token: {
          property: 'accessToken',
          global: true,
          maxAge: 60 * 60 * 24 * 30
        },
        user: {
          property: 'user',
          // autoFetch: true
        },
        endpoints: {
          login: { url: '/jwt/login', method: 'post' },
          register: { url: '/jwt/register', method: 'post' },
          //refresh: { url: '/jwt/refresh', method: 'post' },
          user: { url: '/user/me', method: 'get'},
          logout: false
        }
      }
    }
  },

  // Axios module configuration: https://go.nuxtjs.dev/config-axios
  axios: {
    baseURL: 'https://api.sharedyourstories.com:5000/',
    headers: {
      common: {
        Authorization: (context) => {
          /*const token = context.$auth.getToken('local')
          if (token) {
            return `Bearer ${token}`
          } else {
            return null;
          }*/
        }
      }
    }
  },

  // Vuetify module configuration: https://go.nuxtjs.dev/config-vuetify
  vuetify: {
    customVariables: ['~/assets/variables.scss'],
    theme: {
      dark: true,
      themes: {
        dark: {
          primary: colors.blue.darken2,
          accent: colors.grey.darken3,
          secondary: colors.amber.darken3,
          info: colors.teal.lighten1,
          warning: colors.amber.base,
          error: colors.deepOrange.accent4,
          success: colors.green.accent3
        }
      }
    }
  },

  // Build Configuration: https://go.nuxtjs.dev/config-build
  build: {
  }
}
