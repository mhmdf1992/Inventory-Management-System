import React, { Component } from 'react';
import {BrowserRouter as Router, Route} from 'react-router-dom';
import AuthService from './Services/AuthService';
import Navbar from './Components/Navbar/Navbar';
import Routes from './Routes';

import './App.css';

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
              routes={this.state.routes.map(route => (({ to, text, exact }) => ({ to, text, exact }))(route) )}/>
          </div>
          <div className="page-container">
            {
              this.state.routes.map(route => <Route path={route.to} key={route.text} exact={route.exact}>
                {route.component}
              </Route>)
            }
          </div>
        </Router>
      </div>
    )
  }
}

export default App;
