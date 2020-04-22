<template>
    <div>
        <h2>Main page</h2>
        <div class="container right" style="text-align: right">
            <button @click="addQuestion">СОЗДАТЬ ОПРОС</button>
            <button @click="loadQuestions">REFRESH</button>
        </div>
        <Loader v-if="loading"/>
        <label v-if="!loading && questions.length === 0">Еще никто не создал вопросов, будьте первым!</label>
        <QuestionsList v-else v-bind:questions="questions"/>
        <br/>
        <label v-if="error.text.length > 0" class="error">{{error.text}}</label>
    </div>
</template>

<script>
    import Loader from "../components/Loader"
    import http_service from "../http-service";
    import qa_rest_service from "../qa-rest-service";
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
                qa_rest_service.getQuestions()
                    .then(result => this.onSuccess(result))
                    .catch(error => this.onError(error))
            },
            onSuccess(response) {
                this.error.text = "";
                this.loading = false;
                this.questions = response;
            },
            onError(errorText) {
                this.loading = false;
                this.error.text = errorText;
            },
            onAuthError() {
                http_service.logout();
                this.$router.push({ name: 'login', params: { redirectTo: '/home' } });
            }
        },
        components: {
            QuestionsList,
            Loader
        },
        mounted() {
            if (http_service.userId().length === 0) {
                this.onAuthError();
            } else {
                this.loadQuestions();
            }
        }
    }
</script>
