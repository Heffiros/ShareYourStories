<template>
  <v-app dark>
    <v-navigation-drawer
      v-model="drawer"
      :mini-variant="miniVariant"
      :clipped="clipped"
      fixed
      app
    >
      <v-list>
        <v-list-item
          v-for="(item, i) in items"
          :key="i"
          :to="item.to"
          router
          exact
        >
          <template v-if="(item.hasToBeAuth && isAuth) || !item.hasToBeAuth">
            <v-list-item-action>
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-item-action>
            <v-list-item-content>
              <v-list-item-title>{{ item.title }}</v-list-item-title>
            </v-list-item-content>
          </template>

        </v-list-item>
      </v-list>
    </v-navigation-drawer>
    <v-app-bar
      :clipped-left="clipped"
      fixed
      app
    >
      <v-app-bar-nav-icon @click.stop="drawer = !drawer" />
      <v-toolbar-title>{{ title }}</v-toolbar-title>
      <v-spacer />
    </v-app-bar>
    <v-main>
      <v-container>
        <Nuxt />
      </v-container>
    </v-main>
    <v-footer
      :absolute="!fixed"
      app
    >
      <span>&copy; {{ new Date().getFullYear() }}</span>
    </v-footer>
  </v-app>
</template>

<script>
export default {
  name: 'DefaultLayout',
  data () {
    return {
      clipped: false,
      drawer: true,
      fixed: false,
      items: [
        {
          icon: 'mdi-apps',
          title: 'Dashboard',
          hasToBeAuth: false,
          to: '/app/dashboard'
        },
        {
          icon: 'mdi-book-open-variant',
          title: 'Ma Bibliothèque',
          hasToBeAuth: true,
          to: '/app/library'
        },
        {
          icon: 'mdi-account',
          title: 'Mon Profil',
          hasToBeAuth: true,
          to: '/app/user/me'
        }
      ],
      miniVariant: false,
      title: 'SharedYourStories'
    }
  },
  computed: {
    isAuth () {
      return this.$auth.loggedIn
    }
  }
}
</script>

<style>
.fill-height {
  height: 100%;
}
</style>
