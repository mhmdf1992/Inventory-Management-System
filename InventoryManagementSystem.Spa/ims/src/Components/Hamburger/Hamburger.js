import React from 'react';
import './Hamburger.css';

const Hamburger = ({state}) =>{
    return (
        <div className={`hamburger ${state}`}>
            <span></span>
            <span></span>
            <span></span>
        </div>
    )
}

export default Hamburger;