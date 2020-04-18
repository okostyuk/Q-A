<template>
    <div>
        <h2>MyQuestions Page</h2>
        <hr>
        <Loader v-if="loading"/>
        <br/>
        <label v-if="my_questions.length === 0">Вы не создали ни одного опроса
            <br>
            Нажмите кнопку "ADD QUESTION" что бы создать новый вопрос
        </label>
        <QuestionsList v-else v-bind:questions="my_questions"/>
        <hr>
        <button @click="addQuestion()">ADD QUESTION</button>
    </div>
</template>

<script>
    import Loader from "../components/Loader";
    import http_service from "../http-service";
    import QuestionsList from "../components/QuestionsList";
    export default {
        name: "MyQuestions",
        data: function () {
            return {
                loading: false,
                my_questions: []
            }
        },
        methods: {
            onSuccess(result) {
                this.loading = false;
                this.error_label = '';
                this.my_questions = result.questions;
            },
            onError(error) {
                this.loading = false;
                this.error_label = error;
            },
            loadMyQuestions() {
                this.loading = true;
                http_service.GET('/api/questions/my', this);
            },
            addQuestion() {
                this.$router.push('/add')
            }
        },
        components: {
            QuestionsList,
            Loader
        },
        mounted() {
            this.loadMyQuestions();
        }
    }
</script>

<style scoped>
</style>