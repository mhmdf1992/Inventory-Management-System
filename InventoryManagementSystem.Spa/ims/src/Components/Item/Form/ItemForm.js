import React from 'react';
import { Formik } from 'formik';
import * as Yup from 'yup';
import Helpers from '../../../Helpers';
import './ItemForm.css';

const ItemForm = ({value, onSubmit, service}) => {
    return (
        <Formik
            enableReinitialize={true}
            initialValues={{...value}}
            validationSchema={Yup.object({
                code: Yup.string()
                    .required('Required')
                    .min(3, 'Code is too short')
                    .max(25, 'Code is too long'),
                price: Yup.string()
                    .required('Required')
                    .matches(/^[1-9]\d*(\.\d+)?$/, 'Price is invalid'),
                description: Yup.string()
                    .required('Required')
                    .min(3, 'Description is too short')
                    .max(250, 'Description is too long'),
                imageBase64: Yup.string()
                    .required('Required')
                    .matches('^data:image/([a-zA-Z]*);base64,(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$', 'Image is invalid')
            })}
            onSubmit={(values, { setErrors }) => {
                value.id === 0 
                    ? service.insert(values, res => onSubmit(), err => setErrors(err.errors))
                    : service.update(values, res => onSubmit(), err => setErrors(err.errors))
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
                            {formik.touched.imageBase64 && formik.errors.imageBase64 ? (
                                <span className="err-msg">{formik.errors.imageBase64}</span>
                            ) : null}
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