import React from 'react';

import './Client.css';
const Client = ({value, onEdit, onDelete}) => {
    return(
        <div className="client" onClick={e => onEdit(value.id)}>
            <h4 className="title">{value.name}</h4>
            <span className="telephone">{value.telephone}</span>
            <span className="email">{value.email}</span>
            <span className="location">{value.location}</span>
            <span className="delete" onClick={e => window.confirm('are you sure?') ? onDelete(value) : '' }>x</span>
        </div>
    )
}

export default Client;