import React from 'react';
import { Formik } from 'formik';
import * as Yup from 'yup';

const RegisterForm = ({value, onSubmit, service}) => {
    return (
        <Formik
            initialValues={{ ...value, confirm: '' }}
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
                <div className="form">
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
                            <button type="submit" className="submit">Register</button>
                        </div>
                    </form>
                </div>
            )}
        </Formik>
    );
};
export default RegisterForm;