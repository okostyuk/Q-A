<template>
    <div>
        <h3>New question</h3>
        
        <div id="root">
            <div id="form">
                <label>
                    Вопрос<br>
                    <textarea v-model="title" placeholder="Напишите текст вопроса"/>
                </label>
                <br>
                <label>
                    Дедлайн после которого новые ответы не принимаются<br>
                    <input v-model="expiresDate" type="date">
                </label>
                <br>
                <label>
                    <input v-model="customAnswersAllowed" type="checkbox"/>
                    Могут ли участники добавлять свои варианты ответов
                </label>
                <br>
                <label style="margin-left: 48px">
                    Cколько максимум можно добавить вариантов ответов<br>
                    <input id="maxCustomAnswers" v-model="maxCustomAnswers" type="number" v-bind:disabled="!customAnswersAllowed">
                </label>
                <hr>
                Ответы:
                <br/>
                <input class="answer" v-for="answer in answers" :key="answer.text" placeholder="Напишите текст ответа">
                <button @click="addAnswer()">Добавиь вариант ответа</button>
            </div>
            <div id="buttons">
                <button @click="save">Сохранить</button>
                <button @click="publish">Опубликовать</button>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'AddQuestion',
        data: function () {
            return {
                title: '',
                maxCustomAnswers: 1,
                expiresDate: '',
                customAnswersAllowed: false,
                voteVariantsCount: 1,
                answers: [
                    {text: ''}
                ]
            }
        },
        methods: {
            addAnswer() {
                this.answers.push({text: ''})
            },
            goBack() {
                window.history.length > 1 ? this.$router.go(-1) : this.$router.push('/')
            },
            publish() {
                this.addQuestion(new Date())
            },
            save() {
                this.addQuestion(null)
            },
            addQuestion(publishDate) {
                fetch('/api/questions/add',
                    {
                        method: 'POST',
                        headers: {'Content-Type': 'application/json'},
                        body: JSON.stringify({
                            title: this.title,
                            answers: this.answers,
                            expiresDate: this.expiresDate,
                            publishDate: publishDate,
                            maxCustomAnswers: this.customAnswersAllowed ? 0 : this.maxCustomAnswers
                        })
                    }
                ).then(
                    response => {
                        if (response.status !== 200) {
                            alert("FAILED: " + response.status)
                        } else {
                            response.json().then(json => {
                                if (json.error !== undefined) {
                                    alert("ERROR: " + json.error)
                                } else {
                                    alert("SUCCESS")
                                }
                            })          
                        }
                    },
                    () => {alert("FAILED")}
                )
            }
        }
    }
</script>

<style>
    #form {
        padding: 10px;
        border: 1px solid #42b983;
        text-align: left; 
        width: 720px;
        margin: auto auto 10px;
    }
    
    #buttons {
        text-align: right;
        margin: auto;
        width: 760px;
    }
    
    textarea {
        width: 700px;
        height: 100px;
    }

    label {
        margin-top: 12px;
    }
    
    textarea,
    input {
        margin-bottom: 12px;
    }
    
    #maxCustomAnswers {
        margin-left: 48px;
        min-width: 0;
        width: 160px;
    }

    input[type="checkbox"] {
        min-width: 0;
        margin-bottom: 0;
    }
    
    .answer {
        width: 700px;
    }
    
    #root {
        margin-bottom: 100px;
    }

</style>