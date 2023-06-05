<template>
  <div>
    <v-file-input
      v-model="file"
      accept="image/*"
      label="Choose an image"
      @change="handleFileUpload"
    ></v-file-input>
    <v-img v-if="imageUrl" :src="imageUrl" contain></v-img>
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
        const formData = new FormData();
        formData.append('file', this.file);

        this.$axios
          .post('/upload/image', formData, {
            headers: {
              'Content-Type': 'multipart/form-data',
            },
          })
          .then(response => {
            console.log(response.data)
            const imageUrl = response.data;
            this.imageUrl = imageUrl;
            this.$emit('input', this.imageUrl)
          })
          .catch(error => {
            console.error(error);
          });
      }
    },
  },
};
</script>
