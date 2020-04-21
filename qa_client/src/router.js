import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router);

export default new Router({
    mode: 'history',
    routes: [
        { path: '/index.html', component: () => import('./views/Login.vue') },
        { path: '/', component: () => import('./views/Login.vue') },
        { name: 'login', path: '/login/:redirectTo', component: () => import('./views/Login.vue') },
        { path: '/login', component: () => import('./views/Login.vue') },
        { path: '/my', component: () => import('./views/MyQuestions.vue') },
        { path: '/home', component: () => import('./views/Home.vue') },
        { path: '/add',  component: () => import('./views/AddQuestion.vue') },
        {
            path: '/questions/:id',
            name: 'question',
            props: true,
            component: () => import('./views/Question')
            
        }
    ]
})
