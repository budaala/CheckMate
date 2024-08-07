
/**
 * router/index.ts
 *
 * Automatic routes for `./src/pages/*.vue`
 */

// Composables
import { createRouter, createWebHistory } from 'vue-router'
import ToDo from '../pages/ToDoPage.vue'
import index from '../pages/index.vue'
import about from '../pages/about.vue'

const routes = [
  {
    path: '/',
    component: index
  },
  {
    path: '/todo',
    name: 'ToDo',
    component: ToDo
  },
  {
    path: '/about',
    name: 'About',
    component: about
  }
  // other routes...
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
})

export default router