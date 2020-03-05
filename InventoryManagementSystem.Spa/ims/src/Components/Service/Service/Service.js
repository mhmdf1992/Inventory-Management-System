import React from 'react';

import './Service.css';
const Service = (props) => {
    return(
        <div className="service"
            onClick={(e) => {props.onEdit(props.item.id)}}>
            <h4>{props.item.code}</h4>
            <span>${props.item.price}</span>
            <span>{props.item.description}</span>
            <span 
                onClick={(e) => {
                     if (window.confirm('are you sure?'))
                          props.onDelete(props.item)}}>x</span>
        </div>
    )
}

export default Service;