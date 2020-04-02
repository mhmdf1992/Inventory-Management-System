import React from 'react';

import './ControlPanel.css';

const ControlPanel = ({onFind, onAdd}) => {
    return(
        <div className="control-panel">
            <input className="search" type="text" placeholder="find"
             onChange={(e) => onFind(e.target.value)} />
            <button className="new" onClick={()=> onAdd()}>+ Add</button>
        </div>
    )
}

export default ControlPanel;