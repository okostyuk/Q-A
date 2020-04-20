<template>
    <div>
        <h2>Main page</h2>
        <div class="content" style="text-align: right">
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
                this.error.text = "";
                this.loading = false;
                this.questions = response;
            },
            onError(errorText) {
                this.loading = false;
                this.error.text = errorText;
            }
        },
        components: {
            QuestionsList,
            Loader
        },
        mounted() {
/*            this.questions = [
                {title: "q1", subtitle: "user1", info: 55},
                {title: "q2", subtitle: "user2", info: 9}
            ];*/
            this.loadQuestions();
        }
    }
</script>

<style scoped>

</style>