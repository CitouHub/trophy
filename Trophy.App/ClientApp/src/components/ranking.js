import React, { useState, useEffect } from 'react';
import ToggleButton from '@mui/material/ToggleButton';
import ToggleButtonGroup from '@mui/material/ToggleButtonGroup';

import * as RankingService from '../service/ranking.service'

const rankingLimit = 3;

export const Ranking = () => {
    const [loading, setLoading] = useState(false);
    const [selectedRanking, setSelectedRanking] = useState('by point count');
    const [rankings, setRankings] = useState([]);
    const [showAll, setShowAll] = useState(false);

    const selectRanking = (event, ranking) => {
        setSelectedRanking(ranking);
    };

    useEffect(() => {
        setLoading(true);
        switch (selectedRanking) {
            case 'by point count':
                RankingService.getByPointCount().then((result) => {
                    setRankings(result);
                    setLoading(false);
                });
                break;
            case 'by trophy time':
                RankingService.getByTrophyTime().then((result) => {
                    setRankings(result);
                    setLoading(false);
                });
                break;
            case 'by win count':
                RankingService.getByWinCount().then((result) => {
                    setRankings(result);
                    setLoading(false);
                });
                break;
            case 'by win rate':
                RankingService.getByWinRate().then((result) => {
                    setRankings(result);
                    setLoading(false);
                });
                break;
            case 'by win size':
                RankingService.getByWinSize().then((result) => {
                    setRankings(result);
                    setLoading(false);
                });
                break;
            case 'by win streak':
                RankingService.getByWinStreak().then((result) => {
                    setRankings(result);
                    setLoading(false);
                });
                break;
        }
    }, [selectedRanking]);
    
    return (
        <div className='center flex-column'>
            <h3>Ranking</h3>
            <div className='center flex-row'>
                <ToggleButtonGroup
                    color="primary"
                    value={selectedRanking}
                    exclusive
                    disabled={loading}
                    onChange={selectRanking}
                    aria-label="Platform"
                >
                    <ToggleButton value="by win count">01</ToggleButton>
                    <ToggleButton value="by win rate">02</ToggleButton>
                    <ToggleButton value="by win size">03</ToggleButton>
                    <ToggleButton value="by win streak">04</ToggleButton>
                    <ToggleButton value="by point count">05</ToggleButton>
                    <ToggleButton value="by trophy time">06</ToggleButton>
                </ToggleButtonGroup>
            </div>
            <h5>{selectedRanking}</h5>
            {!loading && <div className='center flex-column'>
                {[...rankings].splice(0, (showAll ? 100 : 3)).map(_ => (
                    <div key={_.player} className='ranking'>
                        <p>{_.player}</p>
                        <p>{_.value} {_.unit}</p>
                    </div>
                ))}
                {rankings.length > rankingLimit && <p className='losers-toggle' onClick={() => setShowAll(!showAll)}>{showAll ? 'Hide losers' : 'Show losers'}</p>}
            </div>}
        </div>
    );
}
