<template>
    <div>
        <h2>Main page</h2>
        <hr>
        <label v-if="public_questions.length === 0">Еще никто не создал вопросов, будьте первым!</label>
        <QuestionsList v-else v-bind:questions="public_questions"/>
        <hr>
        <button @click="addQuestion">СОЗДАТЬ ОПРОС</button>
    </div>
</template>

<script>
    import QuestionsList from "../components/QuestionsList";
    export default {
        name: "Home",
        data: function () {
            return {
                public_questions: [
                    {id: 1, title: "q1"},
                    {id: 2, title: "q2"},
                    {id: 3, title: "q3"},
                    {id: 4, title: "q4"},
                ]
            }
        },
        methods: {
            addQuestion() {
                this.$router.push('/add')
            },
            loadQuestions() {
                fetch('/api/questions')
            }
        },
        components: {
            QuestionsList
        },
        mounted() {
            this.loadQuestions();
        }
    }
</script>

<style scoped>

</style>