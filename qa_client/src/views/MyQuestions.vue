<template>
    <div style="margin-bottom: 48px">
        <h2>MyQuestions Page</h2>
        <Loader v-if="loading"/>
        <div class="container right"> <button @click="addQuestion()">CREATE NEW ONE</button> </div>
        <label v-if="my_questions.length === 0">Вы не создали ни одного опроса
            <br>
            Нажмите кнопку "ADD QUESTION" что бы создать новый вопрос
        </label>
        <QuestionsList v-else v-bind:questions="my_questions"/>
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
                this.my_questions = result;
            },
            onError(error) {
                this.loading = false;
                this.error_label = error;
            },
            onAuthError() {
                http_service.logout();
                this.$router.push({ name: 'login', params: { redirectTo: '/my' } });
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