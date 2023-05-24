import React, { useState, useEffect } from 'react';
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
            <h1>{player}</h1>
        </div>
    );
}
