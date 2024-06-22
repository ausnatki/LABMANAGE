import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

/* Layout */
import Layout from '@/layout'

// /* Router Modules */
// import componentsRouter from './modules/components'
// import chartsRouter from './modules/charts'
// import tableRouter from './modules/table'
// import nestedRouter from './modules/nested'

// import bak from './bak'

/**
 * Note: sub-menu only appear when route children.length >= 1
 * Detail see: https://panjiachen.github.io/vue-element-admin-site/guide/essentials/router-and-nav.html
 *
 * hidden: true                   if set true, item will not show in the sidebar(default is false)
 * alwaysShow: true               if set true, will always show the root menu
 *                                if not set alwaysShow, when item has more than one children route,
 *                                it will becomes nested mode, otherwise not show the root menu
 * redirect: noRedirect           if set noRedirect will no redirect in the breadcrumb
 * name:'router-name'             the name is used by <keep-alive> (must set!!!)
 * meta : {
    roles: ['admin','editor']    control the page roles (you can set multiple roles)
    title: 'title'               the name show in sidebar and breadcrumb (recommend set)
    icon: 'svg-name'/'el-icon-x' the icon show in the sidebar
    noCache: true                if set true, the page will no be cached(default is false)
    affix: true                  if set true, the tag will affix in the tags-view
    breadcrumb: false            if set false, the item will hidden in breadcrumb(default is true)
    activeMenu: '/example/list'  if set path, the sidebar will highlight the path you set
  }
 */

/**
 * constantRoutes
 * a base page that does not have permission requirements
 * all roles can be accessed
 */
// 静态路由
export const constantRoutes = [

  {
    path: '/redirect',
    component: Layout,
    hidden: true,
    children: [{
      path: '/redirect/:path(.*)',
      component: () => import('@/views/redirect/index')
    }]
  },
  {
    path: '/login',
    component: () => import('@/views/login/index'),
    hidden: true
  },
  {
    path: '/auth-redirect',
    component: () => import('@/views/login/auth-redirect'),
    hidden: true
  },
  {
    path: '/404',
    component: () => import('@/views/error-page/404'),
    hidden: true
  },
  {
    path: '/401',
    component: () => import('@/views/error-page/401'),
    hidden: true
  },
  {
    path: '/',
    component: Layout,
    redirect: '/dashboard',
    children: [{
      path: 'dashboard',
      component: () => import('@/views/dashboard/index'),
      name: 'Dashboard',
      meta: {
        title: 'dashboard',
        icon: 'dashboard',
        affix: true
      }
    }]
  },
  {
    path: '/guide',
    component: Layout,
    redirect: '/guide/index',
    children: [{
      path: 'index',
      component: () => import('@/views/guide/index'),
      name: 'Guide',
      meta: {
        title: 'guide',
        icon: 'guide',
        noCache: true
      }
    }]
  },
  {
    path: '/profile',
    component: Layout,
    redirect: '/profile/index',
    hidden: true,
    children: [{
      path: 'index',
      component: () => import('@/views/profile/index'),
      name: 'Profile',
      meta: {
        title: 'profile',
        icon: 'user',
        noCache: true
      }
    }]
  }
]

/**
 * asyncRoutes
 * the routes that need to be dynamically loaded based on user roles
 */
// 动态路由规则
export const asyncRoutes = [

  {
    path: '/user',
    component: Layout,
    redirect: '/user/list',
    alwaysShow: true,
    name: 'GoodManage',
    meta: {
      title: '用户管理',
      icon: 'guide',
      roles: ['admin']
    },
    children: [{
      path: 'list',
      component: () => import('@/views/user/index.vue'),
      name: 'acadmeyList',
      meta: {
        title: '用户列表',
        icon: 'guide',
        noCache: true,
        roles: ['admin']
      }
    }
    ]
  },
  {
    path: '/ditu',
    component: Layout,
    redirect: '/ditu/list',
    alwaysShow: true,
    name: 'GoodManage',
    meta: {
      title: '地图',
      icon: 'guide',
      roles: ['admin']
    },
    children: [{
      path: 'list',
      component: () => import('@/views/ditu/index.vue'),
      name: 'ditulist',
      meta: {
        title: 'ditu',
        icon: 'guide',
        noCache: true,
        roles: ['admin']
      }
    }
    ]
  },
  {
    path: '/academy',
    component: Layout,
    redirect: '/user/list',
    alwaysShow: true,
    name: 'GoodManage',
    meta: {
      title: '学院管理',
      icon: 'guide',
      roles: ['admin']
    },
    children: [{
      path: 'list',
      component: () => import('@/views/academy/index.vue'),
      name: 'acadmeyList',
      meta: {
        title: '学院列表',
        icon: 'guide',
        noCache: true,
        roles: ['admin']
      }
    }
    ]
  },
  {
    path: '/semesteres',
    component: Layout,
    redirect: '/semesteres/list',
    alwaysShow: true,
    name: 'GoodManage',
    meta: {
      title: '学期管理',
      icon: 'guide',
      roles: ['admin']
    },
    children: [{
      path: 'list',
      component: () => import('@/views/semesteres/index.vue'),
      name: 'acadmeyList',
      meta: {
        title: '学期列表',
        icon: 'guide',
        noCache: true,
        roles: ['admin']
      }
    }
    ]
  },
  {
    path: '/builder',
    component: Layout,
    redirect: '/builder/list',
    alwaysShow: true,
    name: 'GoodManage',
    meta: {
      title: '楼房管理',
      icon: 'guide',
      roles: ['admin']
    },
    children: [{
      path: 'builderlist',
      component: () => import('@/views/builder/index.vue'),
      name: 'builderList',
      meta: {
        title: '楼房列表',
        icon: 'guide',
        noCache: true,
        roles: ['admin']
      }
    },
    {
      path: 'floorlist',
      component: () => import('@/views/floor/index.vue'),
      name: 'builderList',
      meta: {
        title: '楼层列表',
        icon: 'guide',
        noCache: true,
        roles: ['admin']
      }
    },
    {
      path: 'lablist',
      component: () => import('@/views/laboratories/index.vue'),
      name: 'laboratoriesList',
      meta: {
        title: '实验室列表',
        icon: 'guide',
        noCache: true,
        roles: ['admin']
      }
    }
    ]
  },
  {
    path: '/check',
    component: Layout,
    redirect: '/check/list',
    alwaysShow: true,
    name: 'CheckManage',
    meta: {
      title: '日志管理',
      icon: 'guide',
      roles: ['admin', 'managers', 'maintenance']
    },
    children: [{
      path: 'dialChecklist',
      component: () => import('@/views/dailSafetyCheck/index.vue'),
      name: 'dialCheckList',
      meta: {
        title: '日志记录',
        icon: 'guide',
        noCache: true,
        roles: ['admin', 'managers']
      }
    },
    {
      path: 'repairlist',
      component: () => import('@/views/handing/index.vue'),
      name: 'repairList',
      meta: {
        title: '异常记录',
        icon: 'guide',
        noCache: true,
        roles: ['admin', 'managers']
      }
    },
    {
      path: 'handinglist',
      component: () => import('@/views/repair/index.vue'),
      name: 'handingList',
      meta: {
        title: '维修记录',
        icon: 'guide',
        noCache: true,
        roles: ['admin', 'maintenance']
      }
    }
    ]
  }

  // 自带模块

  // componentsRouter,
  // chartsRouter,
  // nestedRouter,
  // tableRouter,
  // ...bak,
]

const createRouter = () => new Router({
  // mode: 'history', // require service support
  scrollBehavior: () => ({
    y: 0
  }),
  routes: constantRoutes
})

const router = createRouter()

// Detail see: https://github.com/vuejs/vue-router/issues/1234#issuecomment-357941465
export function resetRouter() {
  const newRouter = createRouter()
  router.matcher = newRouter.matcher // reset router
}

export default router
