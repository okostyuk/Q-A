<template>
    <div>
        <h2>MyQuestions Page</h2>
        <hr>
            <label v-if="my_questions.length === 0">Вы не создали ни одного опроса
                <br>
                Нажмите кнопку "ADD QUESTION" что бы создать новый вопрос</label>
        <hr>
        <button @click="addQuestion()">ADD QUESTION</button>
    </div>
</template>

<script>
    export default {
        name: "MyQuestions",
        data: function () {
            return {
                my_questions: []
            }
        },
        methods: {
            addQuestion() {
                this.$router.push('/add')
            }
        }
    }
</script>

<style scoped>
</style>