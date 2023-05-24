import React from 'react';
import CircularProgress from '@mui/material/CircularProgress';

import trophy from '../assets/trophy.png';

export const TrophyLoader = ({ center }) => {

    return (
        <div className={'center flex-column ' + (center ? 'h-100' : 'pt-4')}>
            <img src={trophy} alt="Trophy" />
            <CircularProgress size={80} className='my-4 mx-auto' />
        </div>
    );
}
