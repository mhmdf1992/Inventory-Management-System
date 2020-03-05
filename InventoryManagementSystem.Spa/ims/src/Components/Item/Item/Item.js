import React from 'react';

import './Item.css';
const Item = (props) => {
    return(
        <div className="item"
            onClick={(e) => {props.onEdit(props.item.id)}}>
                <h4>{props.item.code}</h4>
                <span>${props.item.price}</span>
                <img className="item-image" src={props.item.imageBase64}></img>
                <span>{props.item.description}</span>
                <span 
                    onClick={(e) => {
                        if (window.confirm('are you sure?'))
                            props.onDelete(props.item)}}>x</span>
        </div>
    )
}

export default Item;