import React, { Component} from "react";
import {BrowserRouter as Router, Route} from 'react-router-dom';
import AuthService from './Services/AuthService';
import Routes from './Routes';
import Constants from "./Constants";
import UserService from './Services/UserService';
import App from "./App";

const service = new UserService(Constants.ApiUrl, Constants.Endpoints.Auth);

class Middleware extends Component{
    state = {
        routes: []
    }

    componentDidMount(){
        this.handleChange();
    }

    logOut(){
        AuthService.removeAccessToken().then(() => this.handleChange());
    }

    logIn(token){
        AuthService.setAccessToken(token).then(() => this.handleChange());
    }

    handleChange(){
        this.setState({routes: AuthService.isAuthorized()
            ? Routes.auth
            : (Routes.unAuth.map(route => {
                return {
                    ...route,
                    component: <route.component 
                        service={service} 
                        onSubmit={(token) => this.logIn(token)}/>}
                })
            )});
    }

    render(){
        return (
            AuthService.isAuthorized() 
                ? <App routes={this.state.routes} 
                    currentUser={AuthService.getPayload()}
                    onLogOut={() => this.logOut()} />
                : <Router>
                    {this.state.routes.map(route => 
                        <Route path={route.to} key={route.text} exact={route.exact}>
                            {route.component}
                        </Route>)}
                </Router>
        )
    }
}
export default Middleware;