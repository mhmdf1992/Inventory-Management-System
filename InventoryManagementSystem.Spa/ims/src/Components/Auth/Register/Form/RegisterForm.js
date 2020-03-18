import React, {Component} from 'react';

import './RegisterForm.css';

class RegisterForm extends Component{
    constructor(props){
        super(props);
        this.state = this.props.data;
        this.state.confirm = '';
    }

    render(){
        return (
            <div className="register-form">
                <form>
                    <div className="row">
                        <input type="text" className="form-input full-width" placeholder="Firstname"
                            value={this.state.firstname}
                            onChange={(e) => this.setState({ firstname: e.target.value })} />
                    </div>
                    <div className="row">
                        <input type="text" className="form-input full-width" placeholder="Lastname"
                            value={this.state.lastname}
                            onChange={(e) => this.setState({ lastname: e.target.value })} />
                    </div>
                    <div className="row">
                        <input type="text" className="form-input full-width" placeholder="Email"
                            value={this.state.email}
                            onChange={(e) => this.setState({ email: e.target.value })} />
                    </div>
                    <div className="row">
                        <input type="password" className="form-input full-width" placeholder="Password"
                            value={this.state.password}
                            onChange={(e) => this.setState({ password: e.target.value })} />
                    </div>
                    <div className="row">
                        <input type="password" className="form-input full-width" placeholder="Confirm"
                            value={this.state.confirm}
                            onChange={(e) => this.setState({ confirm: e.target.value })} />
                    </div>
                    <div className="row">
                        <label className="err-msg">{this.state.password !== this.state.confirm ? 'Confirm password does not match' : ''}</label>
                    </div>
                    <div className="footer">
                        <button className="register"
                            onClick={e =>{  e.preventDefault(); this.props.onAction(this.state);}}>Register</button>
                    </div>
                </form>
            </div>
        )
    }
}

export default RegisterForm;