import React, { Component } from 'react';
import {NavLink} from 'react-router-dom';

import './Navbar.css';

class Navbar extends Component{
    constructor(props){
        super(props);
        this.loginModel = this.props.loginModel;
        this.registerModel = this.props.registerModel;
        this.LoginModal = this.props.LoginModal;
        this.RegisterModal = this.props.RegisterModal;
        this.userService = this.props.userService;
        this.authService = this.props.authService;
    }
    state = {
        loginModal: {show: false, errMsg: ''},
        registerModal: {show: false, errMsg: ''}    }

    hideLoginModal(){
        this.setState({loginModal: {show: false, errMsg: ''}})
    }

    hideRegisterModal(){
        this.setState({registerModal: {show: false, errMsg: ''}})
    }

    render(){
        return (
            <div className="navbar">
                <NavLink className="navbar-link-logo" to="/">Inventory Management System</NavLink>
                {
                    this.props.routes.map(route => {
                        return (<NavLink key={route.text} className="navbar-link" exact={route.exact}
                        activeClassName="navbar-link-active" to={route.to}>{route.text}</NavLink>)
                    })
                }
                <div className="auth-panel">
                    {
                        this.authService.isAuthorized()
                            ? <div className="user-profile">
                                <span className="user">
                                    {this.authService.getPayload().fname.charAt(0).toUpperCase() +
                                     this.authService.getPayload().lname.charAt(0).toUpperCase()}</span>
                                <div className="profile">
                                    <h4 className="title">{this.authService.getPayload().fname}&nbsp;{this.authService.getPayload().lname}</h4>
                                    <span className="email">{this.authService.getPayload().email}</span>
                                    <span className="logout"
                                        onClick={() => {
                                            this.authService.removeAccessToken();
                                            this.props.onAuthChanged();
                                        }}>Logout</span>
                                </div>
                            </div>
                            : <div>
                                <this.LoginModal
                                    errMsg={this.state.loginModal.errMsg}
                                    title="Login"
                                    show={this.state.loginModal.show}
                                    data={this.loginModel}
                                    onClose={() => this.hideLoginModal()}
                                    onAction={(user) => {
                                        this.userService.login(user, (token) => {
                                            this.authService.setAccessToken(token);
                                            this.hideLoginModal();
                                            this.props.onAuthChanged();
                                        }, (err) => {
                                            this.setState({ loginModal: { show: this.state.loginModal.show, errMsg: err } });
                                        });
                                    }} />
                                <this.RegisterModal
                                    errMsg={this.state.registerModal.errMsg}
                                    title="Register"
                                    show={this.state.registerModal.show}
                                    data={this.registerModel}
                                    onClose={() => this.hideRegisterModal()}
                                    onAction={(user) => {
                                        this.userService.register(user, (token) => {
                                            this.authService.setAccessToken(token);
                                            this.hideRegisterModal();
                                            this.props.onAuthChanged();
                                        }, (err) => console.log(err));
                                    }} />
                                <span className="login"
                                    onClick={() => { this.setState({ loginModal: { show: true } }) }}>Login</span>
                                <span className="register"
                                    onClick={() => { this.setState({ registerModal: { show: true } }) }}>Register</span>
                            </div>
                    }
                </div>
            </div>
        )
    }
}

export default Navbar;