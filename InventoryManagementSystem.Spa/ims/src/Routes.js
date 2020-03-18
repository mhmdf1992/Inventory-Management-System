import React from 'react';
import Home from './Components/Home/Home';
import List from './Components/List/List';
import ListControlPanel from './Components/ListControlPanel/ListControlPanel';
import Modal from './Components/Modal/Modal';

import './App.css';
import './Components/Navbar/Navbar.css'

//ItemList dependencies
import Item from './Components/Item/Item/Item';
import ItemForm from './Components/Item/Form/ItemForm';
import ItemService from './Services/ItemService';
import ItemModel from './Models/ItemModel';

//ServiceList dependencies
import Service from './Components/Service/Service/Service';
import ServiceForm from './Components/Service/Form/ServiceForm';
import ServiceService from './Services/ServiceService';
import ServiceModel from './Models/ServiceModel';

//SupplierList dependencies
import Supplier from './Components/Supplier/Supplier/Supplier';
import SupplierForm from './Components/Supplier/Form/SupplierForm';
import SupplierService from './Services/SupplierService';
import SupplierModel from './Models/SupplierModel';

//ClientList dependencies
import Client from './Components/Client/Client/Client';
import ClientForm from './Components/Client/Form/ClientForm';
import ClientService from './Services/ClientService';
import ClientModel from './Models/ClientModel';

//hoc Itemlist
const ItemModal = Modal(ItemForm);
const ItemList = List(ListControlPanel, ItemModal, Item);

//hoc Servicelist
const ServiceModal = Modal(ServiceForm);
const ServiceList = List(ListControlPanel, ServiceModal, Service);

//hoc Supplierlist
const SupplierModal = Modal(SupplierForm);
const SupplierList = List(ListControlPanel, SupplierModal, Supplier);

//hoc Clientlist
const ClientModal = Modal(ClientForm);
const ClientList = List(ListControlPanel, ClientModal, Client);

const Routes = [
    {to: "/", text: "Home", exact: true, component: <Home />},
    {to: "/Items", auth: true, text: "Items", component: <ItemList service={ItemService} model={ItemModel} pageSize={10}/>},
    {to: "/Services", auth: true, text: "Services", component: <ServiceList service={ServiceService} model={ServiceModel} pageSize={10} />},
    {to: "/Suppliers", auth: true, text: "Suppliers", component: <SupplierList service={SupplierService} model={SupplierModel} pageSize={10} />},
    {to: "/Clients", auth: true, text: "Clients", component: <ClientList service={ClientService} model={ClientModel} pageSize={10} />}
]
export default Routes;