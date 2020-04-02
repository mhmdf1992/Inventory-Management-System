import React, { Component, Fragment } from 'react';
import ControlPanel from '../ControlPanel/ControlPanel';
import SupplierForm from '../Supplier/Form/SupplierForm';
import Modal from '../Modal/Modal';
import Supplier from '../Supplier/Supplier/Supplier';
import List from '../List/List';
import PagingBar from '../PagingBar/PagingBar';
import SupplierModel from '../../Models/SupplierModel';
import ApiService from '../../Services/ApiService';
import Page from './Page';
import Constants from '../../Constants';

const SupplierList = List(Supplier);
const Form = Modal(SupplierForm);
const service = new ApiService(Constants.ApiUrl, Constants.Endpoints.Supplier);

class SuppliersPage extends Component {
    state = {
        suppliers: [],
        total: 0,
        pageSize: 10,
        currentPage: 1,
        formTitle: '',
        formValue: SupplierModel,
        showForm: false,
        findTxt: ''
    }

    componentDidMount() {
        this.handleListChange();
    }

    handleListChange() {
        service.get((this.state.currentPage - 1) * this.state.pageSize, this.state.pageSize,
            (list, total) => this.setState({ suppliers: list, total: total, showForm: false}));
    }

    handlePageChange(curr) {
        this.state.findTxt
            ? service.find(this.state.findTxt, (curr - 1) * this.state.pageSize, this.state.pageSize,
                (list, total) => this.setState({ supplier: list, total: total, currentPage: curr }))
            : service.get((curr - 1) * this.state.pageSize, this.state.pageSize,
                (list, total) => this.setState({ suppliers: list, total: total, currentPage: curr }));
    }

    handleFindChange(txt) {
        txt 
            ? service.find(txt, 0, this.state.pageSize,
                (list, total) => this.setState({ suppliers: list, total: total, findTxt: txt, currentPage: 1 }))
            : service.get(0, this.state.pageSize,
                (list, total) => this.setState({ suppliers: list, total: total, currentPage: 1}));
    }

    render() {
        return (
            <Page
                header={<Fragment>
                    <ControlPanel
                        onAdd={() => this.setState({ showForm: true, formTitle: 'Add Supplier', formValue: SupplierModel })}
                        onFind={txt => this.handleFindChange(txt)} />
                    <Form
                        title={this.state.formTitle}
                        value={this.state.formValue}
                        show={this.state.showForm}
                        onClose={() => this.setState({ showForm: false })}
                        onSubmit={() => this.handleListChange()}
                        service={service} />
                </Fragment>}
                body={<SupplierList
                    value={this.state.suppliers}
                    onEdit={id => service.getById(id, res => this.setState({ showForm: true, formTitle: 'Edit Supplier', formValue: res }))}
                    onDelete={supplier => service.del(supplier, res => this.handleListChange())} />}
                footer={<PagingBar
                    size={this.state.pageSize}
                    total={this.state.total}
                    current={this.state.currentPage}
                    onChange={curr => this.handlePageChange(curr)} />} />
        )
    }
}

export default SuppliersPage;