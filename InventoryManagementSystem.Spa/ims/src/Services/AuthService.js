const AuthService = {
    isAuthorized(){
        return this.getAccessToken() ? this.getPayload().exp > Date.now() / 1000 : false;
    },
    getPayload(){
        return this.getAccessToken() ? JSON.parse(atob(this.getAccessToken().split('.')[1])) : null;
    },
    setAccessToken(token){
        localStorage.setItem('access_token', token);
    },
    getAccessToken(){
        return localStorage.getItem('access_token');
    },
    removeAccessToken(){
        localStorage.removeItem('access_token');
    }
}
export default AuthService;