import axios from 'axios';
import ApiService from './ApiService';

class UserService extends ApiService {

    login = (userCred, callback, callbackError) =>{
        axios.post(`${this.url}/${this.endpoint}/Authenticate`, userCred)
            .then(res => callback(res.data))
            .catch(err => callbackError(err.response.data));
    }

    register = (user, callback, callbackError) =>{
        axios.post(`${this.url}/${this.endpoint}/Register`, user)
            .then(res => callback(res.data))
            .catch(err => callbackError(err.response.data));
    }   
}
export default UserService;