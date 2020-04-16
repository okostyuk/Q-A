<template>
    <div>
        <div v-for="q in questions" :key="q.title"  @click="showQuestion(q.id)">
            <p class="questionItem">
                {{q}}
            </p>
        </div>
    </div>
</template>

<script>
    export default {
        name: "QuestionsList",
        props: ['questions'],
        methods: {
            showQuestion(_id) {
                this.$router.push({ name: 'question', params: { id: _id }})
            }
        }
    }
    
</script>

<style>
    .questionItem {
        background: var(--primary-color-transparent);
        cursor: pointer;
        margin: auto;
        width: 400px;
        padding-top: 16px;
        min-height: 48px;
        border-bottom: 1px solid var(--placeholder-color);
    }
</style>