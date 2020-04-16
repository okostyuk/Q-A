<template>
    <div>
        <h2>{{page_title}}</h2>
        <input placeholder="Email address" v-model="auth_request.email"><br>
        <input placeholder="Password" v-model="auth_request.password"><br>
        <p v-if="!loading" class="error">{{error_label}}</p>
        <div><loader v-if="loading"/></div>
        <button v-on:click="login()">Sign In</button> or
        <button v-on:click="register()">Register</button>
        <br/>
        <br/>
    </div>
</template>

<script>
    import http_service from '../http-service.js'
    import Loader from '../components/Loader'
    export default {
        name: "Login",
        data: function() {
            return {
                page_title: 'Login page',
                error_label: '',
                loading: false,
                auth_request: {
                    email: 'test1@oleg.com',
                    password: ''
                }
            }
        },
        methods: {
            login() {
                this.loading = true;
                this.error_label = '';
                http_service.POST('/api/auth', this, this.auth_request);
            },
            register() {
                this.loading = true;
                this.error_label = '';
                http_service.POST('/api/auth/signUp', this, this.auth_request);
            },
            onSuccess(jsonResult) {
                console.log("onSuccess: " + jsonResult);
                this.loading = false;
                this.error_label = '';
                this.$router.push('/home')
            },
            onError(errorText) {
                console.log("onError: " + errorText);
                this.loading = false;
                this.error_label = errorText;
            },
        },
        components: {
            Loader
        },
        mounted() {
        }
    }
</script>