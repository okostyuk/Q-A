<template>
    <div>
        <h2>Main page</h2>
        <hr>
        <label v-if="public_questions.length === 0">Еще никто не создал вопросов, будьте первым!</label>
        
        <hr>
        <button @click="addQuestion">СОЗДАТЬ ОПРОС</button>
    </div>
</template>

<script>
    export default {
        name: "Home",
        data: function () {
            return {
                public_questions: []
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