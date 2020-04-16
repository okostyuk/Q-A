<template>
  <div id="app">
    <img alt="Vue logo" src="./assets/logo.png" style="transform: scaleY(-1)">
    <h1>Q&A Application</h1>
    <navigationBar/>
    <router-view />
  </div>
</template>

<script>
  import NavigationBar from './components/NavigationBar'

export default {
  name: 'App',
  navigationBar: NavigationBar,
  components: {
    NavigationBar
  },
  mounted() {
  }
}
</script>

<style>
  :root {
    --primary-color-transparent: rgba(65, 184, 131, 0.2);
    --primary-color: #41B883;
    --secondary-color: #35495E;
    --placeholder-color: #cfcfcf
  }
  
  #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: var(--secondary-color);
    margin-top: 60px;
    font-size: 20px;
  }

  label {
    color: var(--secondary-color);
    font-size: 20px;
  }

  textarea,
  input {
    font-size: 20px;
    min-width: 200px;
    padding: 8px;
    border: 1px solid #35495E;
    margin-bottom: 24px;
  }

  button {
    margin: 4px;
    padding: 12px; 
    background: var(--primary-color); 
    color: white; 
    font-size: 20px;
  }
  
  textarea:focus,
  input:focus {
    outline: none !important;
    border:1px solid var(--primary-color);
    box-shadow: 0 0 4px var(--primary-color);
  }

  .error {color: orangered}
</style>
