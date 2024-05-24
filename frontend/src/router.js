import { createRouter, createWebHistory } from 'vue-router'
import HomePage from '@/components/layout/view/User/HomePage/HomePage.vue';
import LoginForm from '@/components/layout/view/Share/Login/LoginForm.vue';
import RegisterForm from '@/components/layout/view/Share/Login/RegisterForm.vue';
import DictionaryPage from './components/layout/view/User/Dictionary/DictionaryPage.vue';
import UserInfomation from './components/layout/view/Share/Login/UserInfomation.vue';
import OrderDetail from './components/layout/view/Share/Order/OrderDetail.vue';
import DashboardView from './components/layout/view//Admin/Dashboard/DashboardView.vue';
import ManageOrder from './components/layout/view/Admin/ManageOrder/ManageOrder.vue';
import ManageProduct from './components/layout/view/Admin/ManageProduct/ManageProduct.vue';
import ManageCategory from './components/layout/view/Admin/ManageCategory/ManageCategory.vue';
import ManageUser from './components/layout/view/Admin/ManageUser/ManageUser.vue';
import ManageStock from './components/layout/view/Admin/ManageStock/ManageStock.vue';
import ProductForm from './components/layout/view/Admin/ManageProduct/ProductForm.vue';
import StockOrderDetail from './components/layout/view/Admin/ManageStock/StockOrderDetail.vue';
import ProductList from './components/layout/view/User/ListProduct/ProductList.vue';
import ProductView from './components/layout/view/User/ListProduct/ProductView.vue';
import ManageUserInfomation from './components/layout/view/Admin/ManageUser/UserInfomation.vue';

const routes = [
    {
        path: '/:catchAll(.*)',
        component: Error
    },
    {
        path: '/dictionary',
        component: DictionaryPage,
    },
    {
        path: '/products',
        component: ProductList,
    },
    {
        name:'DetailProduct',
        path: '/products/:id',
        component: ProductView,
    },
    {
        path: '/login',
        component: LoginForm
    },
    {
        path: '/register',
        component: RegisterForm
    },
    {
        path: '/userinfo',
        component: UserInfomation
    },
    {
        path: '/homepage',
        component: HomePage
    },
    {
        path: '/order/:id',
        component: OrderDetail
    },
    //dành cho người quản lý
    {
        path: '/dashboard',
        component: DashboardView
    },
    {
        path: '/manageorder',
        component: ManageOrder
    },
    {
        path: '/manageorder/:id',
        component: OrderDetail
    },
    {
        path: '/manageproduct',
        component: ManageProduct
    },
    {
        path: '/manageproduct/editproduct/:id',
        component: ProductForm
    },
    {
        path: '/manageproduct/addproduct',
        component: ProductForm
    },
    {
        path: '/managecategory',
        component: ManageCategory
    },
    {
        path: '/manageuser',
        component: ManageUser
    },
    
    {
        path: '/manageuser/:id',
        component: ManageUserInfomation
    },
    {
        path: '/managestock',
        component: ManageStock
    },
    {
        name: 'DetailStock',
        path: '/managestock/stockorder',
        component: StockOrderDetail,
        props: true
    },
]


const router = createRouter({
    history: createWebHistory(),
    routes: routes
})

export default router