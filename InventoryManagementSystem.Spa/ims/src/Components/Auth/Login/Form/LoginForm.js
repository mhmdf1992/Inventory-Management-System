import React, {Component} from 'react';

import './LoginForm.css';

class LoginForm extends Component{
    constructor(props){
        super(props);
        this.state = this.props.data;
    }

    render(){
        return (
            <div className="login-form">
                <form>
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
                        <label className="err-msg">{this.props.errMsg}</label>
                    </div>
                    <div className="footer">
                        <button className="login"
                            onClick={e => {e.preventDefault(); this.props.onAction(this.state)}}>Login</button>
                    </div>
                </form>
            </div>
        )
    }
}

export default LoginForm;