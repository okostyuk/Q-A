import Vue from 'vue'
import Router from 'vue-router'
import Home from '@vviews/Home'

Vue.use(Router)

export default new Router({
    mode: 'history',
    routes: [
        {
            path: '/Home',
            component: Home
        },
        {
            path: '/my',
            component: () => import('./views/MyQuestions.vue')
        }
    ]
})
