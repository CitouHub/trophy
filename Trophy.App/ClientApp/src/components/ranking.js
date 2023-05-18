import React, { useState, useEffect } from 'react';
import ToggleButton from '@mui/material/ToggleButton';
import ToggleButtonGroup from '@mui/material/ToggleButtonGroup';
import Accordion from '@mui/material/Accordion';
import AccordionDetails from '@mui/material/AccordionDetails';
import AccordionSummary from '@mui/material/AccordionSummary';
import Skeleton from '@mui/material/Skeleton';

import * as RankingService from '../service/ranking.service'

const rankingLimit = 3;
const minSwipeDistance = 50

export const Ranking = () => {
    const [loading, setLoading] = useState(false);
    const [selectedRankingId, setSelectedRankingId] = useState("0");
    const [selectedRanking, setSelectedRanking] = useState([]);
    const [transitionEffect, setTransitionEffect] = useState('fade-in');
    const [rankings, setRankings] = useState([]);
    const [showAll, setShowAll] = useState(false);
    const [touchStart, setTouchStart] = useState(null);
    const [touchEnd, setTouchEnd] = useState(null);
    const [animationActive, setAnimationActive] = useState(false);

    const selectRanking = (event, rankingId) => {
        setTransitionEffect('fade-in');
        setSelectedRankingId(rankingId);
    };

    const onTouchStart = (e) => {
        setTouchEnd(null)
        setTouchStart(e.targetTouches[0].clientX)
    }

    const onTouchMove = (e) => setTouchEnd(e.targetTouches[0].clientX)

    const onTouchEnd = () => {
        if (!touchStart || !touchEnd) return
        const distance = touchStart - touchEnd
        const isLeftSwipe = distance > minSwipeDistance
        const isRightSwipe = distance < -minSwipeDistance
        if (isLeftSwipe) {
            let nextRankingId = ((parseInt(selectedRankingId) + 1) % 6)
            setTransitionEffect('slide-right');
            setSelectedRankingId("" + nextRankingId)
        } else if (isRightSwipe) {
            let nextRankingId = (parseInt(selectedRankingId) - 1)
            if (nextRankingId < 0) nextRankingId = 5
            setTransitionEffect('slide-left');
            setSelectedRankingId("" + nextRankingId)
        }
    }

    const enableAnimation = (duration) => {
        setAnimationActive(true)
        setTimeout(() => setAnimationActive(false), duration);

    }

    useEffect(() => {
        setLoading(true);
            RankingService.getRankings().then((result) => {
                setRankings(result);
                setLoading(false);
            });
    }, []);

    useEffect(() => {
        enableAnimation(500);
        setSelectedRanking(rankings[selectedRankingId] ?? []);
    }, [rankings, selectedRankingId]);

    const getRankingTitle = () => {
        switch (selectedRankingId) {
            case "0": return "by win count";
            case "1": return "by win rate";
            case "2": return "by win size";
            case "3": return "by win streak";
            case "4": return "by point count";
            case "5": return "by trophy time";
            default: return "";
        }
    }

    return (
        <div className='center flex-column'>
            <h3>Ranking</h3>
            {!loading && <React.Fragment>
                <div className='center flex-row pb-3'>
                    <ToggleButtonGroup
                        color="primary"
                        value={selectedRankingId}
                        exclusive
                        disabled={loading}
                        onChange={selectRanking}
                        aria-label="Platform"
                    >
                        <ToggleButton value="0">01</ToggleButton>
                        <ToggleButton value="1">02</ToggleButton>
                        <ToggleButton value="2">03</ToggleButton>
                        <ToggleButton value="3">04</ToggleButton>
                        <ToggleButton value="4">05</ToggleButton>
                        <ToggleButton value="5">06</ToggleButton>
                    </ToggleButtonGroup>
                </div>
                <div className='center flex-column pb-4'
                    onTouchStart={onTouchStart}
                    onTouchEnd={onTouchEnd}
                    onTouchMove={onTouchMove}
                >
                    {[...selectedRanking].splice(0, (showAll ? 100 : rankingLimit)).map(_ => (
                        <Accordion key={_.player}
                            className={ animationActive ? transitionEffect : '' }
                            sx={{ pointerEvents: 'none' }}
                            expanded={ false }>
                            <AccordionSummary>
                                <div className='statistics-item'>
                                    <span>{_.player}</span>
                                    <span>{_.value} {_.unit}</span>
                                </div>
                            </AccordionSummary>
                            <AccordionDetails>
                                <div className='statistics-item'>
                                    <span>NOT USED</span>
                                </div>
                            </AccordionDetails>
                        </Accordion>
                    ))}
                    {selectedRanking.length > rankingLimit && <p className='more-toggle pt-3' onClick={() => {
                        enableAnimation(1000);
                        setShowAll(!showAll);
                        setTransitionEffect('fade-in');
                    }}
                    >
                        {showAll ? 'Show less' : 'Show more'}
                    </p>}
                </div>
            </React.Fragment>}
            {loading && <div>
                <Skeleton animation="wave" variant="text" sx={{ fontSize: '2.7rem' }} />
                <Skeleton animation="wave" variant="text" sx={{ fontSize: '2.7rem' }} />
                <Skeleton animation="wave" variant="text" sx={{ fontSize: '2.7rem' }} />
                <Skeleton animation="wave" variant="text" sx={{ fontSize: '2.7rem' }} />
            </div>}
        </div>
    );
}
