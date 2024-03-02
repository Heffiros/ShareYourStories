<template>
  <div>
    <v-file-input
      v-model="file"
      accept="image/*"
      label="Choose an image"
      @change="handleFileUpload"
    ></v-file-input>
    <v-img
      v-if="imageUrl"
      :src="imageUrl"
      :width="300"
      :height="300"
      class="imageUploaded"
      aspect-ratio="16/9"
      cover
    ></v-img>
    <v-img contain></v-img>
  </div>
</template>

<script>
export default {
  data() {
    return {
      imageUrl: null,
      file: null,
    };
  },
  methods: {
    handleFileUpload() {
      if (this.file) {
        this.$emit('start')
        const formData = new FormData();
        formData.append('file', this.file);

        this.$axios
          .post('/upload/image', formData, {
            headers: {
              'Content-Type': 'multipart/form-data',
            },
          })
          .then(response => {
            const imageUrl = response.data;
            this.imageUrl = imageUrl;
            this.$emit('input', this.imageUrl)
          })
          .catch(error => {
            console.error(error);
          });
          this.$emit('end')
      }
    },
  },
};
</script>

<style>

.imageUploaded {
  margin-bottom: 24px;
}

</style>
