import React, { useState, useEffect } from 'react';
import IconButton from '@mui/material/IconButton';
import KeyboardArrowLeftIcon from '@mui/icons-material/KeyboardArrowLeft';
import KeyboardArrowRightIcon from '@mui/icons-material/KeyboardArrowRight';
import Accordion from '@mui/material/Accordion';
import AccordionDetails from '@mui/material/AccordionDetails';
import AccordionSummary from '@mui/material/AccordionSummary';
import Skeleton from '@mui/material/Skeleton';
import * as RankingService from '../service/ranking.service';

const rankingLimit = 3;
const minSwipeDistance = 50

export const Ranking = ({ playersCount }) => {
    const [loading, setLoading] = useState(true);
    const [selectedRankingId, setSelectedRankingId] = useState("0");
    const [selectedRanking, setSelectedRanking] = useState([]);
    const [transitionEffect, setTransitionEffect] = useState('fade-in');
    const [rankings, setRankings] = useState([]);
    const [showAll, setShowAll] = useState(false);
    const [touchStart, setTouchStart] = useState(null);
    const [touchEnd, setTouchEnd] = useState(null);
    const [animationActive, setAnimationActive] = useState(false);

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
            nextRanking();
        } else if (isRightSwipe) {
            previousRanking();
        }
    }

    const nextRanking = () => {
        let nextRankingId = ((parseInt(selectedRankingId) + 1) % 6);
        setTransitionEffect('slide-right');
        setSelectedRankingId("" + nextRankingId);
    }

    const previousRanking = () => {
        let nextRankingId = (parseInt(selectedRankingId) - 1);
        if (nextRankingId < 0) nextRankingId = 5
        setTransitionEffect('slide-left');
        setSelectedRankingId("" + nextRankingId);
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
            case "2": return "by win streak";
            case "3": return "by win size";
            case "4": return "by trophy time";
            case "5": return "by point count";
            default: return "";
        }
    }

    return (
        <div className='center flex-column'>
            <div className='space-between'>
                <IconButton aria-label="Left" onClick={previousRanking} >
                    <KeyboardArrowLeftIcon color="primary" className="ranking-arrow" sx={{ fontSize: '2rem' }} />
                </IconButton>
                <div>
                    <h3 className="mb-0">Ranking</h3>
                    <p className="mb-0"><small>({getRankingTitle()})</small></p>
                </div>
                <IconButton aria-label="Right" onClick={nextRanking} >
                    <KeyboardArrowRightIcon color="primary" className="ranking-arrow" sx={{ fontSize: '2rem' }} />
                </IconButton>
            </div>
            {!loading && [...selectedRanking].length > 0 && <React.Fragment>
                <div className={(animationActive ? transitionEffect : '') + ' center flex-column'}
                    onTouchStart={onTouchStart}
                    onTouchEnd={onTouchEnd}
                    onTouchMove={onTouchMove}
                >
                    {[...selectedRanking].splice(0, (showAll ? 100 : rankingLimit)).map(_ => (
                        <Accordion key={_.player}
                            sx={{ pointerEvents: 'none' }}
                            expanded={ false }>
                            <AccordionSummary>
                                <div className='space-between'>
                                    <span>{_.player}</span>
                                    <span>{_.value} {_.unit}</span>
                                </div>
                            </AccordionSummary>
                            <AccordionDetails>
                                <div className='space-between'>
                                    <span>NOT USED</span>
                                </div>
                            </AccordionDetails>
                        </Accordion>
                    ))}
                </div>
                {playersCount > rankingLimit && <span className='more-toggle mt-3  mb-4' onClick={() => {
                    enableAnimation(1000);
                    setShowAll(!showAll);
                    setTransitionEffect('fade-in');
                }}
                >
                    {showAll ? 'Show less' : 'Show more'}
                </span>}
            </React.Fragment>}
            {loading && <div className="mb-4" >
                {[...Array(playersCount > rankingLimit ? rankingLimit : playersCount).keys()].map(_ => (
                    <Skeleton key={_} animation="wave" variant="rounded" width="100%" height={44} className="my-1" />
                ))}

                {playersCount > rankingLimit && <Skeleton animation="wave" variant="rounded" width="40%" height={24} className="mt-3 mx-auto" />}
            </div>}
        </div>
    );
}
