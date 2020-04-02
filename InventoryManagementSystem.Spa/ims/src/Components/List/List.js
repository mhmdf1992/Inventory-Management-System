import React from 'react';

import './List.css';
const List = (Component) => ({value, onEdit, onDelete}) => {
    return(
        <div className="list">
            {
                value.map(i => 
                    <Component 
                        key={i.id}
                        value={i}
                        onEdit={onEdit}
                        onDelete={onDelete} />)
            }
        </div>
    )
}

export default List;