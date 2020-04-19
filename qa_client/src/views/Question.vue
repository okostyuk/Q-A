<template>
    <div>
        <h2>Question {{id}} Page</h2>
        <loader v-if="loading"/><br/>
        <label>{{errorText}}</label>
        <button @click="deleteQuestion">DELETE QUESTION</button>
        <button v-if="errorText" @click="loadQuestion">Try again</button>
        <div v-if="!loading">
            <label>{{question}}</label>
            <h3>{{question.title}}</h3>
            <ul v-if="question">
                <li v-for="answer in question.answers" :key="answer.id">
                    {{ answer.text }}
                    <button>Vote</button>
                </li>
            </ul>
            
        </div>
    </div>
</template>

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
                        router.push({ name: '/home'});
                    }
                });
            },
            loadQuestion() {
                this.loading = true;
                this.errorText = null;
                http_service.GET('/api/questions/'+this.id, this);
            },
            onSuccess(json) {
                this.loading = false;
                this.question = json.question;
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