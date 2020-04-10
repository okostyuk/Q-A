<template>
  <div id="app">
    <img alt="Vue logo" src="./assets/logo.png">
    <h1>Q&A Application</h1>
    <router-view style="border: 1px solid #42b983"/>
    <!--<HelloWorld msg="Welcome to Your Vue.js App"/>-->
  </div>
</template>

<script>
//import HelloWorld from './components/HelloWorld.vue'
import Router from './router'
export default {
  name: 'App',
  router: Router,
  components: {
    //HelloWorld
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
