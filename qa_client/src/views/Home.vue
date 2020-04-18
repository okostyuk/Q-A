<template>
    <div>
        <h2>Main page</h2>
        <label v-if="!loading && questions.length === 0">Еще никто не создал вопросов, будьте первым!</label>
        <QuestionsList v-else v-bind:questions="questions"/>
        <br/>
        <label v-if="error.text.length > 0" class="error">{{error.text}}</label>
        <br/>
        <Loader v-if="loading"/>
        <button @click="addQuestion">СОЗДАТЬ ОПРОС</button>
        <button @click="loadQuestions">REFRESH</button>
    </div>
</template>

<script>
    import Loader from "../components/Loader"
    import http_service from "../http-service";
    import QuestionsList from "../components/QuestionsList";
    export default {
        name: "Home",
        data: function () {
            return {
                loading: false,
                error: {
                    text: ""
                },
                questions: [
                ]
            }
        },
        methods: {
            addQuestion() {
                this.$router.push('/add')
            },
            loadQuestions() {
                this.loading = true;
                http_service.GET('/api/questions', this)
            },
            onSuccess(response) {
                console.log("home onSuccess()");
                this.error_label = "";
                this.loading = false;
                this.questions = response.questions;
            },
            onError(errorText) {
                console.log("home onError()");
                this.loading = false;
                this.error.text = errorText;
            }
        },
        components: {
            QuestionsList,
            Loader
        },
        mounted() {
            this.questions = [
                {title: "q1", subtitle: "user1", info: 55},
                {title: "q2", subtitle: "user2", info: 9}
            ];
            //this.loadQuestions();
        }
    }
</script>

<style scoped>

</style>