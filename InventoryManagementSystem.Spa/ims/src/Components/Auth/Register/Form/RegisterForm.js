import React from 'react';
import { Formik } from 'formik';
import * as Yup from 'yup';

import './RegisterForm.css';

const RegisterForm = (props) => {
    return (
        <Formik
            initialValues={{ ...props.data, confirm: '' }}
            validationSchema={Yup.object({
                firstname: Yup.string()
                    .required('Required')
                    .min(3, 'Minimum 3 characters long')
                    .max(25, 'Maximum 25 characters long'),
                lastname: Yup.string()
                    .required('Required')
                    .min(3, 'Minimum 3 characters long')
                    .max(25, 'Maximum 25 characters long'),
                email: Yup.string()
                    .email('Invalid email address')
                    .required('Required'),
                password: Yup.string()
                    .required('Required')
                    .min(6)
                    .max(25, 'Maximum 25 characters long'),
                confirm: Yup.string()
                    .required('Required')
                    .oneOf([Yup.ref("password")], "Password missmatch")
            })}
            onSubmit={(values, { setSubmitting }) => {
                props.onAction(values);
            }}
        >
            {formik => (
                <div className="register-form">
                    <form onSubmit={formik.handleSubmit}>
                        <div className="row">
                            <input id="firstname" className="form-input full-width" placeholder="Firstname"
                                {...formik.getFieldProps('firstname')} />
                            {formik.touched.firstname && formik.errors.firstname ? (
                                <span className="err-msg">{formik.errors.firstname}</span>
                            ) : null}
                        </div>
                        <div className="row">
                            <input id="lastname" className="form-input full-width" placeholder="Lastname"
                                {...formik.getFieldProps('lastname')} />
                            {formik.touched.lastname && formik.errors.lastname ? (
                                <span className="err-msg">{formik.errors.lastname}</span>
                            ) : null}
                        </div>
                        <div className="row">
                            <input id="email" className="form-input full-width" placeholder="Email"
                                {...formik.getFieldProps('email')} />
                            {formik.touched.email && formik.errors.email ? (
                                <span className="err-msg">{formik.errors.email}</span>
                            ) : null}
                        </div>
                        <div className="row">
                            <input id="password" type="password" className="form-input full-width" placeholder="Password"
                                {...formik.getFieldProps('password')} />
                            {formik.touched.password && formik.errors.password ? (
                                <span className="err-msg">{formik.errors.password}</span>
                            ) : null}
                        </div>
                        <div className="row">
                            <input id="confirm" type="password" className="form-input full-width" placeholder="Confirm Password"
                                {...formik.getFieldProps('confirm')} />
                            {formik.touched.confirm && formik.errors.confirm ? (
                                <span className="err-msg">{formik.errors.confirm}</span>
                            ) : null}
                        </div>
                        <div className="footer">
                            <button type="submit" className="register">Register</button>
                        </div>
                    </form>
                </div>
            )}
        </Formik>
    );
};
export default RegisterForm;