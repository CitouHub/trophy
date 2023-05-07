import React from 'react';
import { TrophyHolder } from '../components/trophy-holder'
import { Ranking } from '../components/ranking'

export const Dashboard = () => {
    
    return (
        <React.Fragment>
            <TrophyHolder />
            <Ranking />
        </React.Fragment>
    );
}
