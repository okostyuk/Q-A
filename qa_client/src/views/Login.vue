<template>
    <div>
        <h2>{{page_title}}</h2>
        <input placeholder="Email address" v-model="email"><br>
        <input placeholder="Password" v-model="password"><br>
        <p v-if="!loading" class="error">{{error_label}}</p>
        <div><loader v-if="loading"/></div>
        <button v-on:click="login()">Sign In</button> or
        <button v-on:click="register()">Register</button>
        <br/>
        <br/>
    </div>
</template>

<script>
    import Loader from '@/components/Loader'
    export default {
        name: "Login",
        data: function() {
            return {
                page_title: 'Login page',
                error_label: '',
                loading: false,
                email: '',
                password: ''
            }
        },
        methods: {
            register() {
                this.loading = true;
                this.error_label = '';
                setTimeout(() => this.authRequest('/api/auth/signUp'), 2000);
            },
            login() {
                this.authRequest('/api/auth');
            },
            authRequest(path) {
                this.loading = true;
                this.error_label = '';
                fetch(path,
                    {
                        method: 'POST',
                        headers: {'Content-Type': 'application/json'},
                        body: JSON.stringify({
                            email: this.email,
                            password: this.password
                        })
                    })
                    .then(response => {
                        if (response.status !== 200) {
                            this.loading = false;
                            this.error_label = "Response error: " + response.status
                        } else {
                            response.json().then(
                                authResponse => {
                                    if (authResponse.status === 'OK') {
                                        this.store.userId = authResponse.user.id;
                                        this.$router.push('/home')
                                    } else {
                                        this.loading = false;
                                        this.error_label = "Response error: " + authResponse.error
                                    }
                                }
                            )
                        }
                    });
            },
        },
        components: {
            Loader
        },
        mounted() {

        }
    }
</script>

<style scoped>
    .error {color: orangered}
</style>