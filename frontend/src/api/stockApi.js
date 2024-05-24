
import axios from "axios";
import VueCookies from 'vue-cookies';
/**
 * Tạo request() 
 */
const request = () => {
    return axios.create(
        {
            baseURL: 'http://localhost:60708/api/Stocks',
            timeout: 5000,
            headers: {
                Authorization: "Bearer " + VueCookies.get('token')
            }
        }
    )
}

export const apiGetAllStock = (filter) => {
    return request().post('GetAll', filter);
} 

