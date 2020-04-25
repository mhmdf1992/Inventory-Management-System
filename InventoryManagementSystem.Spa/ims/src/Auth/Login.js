import React from 'react';
import { Formik } from 'formik';
import * as Yup from 'yup';
import { NavLink } from 'react-router-dom';
import UserModel from '../Models/UserModel';

import './Login.css';

const Login = ({onSubmit, service }) => {
    return (
        <Formik
            initialValues={{ ...UserModel.credentials }}
            validationSchema={Yup.object({
                email: Yup.string()
                    .required('Required')
                    .email('Email is invalid'),
                password: Yup.string()
                    .required('Required')
            })}
            onSubmit={(values, { setErrors }) => {
                service.login(values,
                    token => onSubmit(token),
                    err => setErrors({ password: err }));
            }}
        >
            {formik => (
                <div className="login-container">
                    <div className="login">
                        <div className="title"></div>
                        <form onSubmit={formik.handleSubmit}>
                            <input id="email" placeholder="Email"
                                {...formik.getFieldProps('email')} />
                            {formik.touched.email && formik.errors.email ? (
                                <span className="err-msg">{formik.errors.email}</span>
                            ) : null}
                            <input id="password" type="password" placeholder="Password"
                                {...formik.getFieldProps('password')} />
                            {formik.touched.password && formik.errors.password ? (
                                <span className="err-msg">{formik.errors.password}</span>
                            ) : null}
                            <button type="submit" className="submit">Log In</button>
                        </form>
                        <div className="separator">
                            <span></span>
                            <span>OR</span>
                            <span></span>
                        </div>
                        <div className="facebook-container">
                            <div></div>
                            <span>Log in with Facebook</span>
                        </div>
                        <div className="forgetpass">Forget password?</div>
                    </div>
                    <div className="signup">
                        Don't have an account?
                        <NavLink to='/Register'>
                            <span>Register</span>
                        </NavLink>
                    </div>
                </div>
            )}
        </Formik>
    );
};
export default Login;