import axios from 'axios';
import Constants from '../Constants';
import AuthService from './AuthService';

const SupplierService = {
    getConfig() {
        return { headers: { Authorization: `Bearer ${AuthService.getAccessToken()}` } }
    },
    get(skip, take, callback){
        axios.get(`${Constants.ApiUrl}/${Constants.Endpoints.Supplier}?skip=${skip}&take=${take}`, this.getConfig())
            .then(res => callback(res.data, JSON.parse(res.headers["x-pagination"]).total));
    },
    getById(id, callback){
        axios.get(`${Constants.ApiUrl}/${Constants.Endpoints.Supplier}/${id}`, this.getConfig())
            .then(res => callback(res.data));
    },
    find(supplier, skip, take, callback){
        axios.get(`${Constants.ApiUrl}/${Constants.Endpoints.Supplier}/find${supplier.toQueryString()}&skip=${skip}&take=${take}`, this.getConfig())
            .then(res => callback(res.data, JSON.parse(res.headers["x-pagination"]).total));
    },
    insert(supplier, callback){
        axios.post(`${Constants.ApiUrl}/${Constants.Endpoints.Supplier}`, supplier, this.getConfig())
            .then(res => callback(res.data));
    },
    update(supplier, callback){
        axios.put(`${Constants.ApiUrl}/${Constants.Endpoints.Supplier}/${supplier.id}`, supplier, this.getConfig())
            .then(res => callback(res.data));
    },
    del(supplier, callback){
        axios.delete(`${Constants.ApiUrl}/${Constants.Endpoints.Supplier}/${supplier.id}`, this.getConfig())
            .then(res => callback(res.data));
    }
}

export default SupplierService;