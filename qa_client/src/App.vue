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
    --accent-color-transparent: rgba(65, 184, 131, 0.2);
    --accent-color: #41B883;
    --secondary-accent-color: #35495E;
    --primary-color: rgba(0, 0, 0, 0.87);
    --secondary-color: rgba(0, 0, 0, 0.54);
    --placeholder-color: #cfcfcf
  }
  
  #app {
    font-family: Roboto, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: var(--primary-color);
    margin-top: 60px;
    font-size: 20px;
  }

  label {
    color: var(--primary-color);
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
    border: none;
    border-radius: 2px;
    font-weight: 500;
    margin: 4px;
    padding-left: 16px;
    padding-right: 16px;
    height: 48px;
    background: var(--secondary-accent-color);
    color: white;
    font-size: 16px;
  }

  button:hover{background-color: var(--accent-color);}

  textarea:focus,
  input:focus {
    outline: none !important;
    border:1px solid var(--accent-color);
    box-shadow: 0 0 4px var(--accent-color-transparent);
  }

  .error {color: orangered}
</style>
