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
          v-for="(item, i) in menuItemsCompiled"
          :key="i"
          :to="item.to"
          router
          exact
        >                   
          <v-list-item-action>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item-content>                     
        </v-list-item>
      </v-list>
      <v-list-item
        v-if="isAuth"
        @click="logout"
      >
        <v-list-item-action>
          <v-icon>mdi-logout</v-icon>
        </v-list-item-action>
        <v-list-item-content>
          <v-list-item-title>Déconnexion</v-list-item-title>
        </v-list-item-content>
      </v-list-item>
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
          hasToBeAdmin: false,
          to: '/app/dashboard'
        },
        {
          icon: 'mdi-book-open-variant',
          title: 'Ma Bibliothèque',
          hasToBeAuth: true,
          hasToBeAdmin: false,
          to: '/app/library'
        },
        {
          icon: 'mdi-account',
          title: 'Mon Profil',
          hasToBeAuth: true,
          hasToBeAdmin: false,
          to: '/app/user/me'
        },
        {
          icon: 'mdi-account',
          title: 'Admin',
          hasToBeAuth: true,
          hasToBeAdmin: true,
          to: '/app/admin'
        }
      ],
      miniVariant: false,
      title: 'SharedYourStories'
    }
  },
  computed: {
    menuItemsCompiled () {
      let computedMenu = []
      computedMenu = computedMenu.concat(this.items.filter(i => !i.hasToBeAuth))
      if (this.$auth) {
        if (this.$auth.loggedIn) {
          computedMenu = computedMenu.concat(this.items.filter(i => i.hasToBeAuth && !i.hasToBeAdmin))
        }
        if (this.$auth.user && this.$auth.user.isAdmin) {
          computedMenu = computedMenu.concat(this.items.filter(i => i.hasToBeAdmin))
        }
      }
      return computedMenu
    },
    isAuth () {
      return this.$auth.loggedIn
    }
  },
  methods: {
    async logout() {
      try {
        await this.$auth.logout();
      } catch (error) {
      }
    }
  }
}
</script>

<style>
.fill-height {
  height: 100%;
}
</style>
