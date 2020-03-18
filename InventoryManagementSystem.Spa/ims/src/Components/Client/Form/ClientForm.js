import React, {Component} from 'react';

import './ClientForm.css';

class ClientForm extends Component{
    constructor(props){
        super(props);
        this.state = this.props.data
    }

    componentDidUpdate(){
        const client = this.props.data;
        if(this.state.id !== client.id)
            this.setState(client);
    }

    render(){
        return (
            <div className="client-form">
                <form>
                    <div className="row">
                        <input type="text" className="form-input" placeholder="Name"
                            value={this.state.name}
                            onChange={(e) => this.setState({ name: e.target.value })} />
                    </div>
                    <div className="row">
                        <input type="text" className="form-input" placeholder="Email"
                            value={this.state.email}
                            onChange={(e) => this.setState({ email: e.target.value })} />
                    </div>
                    <div className="row">
                        <input type="text" className="form-input" placeholder="Telephone"
                            value={this.state.telephone}
                            onChange={(e) => this.setState({ telephone: e.target.value })} />
                    </div>
                    <div className="row">
                        <input type="text" className="form-input full-width" placeholder="Location"
                            value={this.state.location}
                            onChange={(e) => this.setState({ location: e.target.value })} />
                    </div>
                    <div className="footer">
                        <button className="save"
                            onClick={(e) => { e.preventDefault(); this.props.onAction(this.state)}}>save</button>
                    </div>
                </form>
            </div>
        )
    }
}

export default ClientForm;