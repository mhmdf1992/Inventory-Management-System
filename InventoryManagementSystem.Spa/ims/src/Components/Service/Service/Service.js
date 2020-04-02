import React from 'react';

import './Service.css';
const Service = ({value, onEdit, onDelete}) => {
    return(
        <div className="service" onClick={e => onEdit(value.id)}>
            <h4>{value.code}</h4>
            <span>${value.price}</span>
            <span>{value.description}</span>
            <span onClick={e => window.confirm('are you sure?') ? onDelete(value) : ''}>x</span>
        </div>
    )
}

export default Service;