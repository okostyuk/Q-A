<template>
    <div>
        <h2>{{page_title}}</h2>
        <input placeholder="Email address"><br>
        <input placeholder="Password"><br>
        <button v-on:click="login()">Sign In</button> or
        <button id="registerButton">Register</button>
        <hr>
        <router-link to="/my">Link to My questions</router-link>
    </div>
</template>

<script>
    export default {
        name: "Login",
        data: function() {
            return {
                page_title: 'Login page'
            }
        },
        methods: {
            login() {
                this.$router.push('/my')
            }
        }
    }
</script>

<style scoped>

</style>