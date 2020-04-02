import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';
import AuthService from '../../Services/AuthService';
import UserService from '../../Services/UserService';
import UserModel from '../../Models/UserModel';
import Modal from '../Modal/Modal';
import LoginForm from '../Auth/Login/Form/LoginForm';
import RegisterForm from '../Auth/Register/Form/RegisterForm';
import Constants from '../../Constants';

import './Navbar.css';

const LoginModal = Modal(LoginForm);
const RegisterModal = Modal(RegisterForm);
const service = new UserService(Constants.ApiUrl, Constants.Endpoints.Auth);

class Navbar extends Component {
    state = {
        showLoginModal: false,
        showRegisterModal: false
    }

    render() {
        return (
            <div className="navbar">
                <NavLink className="navbar-link-logo" to="/">Inventory Management System</NavLink>
                {
                    this.props.routes.map(route => <NavLink
                        key={route.text}
                        className="navbar-link"
                        exact={route.exact}
                        activeClassName="navbar-link-active"
                        to={route.to}>{route.text}</NavLink>)
                }
                <div className="auth-panel">
                {
                    AuthService.isAuthorized()
                        ? <div className="user-profile">
                            <span className="user">
                                {AuthService.getPayload().fname.charAt(0).toUpperCase() +
                                    AuthService.getPayload().lname.charAt(0).toUpperCase()}</span>
                            <div className="profile">
                                <h4 className="title">{AuthService.getPayload().fname}&nbsp;{AuthService.getPayload().lname}</h4>
                                <span className="email">{AuthService.getPayload().email}</span>
                                <span className="logout" onClick={() => {
                                        AuthService.removeAccessToken();
                                        this.props.onAuthChanged();}}>Logout</span>
                            </div>
                        </div>
                        : <div>
                            <LoginModal
                                title="Login"
                                show={this.state.showLoginModal}
                                value={UserModel.credentials}
                                onClose={() => this.setState({showLoginModal: false})}
                                service={service}
                                onSubmit={token => {
                                    AuthService.setAccessToken(token);
                                    this.props.onAuthChanged();}}/>
                            <RegisterModal
                                title="Register"
                                show={this.state.showRegisterModal}
                                value={UserModel.user}
                                onClose={() => this.setState({showRegisterModal: false})}
                                service={service}
                                onSubmit={token => {
                                    AuthService.setAccessToken(token);
                                    this.props.onAuthChanged();}} />
                            <span className="login"
                                onClick={() => this.setState({showLoginModal: true}) }>Login</span>
                            <span className="register"
                                onClick={() => this.setState({ showRegisterModal: true}) }>Register</span>
                        </div>
                }
                </div>
            </div>
        )
    }
}

export default Navbar;