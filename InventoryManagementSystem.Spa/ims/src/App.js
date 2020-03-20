import React, { Component } from 'react';
import {BrowserRouter as Router, Route} from 'react-router-dom';
import AuthService from './Services/AuthService';
import Navbar from './Components/Navbar/Navbar';
import Routes from './Routes';
import Modal from './Components/Modal/Modal';
import LoginForm from './Components/Auth/Login/Form/LoginForm';
import RegisterForm from './Components/Auth/Register/Form/RegisterForm';
import UserService from './Services/UserService';
import UserModel from './Models/UserModel';

import './App.css';

const LoginModal = Modal(LoginForm);
const RegisterModal = Modal(RegisterForm);

class App extends Component {
  state = {
    routes: []
  }

  componentDidMount(){
    this.handleAuthChange();
  }

  handleAuthChange(){
    this.setState({routes: Routes.filter(route => AuthService.isAuthorized() 
      ? route 
      : (route.auth ? null : route))});
  }

  render(){
    return (
      <div className="App">
        <Router>
          <div className="navbar-container">
            <Navbar
              onAuthChanged={() => this.handleAuthChange()}
              userService={UserService}
              authService={AuthService} 
              LoginModal={LoginModal}
              RegisterModal={RegisterModal}
              loginModel={UserModel.credentials}
              registerModel={UserModel.user}
              routes={this.state.routes.map(route => 
                (({ to, text, exact }) => ({ to, text, exact }))(route) )}/>
          </div>
          <div className="page-container">
            {
              this.state.routes.map(route =>{
                return <Route path={route.to} key={route.text} exact={route.exact}>
                  {route.component}
                </Route>
              })
            }
          </div>
        </Router>
      </div>
    )
  }
}

export default App;
