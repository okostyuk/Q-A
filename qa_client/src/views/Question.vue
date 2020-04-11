<template>
    <div>
        <h2>Question {{id}} Page</h2>
        <loader v-if="loading"/><br/>
        <label>{{errorText}}</label>
        <button v-if="errorText" @click="loadQuestion">Try again</button>
        <div v-if="!loading">
            <label v-if="question">{{question.title}}</label>
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
    import Loader from '@/components/Loader'
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
            loadQuestion(/*id*/) {
                this.loading = true;
                this.errorText = null;
                //fetch('/api/questions/'+id).then(result => result.json())
                this.mockFetch()
                    .catch(reason => {this.errorText = reason.error})
                    .then(json => {
                        alert("success");
                        this.loading = false;
                        if (json === undefined) {
                            this.errorText = "Unknown error"
                            this.question = null;
                        }else if (json.error == null) {
                            this.errorText = null;
                            this.question = json;
                        } else {
                            this.question = null;
                            this.errorText = json.error
                        }
                    }, () => {
                        alert("failed");
                        this.question = null;
                        this.errorText = "testerror"
                    })
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
            //alert("mounted");
            this.loadQuestion();
        }
    }
    
</script>