import React, {Component} from 'react';

import './ServiceForm.css';

class ServiceForm extends Component{
    constructor(props){
        super(props);
        this.state = this.props.data
    }

    componentDidUpdate(){
        const service = this.props.data;
        if(this.state.id !== service.id)
            this.setState(service);
    }

    render(){
        return (
            <div className="service-form">
                <form>
                    <div className="row">
                        <input type="text" className="form-input" placeholder="Code"
                            value={this.state.code}
                            onChange={(e) => this.setState({ code: e.target.value })} />
                    </div>
                    <div className="row">
                        <input type="text" className="form-input" placeholder="Price"
                            value={this.state.price}
                            onChange={(e) => this.setState({ price: e.target.value })} />
                    </div>
                    <div className="row-2">
                        <textarea type="text" className="form-input full-width" placeholder="Description"
                            value={this.state.description}
                            onChange={(e) => this.setState({ description: e.target.value })} />
                    </div>
                    <div className="footer">
                        <button className="save"
                            onClick={(e) => { e.preventDefault(); this.props.onAction(this.state) }}>save</button>
                    </div>
                </form>
            </div>
        )
    }
}

export default ServiceForm;