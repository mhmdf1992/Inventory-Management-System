import React, { Component, Fragment } from 'react';
import ControlPanel from '../ControlPanel/ControlPanel';
import ServiceForm from '../Service/Form/ServiceForm';
import Modal from '../Modal/Modal';
import Service from '../Service/Service/Service';
import List from '../List/List';
import PagingBar from '../PagingBar/PagingBar';
import ServiceModel from '../../Models/ServiceModel';
import ApiService from '../../Services/ApiService';
import Page from './Page';
import Constants from '../../Constants';

const ServiceList = List(Service);
const Form = Modal(ServiceForm);
const service = new ApiService(Constants.ApiUrl, Constants.Endpoints.Service);

class ServicesPage extends Component {
    state = {
        services: [],
        total: 0,
        pageSize: 10,
        currentPage: 1,
        formTitle: '',
        formValue: ServiceModel,
        showForm: false,
        findTxt: ''
    }

    componentDidMount() {
        this.handleListChange();
    }

    handleListChange() {
        service.get((this.state.currentPage - 1) * this.state.pageSize, this.state.pageSize,
            (list, total) => this.setState({ services: list, total: total, showForm: false}));
    }

    handlePageChange(curr) {
        this.state.findTxt
            ? service.find(this.state.findTxt, (curr - 1) * this.state.pageSize, this.state.pageSize,
                (list, total) => this.setState({ services: list, total: total, currentPage: curr }))
            : service.get((curr - 1) * this.state.pageSize, this.state.pageSize,
                (list, total) => this.setState({ services: list, total: total, currentPage: curr }));
    }

    handleFindChange(txt) {
        txt 
            ? service.find(txt, 0, this.state.pageSize,
                (list, total) => this.setState({ services: list, total: total, findTxt: txt, currentPage: 1 }))
            : service.get(0, this.state.pageSize,
                (list, total) => this.setState({ services: list, total: total, currentPage: 1}));
    }

    render() {
        return (
            <Page
                header={<Fragment>
                    <ControlPanel
                        onAdd={() => this.setState({ showForm: true, formTitle: 'Add Service', formValue: ServiceModel })}
                        onFind={txt => this.handleFindChange(txt)} />
                    <Form
                        title={this.state.formTitle}
                        value={this.state.formValue}
                        show={this.state.showForm}
                        onClose={() => this.setState({ showForm: false })}
                        onSubmit={() => this.handleListChange()}
                        service={service} />
                </Fragment>}
                body={<ServiceList
                    value={this.state.services}
                    onEdit={id => service.getById(id, res => this.setState({ showForm: true, formTitle: 'Edit Service', formValue: res }))}
                    onDelete={service => service.del(service, res => this.handleListChange())} />}
                footer={<PagingBar
                    size={this.state.pageSize}
                    total={this.state.total}
                    current={this.state.currentPage}
                    onChange={curr => this.handlePageChange(curr)} />} />
        )
    }
}

export default ServicesPage;