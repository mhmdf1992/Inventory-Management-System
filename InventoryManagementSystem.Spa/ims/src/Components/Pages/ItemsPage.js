import React, { Component, Fragment } from 'react';
import ControlPanel from '../ControlPanel/ControlPanel';
import ItemForm from '../Item/Form/ItemForm';
import Modal from '../Modal/Modal';
import Item from '../Item/Item/Item';
import List from '../List/List';
import PagingBar from '../PagingBar/PagingBar';
import ItemModel from '../../Models/ItemModel';
import ApiService from '../../Services/ApiService';
import Page from './Page';
import Constants from '../../Constants';

const ItemList = List(Item);
const Form = Modal(ItemForm);
const service = new ApiService(Constants.ApiUrl, Constants.Endpoints.Item);

class ItemsPage extends Component {
    state = {
        items: [],
        total: 0,
        pageSize: 10,
        currentPage: 1,
        formTitle: '',
        formValue: ItemModel,
        showForm: false,
        findTxt: ''
    }

    componentDidMount() {
        this.handleListChange();
    }

    handleListChange() {
        service.get((this.state.currentPage - 1) * this.state.pageSize, this.state.pageSize,
            (list, total) => this.setState({ items: list, total: total, showForm: false}));
    }

    handlePageChange(curr) {
        this.state.findTxt
            ? service.find(this.state.findTxt, (curr - 1) * this.state.pageSize, this.state.pageSize,
                (list, total) => this.setState({ items: list, total: total, currentPage: curr }))
            : service.get((curr - 1) * this.state.pageSize, this.state.pageSize,
                (list, total) => this.setState({ items: list, total: total, currentPage: curr }));
    }

    handleFindChange(txt) {
        txt 
            ? service.find(txt, 0, this.state.pageSize,
                (list, total) => this.setState({ items: list, total: total, findTxt: txt, currentPage: 1 }))
            : service.get(0, this.state.pageSize,
                (list, total) => this.setState({ items: list, total: total, currentPage: 1}));
    }

    render() {
        return (
            <Page
                header={<Fragment>
                    <ControlPanel
                        onAdd={() => this.setState({ showForm: true, formTitle: 'Add Item', formValue: ItemModel })}
                        onFind={txt => this.handleFindChange(txt)} />
                    <Form
                        title={this.state.formTitle}
                        value={this.state.formValue}
                        show={this.state.showForm}
                        onClose={() => this.setState({ showForm: false })}
                        onSubmit={() => this.handleListChange()}
                        service={service} />
                </Fragment>}
                body={<ItemList
                    value={this.state.items}
                    onEdit={id => service.getById(id, res => this.setState({ showForm: true, formTitle: 'Edit Item', formValue: res }))}
                    onDelete={item => service.del(item, res => this.handleListChange())} />}
                footer={<PagingBar
                    size={this.state.pageSize}
                    total={this.state.total}
                    current={this.state.currentPage}
                    onChange={curr => this.handlePageChange(curr)} />} />
        )
    }
}

export default ItemsPage;