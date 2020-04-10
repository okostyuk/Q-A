import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

export default new Router({
    mode: 'history',
    routes: [
        {
            path: '/',
            component: () => import('./views/Login.vue')
        },
        {
            path: '/my',
            component: () => import('./views/MyQuestions.vue')
        },
        {
            path: '/home',
            component: () => import('./views/Home.vue')
        }
    ]
})
