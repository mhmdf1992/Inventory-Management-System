import React from 'react';
import Home from './Components/Home/Home';
import ItemsPage from './Components/Pages/ItemsPage';
import ServicesPage from './Components/Pages/ServicesPage';
import ClientsPage from './Components/Pages/ClientsPage';
import SuppliersPage from './Components/Pages/SuppliersPage';

import './App.css';

const Routes = [
    {to: "/", text: "Home", exact: true, component: <Home />},
    {to: "/Items", auth: true, text: "Items", component: <ItemsPage />},
    {to: "/Services", auth: true, text: "Services", component: <ServicesPage />},
    {to: "/Clients", auth: true, text: "Clients", component: <ClientsPage />},
    {to: "/Suppliers", auth: true, text: "Suppliers", component: <SuppliersPage />},
    ]
export default Routes;