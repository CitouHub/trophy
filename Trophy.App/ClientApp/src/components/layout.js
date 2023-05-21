import React from 'react';

export const Layout = (props) => {
    return (
        <div className='layout h-100'>
            {props.children}
        </div>
    );
}
