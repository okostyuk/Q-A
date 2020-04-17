<template>
    <div class="listItem">
        <label>{{item.title}}</label>
        <label>{{item.subtitle}}</label>
    </div>
</template>

<script>
    export default {
        name: "ListItem",
        props: ['item']
    }
</script>

<style scoped>

</style>