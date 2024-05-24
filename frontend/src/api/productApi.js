
import axios from "axios";
import VueCookies from 'vue-cookies';
/**
 * Tạo request() 
 */
const request = () => {
    return axios.create(
        {
            baseURL: 'http://localhost:60708/api/Products',
            timeout: 5000,
            headers: {
                Authorization: "Bearer " + VueCookies.get('token')
            }
        }
    )
}

export const apiGetPagingProduct = (filter, pageSize, pageNumber, ListCategoryId) => {
    return request().post('PagingProduct?pageSize=' + pageSize + '&pageNumber=' + pageNumber, { filters: filter,listCategoryId: ListCategoryId })
}

export const apiInsertProduct = (product, selectCategory) => {
    return request().post('AddProduct', { product, selectCategory } )
}

export const apiUpdateProduct = (product, selectCategory) => {
    return request().post('UpdateProduct', { product, selectCategory })
}

export const apiGetAllProduct = (filter) => {
    return request().post('GetAll', filter);
} 

export const apiAddProductToCart = (productId, userId, optionCode, quantity) => {
    return request().post('AddProductToCart?productId=' + productId + '&userId=' + userId+'&optionCode='+optionCode+'&quantity='+quantity);
}

export const apiDeleteProduct = (id) => {
    return request().delete('Delete/' + id);
}

export const apiGetProduct = (id) => {
    return request().get(id);
}
