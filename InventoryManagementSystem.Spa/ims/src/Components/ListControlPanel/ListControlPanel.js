import React from 'react';

import './ListControlPanel.css';

const ListControlPanel = (props) => {
    return(
        <div className="list-control-panel">
            <input className="search" type="text" placeholder="find"
             onChange={(e) => props.onChange(
                 props.model.assignText(e.target.value)
             )} />
            <button className="new" onClick={()=> props.onAdd()}>+ Add</button>
        </div>
    )
}

export default ListControlPanel;