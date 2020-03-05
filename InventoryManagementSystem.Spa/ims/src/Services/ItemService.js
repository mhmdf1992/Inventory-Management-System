import axios from 'axios';
import Constants from '../Constants';

const ItemService = {
    get(skip, take, callback){
        axios.get(`${Constants.ApiUrl}/${Constants.Endpoints.Item}?skip=${skip}&take=${take}`)
            .then(res => callback(res.data, JSON.parse(res.headers["x-pagination"]).total));
    },
    getById(id, callback){
        axios.get(`${Constants.ApiUrl}/${Constants.Endpoints.Item}/${id}`)
            .then(res => callback(res.data));
    },
    find(item, skip, take, callback){
        axios.get(`${Constants.ApiUrl}/${Constants.Endpoints.Item}/find${item.toQueryString()}&skip=${skip}&take=${take}`)
            .then(res => callback(res.data, JSON.parse(res.headers["x-pagination"]).total));
    },
    insert(item, callback){
        axios.post(`${Constants.ApiUrl}/${Constants.Endpoints.Item}`, item)
            .then(res => callback(res.data));
    },
    update(item, callback){
        axios.put(`${Constants.ApiUrl}/${Constants.Endpoints.Item}/${item.id}`, item)
            .then(res => callback(res.data));
    },
    del(item, callback){
        axios.delete(`${Constants.ApiUrl}/${Constants.Endpoints.Item}/${item.id}`)
            .then(res => callback(res.data));
    }
}

export default ItemService;