import InfiniteLoading from 'vue-infinite-loading'
import Vue from 'vue'

export default () => {
  Vue.use(InfiniteLoading, {
    slots: {
      noMore: ''
    },
  })
}
