<template>
    <div style="background: var(--color-bg); padding: 24px">
        <div class="list content">
            <div @click="showQuestion(q.id)" v-for="q in questions" :key="q.id" >
                <ListItem v-bind:question="q"/>
            </div>
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
        //width: 30%;
        margin-left: auto;
        margin-right: auto;
    }
</style>