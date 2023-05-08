<template>
  <v-container>
    <v-file-input
      v-model="file"
      accept=".docx"
      label="Sélectionner un fichier"
      outlined
    ></v-file-input>

    <v-btn color="primary" @click="importDocx" :disabled="!file">
      Importer
    </v-btn>

    <v-divider></v-divider>

    <v-card v-if="text">
      <v-card-title>Contenu du fichier</v-card-title>
      <v-card-text>
        <div v-for="(page, index) in text" :key="index">
          <p>{{ page }}</p>
        </div>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script>
import { readDocxFile } from "docx";
export default {
  data () {
    return {
      file: null,
      text: null,
      content: null
    };
  },
  methods: {
    async importDocx() {
      try {
        const formData = new FormData();
        formData.append("file", this.file);

        const response = await this.$axios.post("upload/file", formData, {
          headers: {
            "Content-Type": "multipart/form-data"
          }
        });

        console.log(response.data);
      } catch (error) {
        console.error(error);
      }
    }
  },
}
</script>
