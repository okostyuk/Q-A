<template>
    <div>
        <h3>New question</h3>
        
        <div id="root">
            <label class="error">{{error_text}}</label>
            <div id="form">
                <label>
                    Вопрос<br>
                    <textarea v-model="question.clientTitle" placeholder="Напишите текст вопроса"/>
                </label>
                <br>
                <label>
                    Дедлайн после которого новые ответы не принимаются<br>
                    <input v-model="question.clientExpiresDate" type="date">
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
                    <div v-for="(answer, index) in question.clientAnswers" v-bind:key="answer.id" >
                        <input class="answer" v-model='answer.text' placeholder="Напишите текст ответа"> 
                        <button @click="deleteAnswer(index)" v-if="question.clientAnswers.length > 0">delete</button>
                    </div>
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
    import http_service from "../http-service";
    export default {
        name: 'AddQuestion',
        data: function () {
            return {
                error_text: "test",
                customAnswersAllowed: false,
                maxCustomAnswers: 1,
                question: {
                    clientTitle: '',
                    clientMaxCustomAnswers: 0,
                    clientExpiresDate: '',
                    clientAnswers: [
                        {id: 0, text: ""}
                    ]                
                },
            }
        },
        methods: {
            deleteAnswer(index) {
                if (this.question.clientAnswers.length > 1) {
                    this.question.clientAnswers.splice(index,1);
                }
            },
            addAnswer() {
                let _id = this.question.clientAnswers[this.question.clientAnswers.length-1].id + 1;
                this.question.clientAnswers.push({
                    id: _id,
                    text: ""
                })
            },
            goBack() {
                window.history.length > 1 ? this.$router.go(-1) : this.$router.push('/')
            },
            publish() {
                this.addQuestion(true)
            },
            save() {
                this.addQuestion(false)
            },
            addQuestion(publish) {
                this.question.clientPublish = publish;
                if (this.customAnswersAllowed) {
                    this.question.clientMaxCustomAnswers = 0;    
                } else {
                    this.question.clientMaxCustomAnswers = this.maxCustomAnswers;
                }
                http_service.POST('/api/questions/add', this, this.question);
            },
            onSuccess(response) {
                console.log(response);
                alert("SUCCESS");
            },
            onError(error) {
                console.log(error);
                this.error_text = error;
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
        width: 600px;
    }
    
    #root {
        margin-bottom: 100px;
    }

</style>