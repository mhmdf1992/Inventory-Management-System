import React from "react";

import './PagingBar.css';

const PagingBar = ({size, total, current, onChange}) =>{
    return(
        <div className="paging-bar">
            <button 
                className={`nav-button ${current - 1 === 0 ? "disabled" : ""}`} 
                disabled={current - 1 === 0}
                onClick={() => onChange(current - 1)}>{"<"}</button>
            {
                Array.apply(null, Array(Math.ceil(total / size)))
                    .map((p, i) => <button 
                        className={`nav-button ${current === i + 1 ? "active" : ""} ${i > 9 ? "hidden" : ""}`} 
                        key={i + 1}
                        onClick={() => onChange(i + 1)}>{i + 1}</button>
                )
            }
            <button 
                className={`nav-button ${current + 1 > Math.ceil(total / size) ? "disabled" : ""}`}
                disabled={current + 1 > Math.ceil(total / size)}
                onClick={() => onChange(current + 1)}>{">"}</button>
        </div>
    )
}

export default PagingBar;