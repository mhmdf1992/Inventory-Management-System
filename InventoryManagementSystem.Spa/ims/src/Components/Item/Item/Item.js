import React from 'react';

import './Item.css';
const Item = ({value, onEdit, onDelete}) => {
    return(
        <div className="item" onClick={e => onEdit(value.id)}>
                <h4>{value.code}</h4>
                <span>${value.price}</span>
                <img alt={value.description} className="item-image" src={value.imageBase64}></img>
                <span>{value.description}</span>
                <span onClick={e => window.confirm('are you sure?') ? onDelete(value) : ''}>x</span>
        </div>
    )
}

export default Item;