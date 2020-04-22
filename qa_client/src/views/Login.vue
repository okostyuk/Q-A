<template>
    <div>
        <h2>{{page_title}}</h2>
        <div v-if="signedIn">
            <p>You are logged in as</p>
            <p><a>{{email}}</a></p>
            <button @click="logout">LOGOUT</button>
        </div>
        <div v-else>
            <input v-bind:disabled="loading" placeholder="Email address" v-model="auth_request.email"><br>
            <input v-bind:disabled="loading" placeholder="Password" v-model="auth_request.password"><br>
            <p v-if="!loading" class="error">{{error_label}}</p>
            <div><loader v-if="loading"/></div>
            <button v-bind:disabled="loading" v-on:click="login()">SIGN IN</button> or
            <button v-bind:disabled="loading" v-on:click="register()">REGISTER</button>
        </div>
        <br/>
        <br/>
    </div>
</template>

<script>
    import qa_rest_service from "../qa-rest-service";
    import http_service from '../http-service.js'
    import Loader from '../components/Loader'
    export default {
        name: "Login",
        data: function() {
            return {
                email: http_service.email(),
                userId: "",
                page_title: 'Login page',
                error_label: '',
                loading: false,
                auth_request: {
                    email: 'test1@oleg.com',
                    password: ''
                }
            }
        },
        computed: {
            signedIn: function () {
                return this.userId.length > 0;
            }
        },
        methods: {
            logout() {
                this.userId = "";
                this.email = "";
                http_service.logout();
            },
            login() {
                this.loading = true;
                this.error_label = '';
                qa_rest_service.login(this.auth_request)
                    .then(result => this.onSuccess(result))
                    .catch(error => this.onError(error));
            },
            register() {
                this.loading = true;
                this.error_label = '';
                qa_rest_service.register(this.auth_request)
                    .then(result => this.onSuccess(result))
                    .catch(error => this.onError(error));
            },
            onSuccess(jsonResult) {
                console.log("onSuccess: " + jsonResult);
                this.loading = false;
                this.error_label = '';
                let target = '/home';
                let redirectTo = this.$route.params.redirectTo;
                if (redirectTo) {
                    target = redirectTo;
                }
                this.$router.push(target);
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
            this.userId = http_service.userId();
            this.email = http_service.email();
            if (this.userId.length === 0) {
                http_service.logout();
            }
        }
    }
</script>