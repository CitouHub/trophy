import React, { useState, useEffect } from 'react';
import * as GameService from '../service/game.service'

import trophy from '../assets/trophy.png';

export const TrophyHolder = () => {
    const [loading, setLoading] = useState(true);
    const [player, setPlayer] = useState('');

    useEffect(() => {
        setLoading(true);
        GameService.getTrophyHolder().then((result) => {
            setPlayer(result);
            setLoading(false);
        })
    }, [])

    if (!loading && player !== '') {
        return (
            <div className='center flex-column'>
                <img src={trophy} alt="Trophy" />
                <h1>{player}</h1>
            </div>
        );
    } else {
        return null;
    }
}
