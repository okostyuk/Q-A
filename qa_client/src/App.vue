<template>
  <div id="app">
    <img alt="Vue logo" src="./assets/logo.png">
    <h1>Q&A Application</h1>
    <router-view />
    <!--<HelloWorld msg="Welcome to Your Vue.js App"/>-->
  </div>
</template>

<script>
//import Login from './views/Login.vue'
//import Router from './router'
export default {
  name: 'App',
  //router: Router,
  components: {
    //Login
  },
  mounted() {
    //this.router.push('/login')
  }
}
</script>

<style>
  router-view {
    border: 1px solid #42b983
  }
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
