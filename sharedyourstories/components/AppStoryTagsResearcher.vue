<template>
  <div>
    <!-- Champ de recherche textuel -->
    <v-text-field
      v-model="searchText"
      label="Recherche"
      outlined
      dense
      @input="search"
    ></v-text-field>
    <div>
    <div v-if="storyTagsSelected">
      <div v-for="item in storyTagsSelected" :key="item.id" class="item" @click="remove(item.id)">
        <div class="item-content">
          {{ item.label }}
        </div>
      </div>
    </div>
  </div>
    <!-- Bloc de résultats de recherche -->
    <v-card v-if="searchResults.length > 0" class="search-results" color="#33691E">
      <v-card-title>Résultats de recherche</v-card-title>
      <v-list color="#37474F">
        <v-list-item v-for="result in searchResults" :key="result.id" class="tag" @click="choose(result)">
          <!-- Afficher les détails du résultat -->
          <v-list-item-content>
            <v-list-item-title>{{ result.label }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-card>

    <!-- Message pour indiquer l'absence de résultats -->
    <v-alert
      v-if="searchResults.length === 0 && searchPerformed"
      dense
      outlined
      type="info"
    >
      Aucun résultat trouvé.
    </v-alert>
  </div>
</template>

<script>
export default {
  data() {
    return {
      searchText: '',
      searchResults: [],
      searchPerformed: false,
      storyTagsSelected: []
    };
  },
  watch: {
    storyTagsSelected: {
      deep: false,
      handler(oldlist, newlist) {
        this.$emit('input', newlist)
      }
    }
  },
  methods: {
    async search() {
      // Effectuer la recherche ici en utilisant l'API en full text
      if (this.searchText && this.searchText.length >= 3) {
        const result = await this.$axios.get('storyTags', { params: { search: this.searchText } })
        if (result.data && result.data.length > 0) {
          this.searchResults = result.data
          this.searchPerformed = true
        }
      }
    },
    choose (data) {
      this.storyTagsSelected.push(data)
    },
    remove (id) {
      if (this.storyTagsSelected.length === 1) {
        this.storyTagsSelected = []
      } else {
        const index = this.storyTagsSelected.findIndex(st => st.id === id);
        if (index) {
          this.storyTagsSelected.splice(index, 1);
        }
      }
    }
  }
};
</script>

<style>
.search-results {
  margin: 16px;
}

.tag {
  cursor: pointer
}

.tag:hover {
  background-color: #263238;
}

.item {
  display: inline-block;
  margin: 8px;
  background-color: #4CAF50;
  border-radius: 10px;
  padding: 10px;
  margin-bottom: 10px;
}

.item:hover {
  background-color: #C62828;
  cursor: pointer;
}

.item-content {
  color: white;
}
</style>
