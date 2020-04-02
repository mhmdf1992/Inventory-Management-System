import React, { Component, Fragment } from 'react';
import ControlPanel from '../ControlPanel/ControlPanel';
import ClientForm from '../Client/Form/ClientForm';
import Modal from '../Modal/Modal';
import Client from '../Client/Client/Client';
import List from '../List/List';
import PagingBar from '../PagingBar/PagingBar';
import ClientModel from '../../Models/ClientModel';
import ApiService from '../../Services/ApiService';
import Page from './Page';
import Constants from '../../Constants';

const ClientList = List(Client);
const Form = Modal(ClientForm);
const service = new ApiService(Constants.ApiUrl, Constants.Endpoints.Client);

class ClientsPage extends Component {
    state = {
        clients: [],
        total: 0,
        pageSize: 10,
        currentPage: 1,
        formTitle: '',
        formValue: ClientModel,
        showForm: false,
        findTxt: ''
    }

    componentDidMount() {
        this.handleListChange();
    }

    handleListChange() {
        service.get((this.state.currentPage - 1) * this.state.pageSize, this.state.pageSize,
            (list, total) => this.setState({ clients: list, total: total, showForm: false}));
    }

    handlePageChange(curr) {
        this.state.findTxt
            ? service.find(this.state.findTxt, (curr - 1) * this.state.pageSize, this.state.pageSize,
                (list, total) => this.setState({ clients: list, total: total, currentPage: curr }))
            : service.get((curr - 1) * this.state.pageSize, this.state.pageSize,
                (list, total) => this.setState({ clients: list, total: total, currentPage: curr }));
    }

    handleFindChange(txt) {
        txt 
            ? service.find(txt, 0, this.state.pageSize,
                (list, total) => this.setState({ clients: list, total: total, findTxt: txt, currentPage: 1 }))
            : service.get(0, this.state.pageSize,
                (list, total) => this.setState({ clients: list, total: total, currentPage: 1}));
    }

    render() {
        return (
            <Page
                header={<Fragment>
                    <ControlPanel
                        onAdd={() => this.setState({ showForm: true, formTitle: 'Add Client', formValue: ClientModel })}
                        onFind={txt => this.handleFindChange(txt)} />
                    <Form
                        title={this.state.formTitle}
                        value={this.state.formValue}
                        show={this.state.showForm}
                        onClose={() => this.setState({ showForm: false })}
                        onSubmit={() => this.handleListChange()}
                        service={service} />
                </Fragment>}
                body={<ClientList
                    value={this.state.clients}
                    onEdit={id => service.getById(id, res => this.setState({ showForm: true, formTitle: 'Edit Client', formValue: res }))}
                    onDelete={client => service.del(client, res => this.handleListChange())} />}
                footer={<PagingBar
                    size={this.state.pageSize}
                    total={this.state.total}
                    current={this.state.currentPage}
                    onChange={curr => this.handlePageChange(curr)} />} />
        )
    }
}

export default ClientsPage;