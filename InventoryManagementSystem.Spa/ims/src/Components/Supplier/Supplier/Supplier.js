import React from 'react';

import './Supplier.css';
const Supplier = (props) => {
    return(
        <div className="supplier"
            onClick={(e) => {props.onEdit(props.item.id)}}>
            <h4 className="title">{props.item.name}</h4>
            <span className="telephone">{props.item.telephone}</span>
            <span className="email">{props.item.email}</span>
            <span className="location">{props.item.location}</span>
            <span className="delete"
                onClick={(e) => {
                     if (window.confirm('are you sure?'))
                          props.onDelete(props.item)}}>x</span>
        </div>
    )
}

export default Supplier;