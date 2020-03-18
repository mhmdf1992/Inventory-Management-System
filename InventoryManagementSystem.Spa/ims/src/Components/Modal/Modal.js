import React from 'react';

import './Modal.css';
const Modal = (Form) => (props) => {
    return(
        <div className={`modal ${props.show ? "" : "hidden"}`}>
            <div className="modal-content">
                <div className="content-header">
                    <h4 className="modal-title">{props.title}</h4>
                    <span className="close" onClick={() => props.onClose()}>&times;</span>
                </div>
                <div className="content-body">
                    <Form data={props.data} onAction={props.onAction} errMsg={props.errMsg} />
                </div>
            </div>
        </div>
    )
}

export default Modal;