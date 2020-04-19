<template>
    <div class="item">
        <span class="item-text">
            <span class="title">{{question.title}}</span>
            <span class="secondary">{{question.userId}}</span>
        </span>
        <span class="info">{{question.publishDate}}</span>
    </div>
</template>

<script>
    export default {
        name: "ListItem",
        props: ['question']
    }
</script>

<style scoped>
    .item {
        cursor: pointer;
        display: flex;
        position: relative;
        padding-left: 16px;
        padding-right: 16px;
        height: 72px;
        align-items: center;
    }

    .item:hover{background-color: var(--color-accent-transparent);}

    .item-text {
        display: flex;
        flex-direction: column;
        align-items: flex-start;
        align-self: flex-start;
    }

    .title {
        align-self: flex-start;
        font-size: var(--font-size-primary);
        margin-bottom: -20px;
        display: block;
        text-overflow: ellipsis;
        white-space: nowrap;
        overflow: hidden;
        margin-top: 0;
        line-height: normal;
    }

    .title::before {
        display: inline-block;
        height: 32px;
        content: "";
    }

    .title::after {
        display: inline-block;
        width: 0;
        height: 20px;
        content: "";
        vertical-align: -20px;
    }

    .secondary {
        align-self: flex-start;
        display: block;
        font-size: var(--font-size-secondary);
        color: var(--color-secondary)
    }
    .secondary::before {
        display: inline-block;
        width: 0;
        height: 20px;
        content: "";
        vertical-align: 0;
    }

    .info {
        display: block;
        margin-left: auto;
        margin-right: 0;
        font-size: var(--font-size-info);
        color: rgba(0,0,0,.6);
        line-height: 20px
    }
</style>