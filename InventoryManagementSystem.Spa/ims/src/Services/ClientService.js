import axios from 'axios';
import Constants from '../Constants';
import AuthService from './AuthService';

const ClientService = {
    getConfig(){ return {headers: { Authorization: `Bearer ${AuthService.getAccessToken()}`}}
    },
    get(skip, take, callback){
        axios.get(`${Constants.ApiUrl}/${Constants.Endpoints.Client}?skip=${skip}&take=${take}`, this.getConfig())
            .then(res => callback(res.data, JSON.parse(res.headers["x-pagination"]).total));
    },
    getById(id, callback){
        axios.get(`${Constants.ApiUrl}/${Constants.Endpoints.Client}/${id}`, this.getConfig())
            .then(res => callback(res.data));
    },
    find(client, skip, take, callback){
        axios.get(`${Constants.ApiUrl}/${Constants.Endpoints.Client}/find${client.toQueryString()}&skip=${skip}&take=${take}`, this.getConfig())
            .then(res => callback(res.data, JSON.parse(res.headers["x-pagination"]).total));
    },
    insert(client, callback){
        axios.post(`${Constants.ApiUrl}/${Constants.Endpoints.Client}`, client, this.getConfig())
            .then(res => callback(res.data));
    },
    update(client, callback){
        axios.put(`${Constants.ApiUrl}/${Constants.Endpoints.Client}/${client.id}`, client, this.getConfig())
            .then(res => callback(res.data));
    },
    del(client, callback){
        axios.delete(`${Constants.ApiUrl}/${Constants.Endpoints.Client}/${client.id}`, this.getConfig())
            .then(res => callback(res.data));
    }
}

export default ClientService;