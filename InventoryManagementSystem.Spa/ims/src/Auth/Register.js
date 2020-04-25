import React from 'react';
import { Formik } from 'formik';
import * as Yup from 'yup';
import { NavLink } from 'react-router-dom';
import UserModel from '../Models/UserModel';

import './Register.css';

const Register = ({onSubmit, service}) => {
    return (
        <Formik
            initialValues={{ ...UserModel.user, confirm: '' }}
            validationSchema={Yup.object({
                firstname: Yup.string()
                    .required('Required')
                    .min(3, 'Firstname is too short')
                    .max(25, 'Firstname is too long'),
                lastname: Yup.string()
                    .required('Required')
                    .min(3, 'Lastname is too short')
                    .max(25, 'Lastname is too long'),
                email: Yup.string()
                    .email('Email address is invalid')
                    .required('Required'),
                password: Yup.string()
                    .required('Required')
                    .min(6, 'Password is too short')
                    .max(25, 'Password is too long'),
                confirm: Yup.string()
                    .required('Required')
                    .oneOf([Yup.ref("password")], "Password missmatch")
            })}
            onSubmit={(values, { setErrors }) => {
                service.register(values, 
                    token => onSubmit(token),
                    err => setErrors(err.errors));
            }}
        >
            {formik => (
                <div className="register-container">
                    <div className="register">
                        <div className="title"></div>
                        <div className="facebook-container">
                            <div></div>
                            <span>Log in with Facebook</span>
                        </div> 
                        <div className="separator">
                            <span></span>
                            <span>OR</span>
                            <span></span>
                        </div>
                        <form onSubmit={formik.handleSubmit}>
                                <input id="firstname" placeholder="Firstname"
                                    {...formik.getFieldProps('firstname')} />
                                {formik.touched.firstname && formik.errors.firstname ? (
                                    <span className="err-msg">{formik.errors.firstname}</span>
                                ) : null}
                                <input id="lastname" placeholder="Lastname"
                                    {...formik.getFieldProps('lastname')} />
                                {formik.touched.lastname && formik.errors.lastname ? (
                                    <span className="err-msg">{formik.errors.lastname}</span>
                                ) : null}
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
                                <input id="confirm" type="password" placeholder="Confirm Password"
                                    {...formik.getFieldProps('confirm')} />
                                {formik.touched.confirm && formik.errors.confirm ? (
                                    <span className="err-msg">{formik.errors.confirm}</span>
                                ) : null}
                                <button type="submit" className="submit">Register</button>
                        </form>
                    </div>
                    <div className="login">
                        Have an account?
                        <NavLink to='/'>
                            <span>Log in</span>
                        </NavLink>
                    </div>
                </div>
            )}
        </Formik>
    );
};
export default Register;