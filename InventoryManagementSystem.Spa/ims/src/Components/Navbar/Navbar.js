import React from 'react';
import { NavLink } from 'react-router-dom';
import SidebarMenu from '../SidebarMenu/SidebarMenu';

import './Navbar.css';

const Navbar = ({routes, currentUser, onLogOut}) => {
    return (
        <div className="navbar">
            <SidebarMenu routes={routes} />
            <NavLink className="navbar-link-logo" to="/">Inventory Management System</NavLink>
            <div className="auth-panel">
                <div className="user-profile">
                    <span className="user">
                        {currentUser.fname.charAt(0).toUpperCase() +
                            currentUser.lname.charAt(0).toUpperCase()}</span>
                    <div className="profile">
                        <h4 className="title">{currentUser.fname}&nbsp;{currentUser.lname}</h4>
                        <span className="email">{currentUser.email}</span>
                        <span className="logout" onClick={() => onLogOut()}>Logout</span>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Navbar;