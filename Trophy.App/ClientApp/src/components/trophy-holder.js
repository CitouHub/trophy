import React, { useState, useEffect } from 'react';
import Skeleton from '@mui/material/Skeleton';
import * as GameService from '../service/game.service'

import trophy from '../assets/trophy.png';

export const TrophyHolder = () => {
    const [player, setPlayer] = useState('');

    useEffect(() => {
        GameService.getTrophyHolder().then((result) => {
            setPlayer(result);
        })
    }, [])

    return (
        <div className='center flex-column'>
            <img src={trophy} alt="Trophy" />
            {player !== '' && <h1>{player}</h1>}
            {player === '' && <Skeleton animation="wave" variant="rounded" width="80%" height={48} className="my-1 mx-auto" />}
        </div>
    );
}
