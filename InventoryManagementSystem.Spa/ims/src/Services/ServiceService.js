import axios from 'axios';
import Constants from '../Constants';
import AuthService from './AuthService';

const ServiceService = {
    getConfig() {
        return { headers: { Authorization: `Bearer ${AuthService.getAccessToken()}` } }
    },
    get(skip, take, callback){
        axios.get(`${Constants.ApiUrl}/${Constants.Endpoints.Service}?skip=${skip}&take=${take}`, this.getConfig())
            .then(res => callback(res.data, JSON.parse(res.headers["x-pagination"]).total));
    },
    getById(id, callback){
        axios.get(`${Constants.ApiUrl}/${Constants.Endpoints.Service}/${id}`, this.getConfig())
            .then(res => callback(res.data));
    },
    find(item, skip, take, callback){
        axios.get(`${Constants.ApiUrl}/${Constants.Endpoints.Service}/find${item.toQueryString()}&skip=${skip}&take=${take}`, this.getConfig())
            .then(res => callback(res.data, JSON.parse(res.headers["x-pagination"]).total));
    },
    insert(service, callback){
        axios.post(`${Constants.ApiUrl}/${Constants.Endpoints.Service}`, service, this.getConfig())
            .then(res => callback(res.data));
    },
    update(service, callback){
        axios.put(`${Constants.ApiUrl}/${Constants.Endpoints.Service}/${service.id}`, service, this.getConfig())
            .then(res => callback(res.data));
    },
    del(service, callback){
        axios.delete(`${Constants.ApiUrl}/${Constants.Endpoints.Service}/${service.id}`, this.getConfig())
            .then(res => callback(res.data));
    }
}

export default ServiceService;