<template>
  <div>
    <v-row class="podium">
      <v-col cols="4" class="flex-column">
        <v-card class="place-card firstPlace" color="#FFCA28" prepend-icon="mdi-home">
          <v-card-text>
            <div v-if="podiums[0]">{{ podiums[0].count }}</div>
            <div v-if="podiums[0]">{{ podiums[0].storyName }}</div>
          </v-card-text>
          <v-card-subtitle>
            <!-- Informations supplémentaires sur le premier gagnant -->
          </v-card-subtitle>
        </v-card>
      </v-col>

      <v-col cols="4" class="flex-column">
        <v-card class="place-card secondPlace" color="#546E7A" prepend-icon="mdi-home">
          <v-card-text>
            <div v-if="podiums[1]">{{ podiums[1].count }}</div>
            <div v-if="podiums[1]">{{ podiums[1].storyName }}</div>
          </v-card-text>
          <v-card-subtitle>
            <!-- Informations supplémentaires sur le deuxième gagnant -->
          </v-card-subtitle>
        </v-card>
      </v-col>

      <v-col cols="4" class="flex-column">
        <v-card class="place-card thirdPlace" color="#6D4C41" prepend-icon="mdi-home">
          <v-card-text>
            <div v-if="podiums[2]">{{ podiums[2].count }}</div>
            <div v-if="podiums[2]">{{ podiums[2].storyName }}</div>
          </v-card-text>
          <v-card-subtitle>
            <!-- Informations supplémentaires sur le troisième gagnant -->
          </v-card-subtitle>
        </v-card>
      </v-col>
    </v-row>
  </div>
</template>

<script>

export default {
  props: {
    eventId: {
      type: Number,
      default: 0
    }
  },
  data () {
    return {
      podiums: []
    }
  },
  async mounted () {
    const result = await this.$axios.get('storyVote/podium/' + this.eventId)
    if (result.data) {
      console.log(result.data)
      this.podiums = result.data
    }
  }
}

</script>

<style>
.podium {
  justify-content: space-around;
  margin: 16px;
}

.flex-column {
  display: flex; /* Ajoutez cette ligne */
  flex-direction: column; /* Ajoutez cette ligne */
}

.place-card {
  text-align: center;
  margin: 16px;
  align-self: flex-end;
}

.firstPlace {
  background-color: #FFCA28;
  height: 100%;
  min-width: 130px;
}

.secondPlace {
  background-color: #546E7A;
  height: 75%;
  min-width: 130px;
}

.thirdPlace {
  background-color: #6D4C41;
  height: 50%;
  min-width: 130px;
}
</style>
