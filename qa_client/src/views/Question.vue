<template>
    <div style="padding-bottom: 48px">
        <h2>Question {{id}} Page</h2>
        <loader v-if="loading"/><br/>
        <label>{{errorText}}</label>
        <div v-if="!loading">
            <label style="font-size: 14px">{{question}}</label>
            <h3>{{question.title}}</h3>
            
            <div class="container left">Выберите вариант ответа</div>
            <div class="content">
                <div class="list_item" v-bind:class="{ list_item: true, voted: !canVote(answer.id)}" 
                     @click="vote(answer.id)"
                     v-for="answer in question.answers" :key="answer.id">
                    <input type="checkbox" class="checkbox" :checked="voted(answer.id)" :disabled="voted(answer.id)"/>
                    <label style="height: 24px;">{{ answer.text }} votes({{answer.votesCount}})</label>
                </div>
                <div v-if="canVoteAtAll() && question.maxCustomAnswers > 0" class="list_item">
                    <input type="text" class="customAnswer" v-model="customAnswer">
                    <button @click="voteCustom()">VOTE</button>
                </div>
            </div>
        </div>
        <div class="container right" v-if="userId == question.userId">
            <button @click="deleteQuestion">DELETE QUESTION</button>
            <button v-if="errorText" @click="loadQuestion">Try again</button>
        </div>
    </div>
</template>

<style>
    .customAnswer {
        width: 100%; margin-top: 22px; margin-right: 16px
    }
    .checkbox {
        margin: 0 16px 0 0;
        min-height: 24px;
        min-width: 24px;
    }
    .voted {
        cursor: not-allowed;
    }
</style>

<script>
    import qa_rest_service from "../qa-rest-service";
    import http_service from "../http-service";
    import Loader from '../components/Loader'
    export default {
        props: [
            'id'
        ],
        name: 'QuestionView',
        data: function () {
            return {
                customAnswer: "",
                userId: http_service.userId(),
                loading: true,
                errorText: '',
                question: {
                    maxVoteVariants: 1,
                    maxCustomAnswers: 0,
                    votes: []
                },
            }
        },
        computed: {
            myVotes: function() {
                let result = 0;
                for (let vote of this.question.votes) {
                    if (vote.userId == this.userId) {
                        result++;
                    }
                }
                return result;
            }
        },
        methods: {
            voteCustom() {
                let listener = this;
                let customAnswer = {
                    text: this.customAnswer,
                    questionId: this.question.id
                };
                http_service.POST(
                    '/api/questions/voteCustom',
                    listener,
                    customAnswer);
            },
            canVoteAtAll() {
                return this.myVotes < this.question.maxVoteVariants;
            },
            canVote(answerId) {
                return this.canVoteAtAll() && !this.voted(answerId);
            },
            voted(answerId) {
                for (let vote of this.question.votes) {
                    if (answerId === vote.answerId && this.userId == vote.userId) {
                        return true;
                    } else {
                        console.log(answerId + " == " + vote.answerId + " = false");
                    }
                }
                return false;
            },
            vote(answerId){
                if (!this.canVote(answerId)) {
                    return;
                }
                let questionComponent = this;
                http_service.POST('/api/questions/'+this.question.id+'/vote/'+answerId,
                    {
                        onSuccess(result) {
                            questionComponent.question = result;
                        },
                        onError(error_text) {
                            questionComponent.onError(error_text);
                        }
                    }, {}
                )
            },
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
                qa_rest_service.getQuestion(this.id)
                    .then(_question => this.onSuccess(_question))
                    .catch(error => this.onError(error));
            },
            onSuccess(_question) {
                this.loading = false;
                this.question = _question;
                console.log("question.userId = " + this.question.userId);
                console.log("userId = " + this.userId);
                let equals = (this.question.userId == this.userId);
                console.log("this.question.userId == this.userId " + equals);
            },
            onError(error_text) {
                this.loading = false;
                this.errorText = error_text;
            },
            onAuthError() {
                http_service.logout();
                this.$router.push({ name: 'login', params: { redirectTo: '/questions/'+this.id } });
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