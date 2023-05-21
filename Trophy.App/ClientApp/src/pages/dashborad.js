import React, { useState, useEffect } from 'react';
import { TrophyHolder } from '../components/trophy-holder';
import { Ranking } from '../components/ranking';
import { Games } from '../components/games';
import * as PlayerService from '../service/player.service';

export const Dashboard = () => {
    const [loading, setLoading] = useState(false);
    const [players, setPlayers] = useState([]);

    useEffect(() => {
        setLoading(true);
        PlayerService.getPlayers().then((result) => {
            setPlayers(result);
            setLoading(false);
        })
    }, [])

    if (!loading) {
        return (
            <div className='pt-4'>
                <TrophyHolder />
                <Ranking playersCount={players.length} />
                <Games />
            </div>
        );
    } else {
        return null
    }
}
