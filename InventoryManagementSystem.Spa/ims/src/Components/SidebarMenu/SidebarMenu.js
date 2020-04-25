import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';
import './SidebarMenu.css';

class SidebarMenu extends Component{
    state = {
        show: false
    }

    render(){
        return(
            <div className="sidebar-menu">
                <div className={`hamburger ${this.state.show ? 'open' : ''}`}
                    onClick={() => this.setState({show: !this.state.show})}>
                    <span></span>
                    <span></span>
                    <span></span>
                </div>
                <div className={`menu ${this.state.show ? 'open' : ''}`}>
                {
                    this.props.routes.map(route => <NavLink
                        key={route.text}
                        className="menu-item"
                        exact={route.exact}
                        activeClassName="menu-item active"
                        to={route.to}>{route.text}</NavLink>)
                }
                </div>
            </div>
        )
    }
}

export default SidebarMenu;