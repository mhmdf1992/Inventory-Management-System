import React from 'react';
import {BrowserRouter as Router, Route} from 'react-router-dom';
import Navbar from './Components/Navbar/Navbar';
import './App.css';

const App = ({routes, currentUser, onLogOut}) =>{
  return (
    <div className="App">
      <Router>
        <div className="navbar-container">
          <Navbar
            currentUser={currentUser}
            onLogOut={onLogOut}
            routes={routes.map(route => (({ to, text, exact }) => ({ to, text, exact }))(route))} />
        </div>
        <div className="page-container">
          
            {routes.map(route => <Route path={route.to} key={route.text} exact={route.exact}>
              {route.component}
            </Route>)}
        </div>
      </Router>
    </div>
  )
}

export default App;
