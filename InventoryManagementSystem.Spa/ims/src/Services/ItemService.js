import axios from 'axios';
import Constants from '../Constants';
import AuthService from './AuthService';

const ItemService = {
    getConfig(){
        return { headers: { Authorization: `Bearer ${AuthService.getAccessToken()}` } }
    },
    get(skip, take, callback){
        axios.get(`${Constants.ApiUrl}/${Constants.Endpoints.Item}?skip=${skip}&take=${take}`, this.getConfig())
            .then(res => callback(res.data, JSON.parse(res.headers["x-pagination"]).total));
    },
    getById(id, callback){
        axios.get(`${Constants.ApiUrl}/${Constants.Endpoints.Item}/${id}`, this.getConfig())
            .then(res => callback(res.data));
    },
    find(item, skip, take, callback){
        axios.get(`${Constants.ApiUrl}/${Constants.Endpoints.Item}/find${item.toQueryString()}&skip=${skip}&take=${take}`, this.getConfig())
            .then(res => callback(res.data, JSON.parse(res.headers["x-pagination"]).total));
    },
    insert(item, callback){
        axios.post(`${Constants.ApiUrl}/${Constants.Endpoints.Item}`, item, this.getConfig())
            .then(res => callback(res.data));
    },
    update(item, callback){
        axios.put(`${Constants.ApiUrl}/${Constants.Endpoints.Item}/${item.id}`, item, this.getConfig())
            .then(res => callback(res.data));
    },
    del(item, callback){
        axios.delete(`${Constants.ApiUrl}/${Constants.Endpoints.Item}/${item.id}`, this.getConfig())
            .then(res => callback(res.data));
    }
}

export default ItemService;