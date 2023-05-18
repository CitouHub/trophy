import React from 'react';
import { TrophyHolder } from '../components/trophy-holder'
import { Ranking } from '../components/ranking'
import { Games } from '../components/games'

export const Dashboard = () => {
    
    return (
        <React.Fragment>
            <TrophyHolder />
            <Ranking />
            <Games />
        </React.Fragment>
    );
}
