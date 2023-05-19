<template>
  <v-container>
    <v-row class="justify-center">
      <!-- Colonne 1 -->
      <v-col cols="2" class="d-flex justify-center align-center">
        <v-btn v-if="currentPageIndex != 0" fab dark small class="ma-2" @click="currentPageIndex--">
          <v-icon>mdi-arrow-left</v-icon>
        </v-btn>
      </v-col>

      <!-- Colonne 2 -->
      <v-col cols="8">
        <v-sheet class="d-flex justify-center align-center fill-height sheet-with-padding">
          {{ currentPage.content }}
        </v-sheet>
        <div class="fix-right">{{ currentPageIndex }} / {{ story.pages.length - 1}}</div>
      </v-col>

      <!-- Colonne 3 -->
      <v-col cols="2" class="d-flex justify-center align-center">
        <v-btn v-if="currentPageIndex != story.pages.length - 1" fab dark small class="ma-2" @click="currentPageIndex++">
          <v-icon>mdi-arrow-right</v-icon>
        </v-btn>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
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
  computed: {
    currentPage () {
      return this.story.pages[this.currentPageIndex]
    }
  }
}
</script>

<style>
.sheet-with-padding {
  padding: 15px;
  font-size: 18px;
}

.fix-right {
  text-align: right;
}
</style>
