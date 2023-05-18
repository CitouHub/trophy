import React, { useState, useEffect } from 'react';
import Accordion from '@mui/material/Accordion';
import AccordionDetails from '@mui/material/AccordionDetails';
import AccordionSummary from '@mui/material/AccordionSummary';
import Skeleton from '@mui/material/Skeleton';
import * as GameService from '../service/game.service'

const gamesLimit = 5;

export const Games = () => {
    const [loading, setLoading] = useState(false);
    const [games, setGames] = useState([]);
    const [showAll, setShowAll] = useState(false);
    const [expandedGame, setExpandedGame] = useState(0);

    const handleExpandChange = (gameId) => {
        if (expandedGame === gameId) {
            setExpandedGame(0);
        } else {
            setExpandedGame(gameId);
        }
    };

    useEffect(() => {
        setLoading(true);
        GameService.getGames().then((result) => {
            setGames(result);
            setLoading(false);
        })
    }, [])

    return (
        <React.Fragment>
            <div className='center flex-column pb-4'>
                <h3>Games</h3>
                {!loading && <React.Fragment>
                    {[...games].splice(0, (showAll ? 100 : gamesLimit)).map(_ => (
                        <Accordion key={_.id}
                            className='fade-in'
                            expanded={expandedGame === _.id}
                            onChange={() => handleExpandChange(_.id)} >
                            <AccordionSummary
                                aria-controls="panel1bh-content"
                                id="panel1bh-header"
                            >
                                <div className='statistics-item'>
                                    <span>{_.playerResults[0].playerName} - {_.playerResults[1].playerName}</span>
                                    <span>{_.playerResults[0].score} - {_.playerResults[1].score}</span>
                                </div>
                            </AccordionSummary>
                            <AccordionDetails>
                                <div className='statistics-item'>
                                    <span>{_.matchDate.split('T')[0]}</span>
                                    <span>{_.location}</span>
                                </div>
                            </AccordionDetails>
                        </Accordion>
                    ))}

                    {games.length > gamesLimit && <p className='more-toggle pt-3' onClick={() => setShowAll(!showAll)}>
                        {showAll ? 'Show less' : 'Show more'}
                    </p>}
                </React.Fragment>}
            </div>
            {loading && <div>
                <Skeleton animation="wave" variant="text" sx={{ fontSize: '2.5rem' }} />
                <Skeleton animation="wave" variant="text" sx={{ fontSize: '2.5rem' }} />
                <Skeleton animation="wave" variant="text" sx={{ fontSize: '2.5rem' }} />
            </div>}
        </React.Fragment>
    );
}
