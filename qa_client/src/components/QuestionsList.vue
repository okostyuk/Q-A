<template>
    <div style="background: var(--color-bg); padding: 24px">
        <div class="list">
            <ListItem v-for="q in questions" :key="q.title" v-bind:item="q"/>
        </div>
    </div>
</template>

<script>
    import ListItem from "./ListItem";
    export default {
        name: "QuestionsList",
        props: ['questions'],
        methods: {
            showQuestion(_id) {
                this.$router.push({ name: 'question', params: { id: _id }})
            }
        },
        components: {
            ListItem
        }
    }
    
</script>

<style>
    .list {
        border: 1px solid #e5e5e5;
        padding-bottom: 12px;
        padding-top: 12px;
        background: white;
        width: 30%;
        margin-left: auto;
        margin-right: auto;
    }
    
    .questionItem {
        background: var(--color-accent-transparent);
        cursor: pointer;
        margin: auto;
        width: 400px;
        padding-top: 16px;
        min-height: 48px;
        border-bottom: 1px solid var(--color-placeholder);
    }
</style>