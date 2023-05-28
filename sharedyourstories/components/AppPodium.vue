<template>
  <div class="result-container">
    <div class="result-title">Résultats du concours</div>
    <div v-if="!eventIsOver" class="result-subtitle">Le concours n'est pas encore terminé les résultats ci-dessous ne sont définitifs</div>

    <v-container class="result-grid">
      <v-row>
        <v-col cols="4">
          <v-card class="result-card first-place" color="#FFCA28">
            <div class="result-card-header">1er</div>
            <div class="result-card-content">
              <template v-if="podiums[0]">
                <div>{{ podiums[0].storyName }}</div>
                <div>{{ podiums[0].count }}</div>
              </template>
            </div>
          </v-card>
        </v-col>

        <v-col cols="4">
          <v-card class="result-card second-place" color="#546E7A">
            <div class="result-card-header">2ème</div>
            <div class="result-card-content">
              <template v-if="podiums[1]">
                <div>{{ podiums[1].storyName }}</div>
                <div>{{ podiums[1].count }}</div>
              </template>
            </div>
          </v-card>
        </v-col>

        <v-col cols="4">
          <v-card class="result-card third-place" color="#6D4C41">
            <div class="result-card-header">3ème</div>
            <div class="result-card-content">
              <template v-if="podiums[2]">
                <div>{{ podiums[2].storyName }}</div>
                <div>{{ podiums[2].count }}</div>
              </template>
            </div>
          </v-card>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script>

export default {
  props: {
    eventId: {
      type: Number,
      default: 0
    },
    eventDateEnd: {
      type: String,
      default: null
    }
  },
  data () {
    return {
      podiums: []
    }
  },
  computed: {
    eventIsOver () {
      if (this.eventDateEnd){
        const date = new Date(this.eventDateEnd);
        const currentDate = new Date();
        return date < currentDate
      }
      return true
    }
  },
  async mounted () {
    console.log(typeof this.eventDateEnd)
    const result = await this.$axios.get('storyVote/podium/' + this.eventId)
    if (result.data) {
      this.podiums = result.data
    }
  }
}

</script>

<style>
.result-container {
  padding: 16px;
}

.result-title {
  text-align: center;
  font-size: 24px;
  margin-bottom: 4px;
}

.result-subtitle {
  text-align: center;
  font-size: 12px;
  margin-bottom: 8px;
}

.result-grid {
  justify-content: center;
}

.result-card {
  text-align: center;
  padding: 16px;
  border-radius: 8px;
  margin: 16px;
}

.result-card-header {
  font-size: 20px;
  font-weight: bold;
}

.result-card-content {
  min-height: 40px;
}

.first-place {
  background-color: #FFCA28;
}

.second-place {
  background-color: #546E7A;
}

.third-place {
  background-color: #6D4C41;
}
</style>
