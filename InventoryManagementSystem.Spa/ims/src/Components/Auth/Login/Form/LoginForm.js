import React from 'react';
import { Formik } from 'formik';
import * as Yup from 'yup';

const LoginForm = ({value, onSubmit, service}) => {
    return (
        <Formik
            initialValues={{ ...value }}
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
                    err => setErrors({password: err}));
            }}
        >
            {formik => (
                <div className="form">
                    <form onSubmit={formik.handleSubmit}>
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
                        <div className="footer">
                            <button type="submit" className="submit">Login</button>
                        </div>

                    </form>
                </div>
            )}
        </Formik>
    );
};
export default LoginForm;