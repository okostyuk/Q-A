<template>
    <div>
        <router-link class="nav-link" to="/login">Login Page</router-link>
        <router-link class="nav-link" to="/home">All Questions</router-link>
        <router-link class="nav-link" to="/my">My Questions</router-link>
        <router-link class="nav-link" :to="randomId">Random Question</router-link>
    </div>
</template>

<script>
    export default {
        name: 'NavigationBar',
        data: function () {
            return {
                randomId: '/question/' + Math.floor(Math.random() * Math.floor(99))
            }
        }
    }
</script>


<style>
    .nav-link {
        margin: 24px;
    }
</style>