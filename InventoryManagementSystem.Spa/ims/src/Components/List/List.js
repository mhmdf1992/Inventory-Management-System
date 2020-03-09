import React, {Component} from 'react';

import './List.css';
import PagingBar from '../PagingBar/PagingBar';

const List = (ControlPanel, Modal, Item) =>{
    return class extends Component{
        constructor(props) {
            super(props);
            this.service = this.props.service;
            this.model = this.props.model;
        }

        state = {
            items: [],
            total: 0,
            pageSize: this.props.pageSize,
            currentPage: 1,
            editModal:{
                show: false,
                item: this.props.model
            },
            findItem: this.props.model
        }

        componentDidMount(){
            this.handleChange();
        }

        handleChange(){
            this.service
                .get((this.state.currentPage - 1) * this.state.pageSize,
                 this.state.pageSize ,
                  (items, total) => this.setState({ items: items, total: total }));
        }

        handleChangePage(curr) {
            if(!this.model.equals(this.state.findItem))
                return this.service.find(this.state.findItem, (curr - 1) * this.state.pageSize, this.state.pageSize,
                    (items, total) => 
                        this.setState({ items: items, total: total, currentPage: curr }));
            this.service
                .get((curr - 1) * this.state.pageSize,
                    this.state.pageSize,
                    (items, total) => this.setState({ items: items, total: total, currentPage: curr }));
        }

        handleChangeFind(item){
            if(this.model.equals(item))
                return this.service.get(0, this.state.pageSize,
                    (items, total) => this.setState({ items: items, total: total, currentPage: 1, findItem: this.model }));
            this.service.find(item, 0, this.state.pageSize,
                (items, total) => 
                    this.setState({ items: items, total: total, findItem: item, currentPage: 1 }));
        }
    
        showEditModal(item) {
            this.setState({ editModal: {show: true, item: item} });
        }
    
        hideEditModal() {
            this.setState({ editModal: {show: false, item: this.model} });
        }

        render() {
            return (
                <div className="list">
                    <div className="list-header">
                        <ControlPanel onAdd={() => this.showEditModal(this.model)}
                            model={this.model}
                            onChange={(item) => this.handleChangeFind(item)}/>
                        <Modal title={`Edit ${Item.name}`}
                            show={this.state.editModal.show}
                            onClose={() => this.hideEditModal()}
                            data={this.state.editModal.item}
                            onSave={(item) => {
                                if (item.id === 0)
                                    return this.service.insert(item, (id) => {
                                        this.handleChange();
                                        this.hideEditModal();
                                    });
                                this.service.update(item, (id) => {
                                    this.handleChange();
                                    this.hideEditModal();
                                });
                            }} />
                        
                    </div>
                    <div className="list-body">
                        {this.state.items.map(item => {
                            return (
                                <Item item={item}
                                    onEdit={(id) => this.service.getById(id, (item) => {
                                        this.showEditModal(item);
                                    })}
                                    onDelete={(item) => this.service.del(item, (id) => {
                                        this.hideEditModal();
                                        this.handleChange();
                                    })}
                                    key={item.id} />
                            )
                        })}
                    </div>
                    <div className="list-footer">
                        <PagingBar size={this.state.pageSize} 
                        total={this.state.total}
                        current={this.state.currentPage}
                            onChange={(curr) => {this.handleChangePage(curr)}}/>
                    </div>
                </div>
            )
        }
    }
}

export default List;