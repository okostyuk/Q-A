<template>
  <div id="app" v-bind:class="theme">
    <div style="text-align: right; width: 720px; margin-left: auto; margin-right: auto">    
      <a @click="switchTheme" href="#">{{theme}}</a>
    </div>
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
  data: function () {
    return {
      theme: "light"
    }
  },
  methods: {
    switchTheme() {
      this.theme = (this.theme === "dark" ? "light" : "dark")
    }
  },
  components: {
    NavigationBar
  },
  mounted() {
  }
}
</script>

<style>
  
  #app {
    --color-accent-transparent: rgba(65, 184, 131, 0.2);
    --color-accent: #41B883;
    --color-accent-secondary: #35495E;
    --color-primary: rgba(0, 0, 0, 0.87);
    --color-secondary: rgba(0, 0, 0, 0.54);
    --color-bg: rgb(250, 250, 250);
    --color-placeholder: #cfcfcf;
    --font-size-primary: 20px;
    --font-size-secondary: 16px;
    --font-size-info: 14px;
    
    font-family: Roboto, sans-serif;
    font-size: var(--font-size-primary);
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: var(--color-primary);
    padding-top: 60px;
  }
  
  .dark {
    background: slategray;
  }
  .light {
    background: #fafafa;
  }

  label {
    color: var(--color-primary);
    //font-size: 20px;
  }

  textarea,
  input {
    font-size: var(--font-size-primary);
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
    background: var(--color-accent-secondary);
    color: white;
    font-size: 16px;
  }

  button:hover{background-color: var(--color-accent);}

  button:disabled {
    background: var(--color-accent-transparent);
  }


  textarea:focus,
  input:focus {
    outline: none !important;
    border:1px solid var(--color-accent);
    box-shadow: 0 0 4px var(--color-accent-transparent);
  }

  .error {color: orangered}
  
  .content{
    width: 50%;
    margin: auto;
  }

  .list_item {
    cursor: pointer;
    display: flex;
    position: relative;
    padding-left: 16px;
    padding-right: 16px;
    height: 72px;
    align-items: center;
  }

  .list_item:hover{background-color: var(--color-accent-transparent);}
  
</style>
