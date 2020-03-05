import React, {Component} from 'react';

import Helpers from '../../../Helpers';

import './ItemForm.css';

class ItemForm extends Component{
    constructor(props){
        super(props);
        this.state = this.props.data
    }

    componentDidUpdate(){
        const item = this.props.data;
        if(this.state.id !== item.id)
            this.setState(item);
    }

    render(){
        return (
            <div className="item-form">
                <div className="row-5">
                    <img className="image" src={this.state.imageBase64} />
                    <label htmlFor="select-image" className="select-image">Select</label>
                    <input id="select-image" type="file" className="select-image-file" accept="image/*"
                        onChange={(e) => {Helpers.getBase64(e.target.files[0],
                            (res) => this.setState({imageBase64: res}))}} />
                </div>
                <div className="row">
                    <input type="text" className="form-input" placeholder="Code"
                        value={this.state.code} 
                        onChange={(e) => this.setState({code: e.target.value})} />
                </div>
                <div className="row">
                    <input type="text" className="form-input" placeholder="Price" 
                        value={this.state.price} 
                        onChange={(e) => this.setState({price: e.target.value})} />
                </div>
                <div className="row-2">
                    <textarea type="text" className="form-input full-width" placeholder="Description"
                        value={this.state.description} 
                        onChange={(e) => this.setState({description: e.target.value})} />
                </div>
                <div className="footer">
                    <button className="save" 
                        onClick={() => this.props.onSave(this.state)}>save</button>
                </div>
            </div>
        )
    }
}

export default ItemForm;