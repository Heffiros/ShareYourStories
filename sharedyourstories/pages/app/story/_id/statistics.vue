<template>
  <v-container fluid>
    <v-row justify="space-around">
      <v-col cols="4">
        <v-card class="text-center" color="primary" dark>
          <v-card-text class="text-h6">Carré 1</v-card-text>
          <v-card-text class="display-1">42</v-card-text>
        </v-card>
      </v-col>

      <v-col cols="4">
        <v-card class="text-center" color="secondary" dark>
          <v-card-text class="text-h6">Carré 2</v-card-text>
          <v-card-text class="display-1">99</v-card-text>
        </v-card>
      </v-col>

      <v-col cols="4">
        <v-card class="text-center" color="accent" dark>
          <v-card-text class="text-h6">Carré 3</v-card-text>
          <v-card-text class="display-1">73</v-card-text>
        </v-card>
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12">
        <v-sheet class="pa-4 graphContainer">
          <h2>Contenu du bloc</h2>
          <p>Texte, images, formulaires, etc.</p>
        </v-sheet>
      </v-col>
    </v-row>
  </v-container>
</template>

<style>
.graphContainer {
  height: 700px;
}
</style>
