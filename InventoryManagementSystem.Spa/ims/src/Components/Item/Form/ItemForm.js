import React from 'react';
import { Formik } from 'formik';
import * as Yup from 'yup';
import Helpers from '../../../Helpers';
import './ItemForm.css';

const ItemForm = (props) => {
    return (
        <Formik
            enableReinitialize={true}
            initialValues={{ ...props.data}}
            validationSchema={Yup.object({
                code: Yup.string()
                    .required('Required')
                    .min(3, 'Minimum 3 characters long')
                    .max(50, 'Maximum 50 characters long'),
                price: Yup.string()
                    .required('Required')
                    .matches(/^[1-9]\d*(\.\d+)?$/, 'Invalid price'),
                description: Yup.string()
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
                        <div className="row-5">
                            <img alt={formik.values.description} id="imageBase64" className="image" src={formik.values.imageBase64} />
                            <label htmlFor="select-image" className="select-image">Select</label>
                            <input id="select-image" type="file" className="select-image-file" accept="image/*"
                                onChange={(e) => {
                                    Helpers.getBase64(e.target.files[0],
                                        (res) => formik.setFieldValue("imageBase64", res))
                                }} />
                        </div>
                        <div className="row">
                            <input id="code" className="form-input full-width" placeholder="Code"
                                {...formik.getFieldProps('code')} />
                            {formik.touched.code && formik.errors.code ? (
                                <span className="err-msg">{formik.errors.code}</span>
                            ) : null}
                        </div>
                        <div className="row">
                            <input id="price" className="form-input full-width" placeholder="Price"
                                {...formik.getFieldProps('price')} />
                            {formik.touched.price && formik.errors.price ? (
                                <span className="err-msg">{formik.errors.price}</span>
                            ) : null}
                        </div>
                        <div className="row">
                            <textarea id="description" className="form-input full-width" placeholder="Description"
                                {...formik.getFieldProps('description')} />
                            {formik.touched.description && formik.errors.description ? (
                                <span className="err-msg">{formik.errors.description}</span>
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
export default ItemForm;