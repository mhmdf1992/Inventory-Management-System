import React from "react";

import './PagingBar.css';

const PagingBar = (props) =>{
    return(
        <div className="paging-bar">
            <button 
                className={`nav-button ${props.current - 1 === 0 ? "disabled" : ""}`} 
                disabled={props.current - 1 === 0}
                onClick={() => props.onChange(props.current - 1)}>{"<-- Previous"}</button>
            {
                Array.apply(null, Array(Math.ceil(props.total / props.size)))
                    .map((p, i) => {
                        return <button 
                        className={`nav-button${props.current === i + 1 ? " active" : ""}`} 
                        key={i + 1}
                        onClick={() => props.onChange(i + 1)}>{i + 1}</button>
                })
            }
            <button 
                className={`nav-button ${props.current + 1 > Math.ceil(props.total / props.size) ? "disabled" : ""}`}
                disabled={props.current + 1 > Math.ceil(props.total / props.size)}
                onClick={() => props.onChange(props.current + 1)}>{"Next -->"}</button>
        </div>
    )
}

export default PagingBar;