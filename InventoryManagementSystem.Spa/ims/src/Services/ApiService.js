import AuthService from './AuthService';
import axios from 'axios';
class ApiService {
    constructor(url, endpoint){
        this.url = url;
        this.endpoint = endpoint;
    }

    getConfig = () => {
        return {
            headers: {
                Authorization: `Bearer ${AuthService.getAccessToken()}`
            }
        }
    }

    get = (skip, take, callback) =>{
        axios.get(`${this.url}/${this.endpoint}?skip=${skip}&take=${take}`, this.getConfig())
            .then(res => callback(res.data, JSON.parse(res.headers["x-pagination"]).total));
    }

    getById = (id, callback) =>{
        axios.get(`${this.url}/${this.endpoint}/${id}`, this.getConfig())
            .then(res => callback(res.data));
    }

    find = (txt, skip, take, callback) =>{
        axios.get(`${this.url}/${this.endpoint}/find?txt=${txt}&skip=${skip}&take=${take}`, this.getConfig())
            .then(res => callback(res.data, JSON.parse(res.headers["x-pagination"]).total));
    }

    insert = (item, callback, callbackError) =>{
        axios.post(`${this.url}/${this.endpoint}`, item, this.getConfig())
            .then(res => callback(res.data))
            .catch(err => callbackError(err.response.data));
    }

    update = (item, callback, callbackError) =>{
        axios.put(`${this.url}/${this.endpoint}/${item.id}`, item, this.getConfig())
            .then(res => callback(res.data))
            .catch(err => callbackError(err.response.data));
    }

    del = (item, callback, callbackError) =>{
        axios.delete(`${this.url}/${this.endpoint}/${item.id}`, this.getConfig())
            .then(res => callback(res.data))
            .catch(err => callbackError(err.response.data));
    }
}

export default ApiService;