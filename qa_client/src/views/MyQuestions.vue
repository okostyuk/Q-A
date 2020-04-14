<template>
    <div>
        <h2>MyQuestions Page</h2>
        <hr>
            <label v-if="my_questions.length === 0">Вы не создали ни одного опроса
                <br>
                Нажмите кнопку "ADD QUESTION" что бы создать новый вопрос
            </label>
            <QuestionsList/>
        <hr>
        <button @click="addQuestion()">ADD QUESTION</button>
    </div>
</template>

<script>
    import QuestionsList from "../components/QuestionsList";
    export default {
        name: "MyQuestions",
        data: function () {
            return {
                my_questions: []
            }
        },
        methods: {
            showError(error) {
                this.error_label = error;
            },
            loadMyQuestions() {
                fetch("/questions/my")
                .then(response => {
                    if (response.status !== 200) {
                        this.loading = false;
                        this.showError("Response error: " + response.status)
                    } else {
                        response.json().then(
                            json => {
                                if (json.status === 'OK') {
                                    this.my_questions = json['questions']
                                } else {
                                    this.error_label = "Response error: " + response.status
                                }
                            })
                    }
                })
            },
            addQuestion() {
                this.$router.push('/add')
            }
        },
        components: {
            QuestionsList
        }
    }
</script>

<style scoped>
</style>