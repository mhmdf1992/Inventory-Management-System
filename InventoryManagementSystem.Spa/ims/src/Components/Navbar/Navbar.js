import React from 'react';
import {NavLink} from 'react-router-dom';

const Navbar = () => {
    return (
        <div className="navbar">
            <NavLink className="navbar-link-logo" to="/">Inventory Management System</NavLink>
            <NavLink className="navbar-link" exact activeClassName="navbar-link-active" to="/">Home</NavLink>
            <NavLink className="navbar-link" activeClassName="navbar-link-active" to="/items">Items</NavLink>
            <NavLink className="navbar-link" activeClassName="navbar-link-active" to="/services">Services</NavLink>
        </div>
    )
}

export default Navbar;