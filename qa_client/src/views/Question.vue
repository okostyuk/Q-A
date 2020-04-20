<template>
    <div>
        <h2>Question {{id}} Page</h2>
        <loader v-if="loading"/><br/>
        <label>{{errorText}}</label>
        <div class="content" style="text-align: right">
            <button @click="deleteQuestion">DELETE QUESTION</button>
            <button v-if="errorText" @click="loadQuestion">Try again</button>
        </div>
        <div v-if="!loading">
            <label>{{question}}</label>
            <h3>{{question.title}}</h3>
            <form class="content">
                <div class="list_item" v-for="answer in question.answers" :key="answer.id">
                    <label>
                        <input type="checkbox" class="checkbox"/>
                        {{ answer.text }}
                    </label>
                </div>
            </form>
        </div>
    </div>
</template>

<style>
    .checkbox {
        margin-bottom: 0;
        margin-right: 24px;
        min-height: 24px;
        min-width: 24px;
    }
</style>

<script>
    import http_service from "../http-service";
    import Loader from '../components/Loader'
    export default {
        props: [
            'id'
        ],
        name: 'QuestionView',
        data: function () {
            return {
                loading: true,
                errorText: '',
                question: {},
                question2: {
                    id: '1',
                    userId: '2',
                    title: 'How much is the fish?',
                    expiresDate: 'tomorrow',
                    publishDate: 'yesterday',
                    maxCustomAnswers: 2,
                    published: true,
                    answers: [
                        {id: '0', text: 'answer1'},
                        {id: '1', text: 'answer2'}
                    ]
                }
            }
        },
        methods: {
            deleteQuestion() {
                const router = this.$router;
                http_service.DELETE('/api/questions/'+this.id, {
                    onSuccess() {
                        router.push('/home');
                    }
                });
            },
            loadQuestion() {
                this.loading = true;
                this.errorText = null;
                http_service.GET('/api/questions/'+this.id, this);
            },
            onSuccess(_question) {
                this.loading = false;
                this.question = _question;
            },
            onError(error_text) {
                this.loading = false;
                this.errorText = error_text;
            },
            mockFetch() {
                this.loading = true;
                return new Promise((resolve, reject) => {
                    setTimeout(() => {
                        // Успех в половине случаев.
                        if (Math.random() > .5) {
                            resolve({
                                id: '1',
                                userId: '2',
                                title: 'How much is the fish?',
                                expiresDate: 'tomorrow',
                                publishDate: 'yesterday',
                                maxCustomAnswers: 2,
                                published: true,
                                answers: [
                                    {id: '0', text: 'answer1'},
                                    {id: '1', text: 'answer2'}
                                ]
                            })
                        } else {
                            reject({status: 'Error', error: 'Some Error'})
                        }                    
                        },
                        1000);
                })
            }
        },
        components: {
            Loader
        },
        mounted() {
            this.loadQuestion();
        }
    }
    
</script>