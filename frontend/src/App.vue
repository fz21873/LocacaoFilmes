<template>
  <v-app>
      <component v-bind:is="layout"></component>
  </v-app>
</template>

<script>

import BaseLayout from './layout/Base';

export default{
  computed: {
    layout(){
      return this.$store.state.layout;
    }
  },
  components: {

    'baseLayout': BaseLayout,

  }
};
</script>
<!--<template>


  <div id="app">
      <HelloWorld ></HelloWorld>


</div>
</template>

<script>
import HelloWorld from './components/HelloWorld';
export default {
    components: {
         HelloWorld,

        },
  name: 'App'
};
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 10px;
}


</style>-->
