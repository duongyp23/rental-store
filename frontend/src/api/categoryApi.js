
import axios from "axios";
import VueCookies from 'vue-cookies';
/**
 * Tạo request() 
 */
const request = () => {
    return axios.create(
        {
            baseURL: 'http://localhost:60708/api/Categorys',
            timeout: 5000,
            headers: {
                Authorization: "Bearer " + VueCookies.get('token')
            }
        }
    )
}

export const apiGetPagingCategory = (filter, pageSize, pageNumber) => {
    return request().post('Paging?pageSize=' + pageSize + '&pageNumber=' + pageNumber, filter)
}

export const apiInsertCategory = (category) => {
    return request().post('Add', category)
}

export const apiUpdateCategory = (category) => {
    return request().post('Update', category)
}

export const apiGetAllCategory = (filter) => {
    return request().post('GetAll', filter);
} 

export const apiGetCategoryOfProduct = (productId) => {
    return request().get('GetCategoryOfProduct?productId=' + productId);
}

export const apiDeleteCategory = (id) => {
    return request().delete('Delete/' + id);
}
