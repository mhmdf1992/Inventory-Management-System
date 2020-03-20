import React from 'react';
import { Formik } from 'formik';
import * as Yup from 'yup';

const SupplierForm = (props) => {
    return (
        <Formik
            enableReinitialize={true}
            initialValues={{ ...props.data}}
            validationSchema={Yup.object({
                name: Yup.string()
                    .required('Required')
                    .min(3, 'Minimum 3 characters long')
                    .max(50, 'Maximum 50 characters long'),
                email: Yup.string()
                    .email('Invalid email address')
                    .required('Required'),
                telephone: Yup.string()
                    .required('Required')
                    .matches(/^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/, "Invalid telephone number"),
                location: Yup.string()
                    .required('Required')
                    .min(3, 'Minimum 3 characters long')
                    .max(250, 'Maximum 250 characters long')
            })}
            onSubmit={(values, { setSubmitting }) => {
                props.onAction(values);
            }}
        >
            {formik => (
                <div className="form">
                    <form onSubmit={formik.handleSubmit}>
                        <div className="row">
                            <input id="name" className="form-input full-width" placeholder="Name"
                                {...formik.getFieldProps('name')} />
                            {formik.touched.name && formik.errors.name ? (
                                <span className="err-msg">{formik.errors.name}</span>
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
                            <input id="telephone" className="form-input full-width" placeholder="Telephone"
                                {...formik.getFieldProps('telephone')} />
                            {formik.touched.telephone && formik.errors.telephone ? (
                                <span className="err-msg">{formik.errors.telephone}</span>
                            ) : null}
                        </div>
                        <div className="row">
                            <input id="location" className="form-input full-width" placeholder="Location"
                                {...formik.getFieldProps('location')} />
                            {formik.touched.location && formik.errors.location ? (
                                <span className="err-msg">{formik.errors.location}</span>
                            ) : null}
                        </div>
                        <div className="footer">
                            <button type="submit" className="submit">Save</button>
                        </div>
                    </form>
                </div>
            )}
        </Formik>
    );
};
export default SupplierForm;