<template>
  <v-container class="reader">
    <v-row class="justify-center">
      <!-- Colonne 1 -->
      <v-col cols="2" class="d-flex justify-center align-center">
        <v-btn v-if="currentPageIndex != 0" fab dark small class="ma-2" @click="currentPageIndex--">
          <v-icon>mdi-arrow-left</v-icon>
        </v-btn>
      </v-col>

      <!-- Colonne 2 -->
      <v-col cols="6">
        <v-sheet class="d-flex justify-center align-center fill-height sheet-with-padding">
          <div v-html="currentPage.content"></div>
        </v-sheet>
        <div class="fix-right">{{ currentPageIndex + 1 }} / {{ story.pages.length }}</div>
      </v-col>

      <!-- Colonne 3 -->
      <v-col cols="2" class="d-flex justify-center align-center">
        <v-btn v-if="currentPageIndex != story.pages.length - 1" fab dark small class="ma-2" @click="currentPageIndex++">
          <v-icon>mdi-arrow-right</v-icon>
        </v-btn>
      </v-col>
    </v-row>
    <app-moderation-popover v-if="currentUser && currentUser.isAdmin" :story="story" class="moderation-popover"/>
  </v-container>
</template>

<script>
import AppModerationPopover from '~/components/popover/AppModerationPopover'

export default {
  components: {
    AppModerationPopover
  },
  data () {
    return {
      currentPageIndex: 0
    }
  },
  props: {
    story: {
      type: Object,
      default: null
    }
  },
  watch: {
    currentPageIndex (n, o) {
      let storyHistory = {...this.story.storyHistory}
      storyHistory.lastPageReadId = this.currentPage.id
      this.$axios.put('storyHistories' , storyHistory)
    }
  },
  computed: {
    currentPage () {
      return this.story.pages[this.currentPageIndex]
    },
    currentUser () {
      return this.$auth.user
    }
  },
  mounted () {
    if (this.story.storyHistory) {
      const pageIndex = this.story.pages.findIndex((page) => page.id === this.story.storyHistory.lastPageReadId)
      if (pageIndex) {
        this.currentPageIndex = pageIndex
      }
    } else {
      const openingStoryHistory = {
        userId:this.$auth.user.id,
        storyId: this.story.id,
        lastPageReadId: this.story.pages[0].id
      }
      this.$axios.post('storyHistories' , openingStoryHistory)
    }
  }
}
</script>

<style>
.sheet-with-padding {
  padding: 15px;
  font-size: 18px;
  font-size: 24px;
}

.fix-right {
  text-align: right;
}

.moderation-popover {
  position: absolute;
  top: 10px;
}

.reader br {
  margin-bottom: 16px
}
</style>
