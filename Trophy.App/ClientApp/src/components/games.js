import React, { useState, useEffect } from 'react';
import Accordion from '@mui/material/Accordion';
import AccordionDetails from '@mui/material/AccordionDetails';
import AccordionSummary from '@mui/material/AccordionSummary';
import * as GameService from '../service/game.service';

const gamesLimit = 5;

export const Games = () => {
    const [loading, setLoading] = useState(true);
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
        });
    }, []);

    return (
        <React.Fragment>
            {!loading && games.length > 0 && <div className='center flex-column pb-4'>
                <h3>Games</h3>
                <React.Fragment>
                    {[...games].splice(0, (showAll ? 100 : gamesLimit)).map(_ => (
                        <Accordion key={_.id}
                            className='fade-in'
                            expanded={expandedGame === _.id}
                            onChange={() => handleExpandChange(_.id)} >
                            <AccordionSummary>
                                <div className='statistics-item'>
                                    <span>{_.playerResults[0].player.name} - {_.playerResults[1].player.name}</span>
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
                </React.Fragment>
            </div>}
        </React.Fragment>
    );
}
