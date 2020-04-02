import React from 'react';

import './Modal.css';
const Modal = (Form) => ({show, title, onClose, value, onSubmit, service}) => {
    return(
        <div className={`modal ${show ? "" : "hidden"}`}>
            <div className="modal-content">
                <div className="content-header">
                    <h4 className="modal-title">{title}</h4>
                    <span className="close" onClick={() => onClose()}>&times;</span>
                </div>
                <div className="content-body">
                    <Form value={value} onSubmit={onSubmit} service={service}/>
                </div>
            </div>
        </div>
    )
}

export default Modal;