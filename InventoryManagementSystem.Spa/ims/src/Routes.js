import React from 'react';
import Login from './Auth/Login';
import Register from './Auth/Register';
import Dashboard from './Components/Dashboard/Dashboard';
import ItemsPage from './Components/Pages/ItemsPage';
import ServicesPage from './Components/Pages/ServicesPage';
import ClientsPage from './Components/Pages/ClientsPage';
import SuppliersPage from './Components/Pages/SuppliersPage';

import './App.css';

const Routes = {
    unAuth: [
        {to: "/", exact: true, text: "Login", component: Login},
        {to: "/Register", text: "Register", component: Register}
    ],
    auth: [
        {to: "/", exact: true, text: "Dashboard", component: <Dashboard />},
        {to: "/Items",  text: "Items", component: <ItemsPage />},
        {to: "/Services",  text: "Services", component: <ServicesPage />},
        {to: "/Clients",  text: "Clients", component: <ClientsPage />},
        {to: "/Suppliers",  text: "Suppliers", component: <SuppliersPage />}
    ]
}
export default Routes;