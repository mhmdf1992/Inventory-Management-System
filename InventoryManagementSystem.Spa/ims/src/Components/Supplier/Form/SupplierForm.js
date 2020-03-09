import React, {Component} from 'react';

import './SupplierForm.css';

class SupplierForm extends Component{
    constructor(props){
        super(props);
        this.state = this.props.data
    }

    componentDidUpdate(){
        const supplier = this.props.data;
        if(this.state.id !== supplier.id)
            this.setState(supplier);
    }

    render(){
        return (
            <div className="supplier-form">
                <div className="row">
                    <input type="text" className="form-input" placeholder="Name"
                        value={this.state.name} 
                        onChange={(e) => this.setState({name: e.target.value})} />
                </div>
                <div className="row">
                    <input type="text" className="form-input" placeholder="Email"
                        value={this.state.email} 
                        onChange={(e) => this.setState({email: e.target.value})} />
                </div>
                <div className="row">
                    <input type="text" className="form-input" placeholder="Telephone"
                        value={this.state.telephone} 
                        onChange={(e) => this.setState({telephone: e.target.value})} />
                </div>
                <div className="row">
                    <input type="text" className="form-input full-width" placeholder="Location" 
                        value={this.state.location} 
                        onChange={(e) => this.setState({location: e.target.value})} />
                </div>
                <div className="footer">
                    <button className="save" 
                        onClick={() => this.props.onSave(this.state)}>save</button>
                </div>
            </div>
        )
    }
}

export default SupplierForm;