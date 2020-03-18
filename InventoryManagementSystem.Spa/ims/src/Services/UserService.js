import axios from 'axios';
import Constants from '../Constants';

const UserService = {
    login(userCred, callback, errCallback){
        axios.post(`${Constants.ApiUrl}/${Constants.Endpoints.Auth}/Authenticate`, userCred)
            .then(res => callback(res.data))
            .catch(err => errCallback(err.response.data))
    },
    register(user, callback, errCallback){
        axios.post(`${Constants.ApiUrl}/${Constants.Endpoints.Auth}/Register`, user)
            .then(res => callback(res.data))
            .catch(err => errCallback(err))
    }
}

export default UserService;