import React, { useState, useEffect } from 'react';
import * as GameService from '../service/game.service'

const gamesLimit = 3;

export const Games = () => {
    const [loading, setLoading] = useState(false);
    const [games, setGames] = useState([]);
    const [showAll, setShowAll] = useState(false);

    useEffect(() => {
        setLoading(true);
        GameService.getGames().then((result) => {
            setGames(result);
            setLoading(false);
        })
    }, [])

    return (
        <React.Fragment>
            {!loading && <div className='center flex-column'>
                <h3>Games</h3>
                {[...games].splice(0, (showAll ? 100 : gamesLimit)).map(_ => (
                    <div key={_.mathcDate} className='ranking'>
                        <p>{_.playerResults[0].playerName} - {_.playerResults[1].playerName}</p>
                        <p>{_.playerResults[0].score} - {_.playerResults[1].score}</p>
                    </div>
                ))}
                {games.length > gamesLimit && <p className='more-toggle' onClick={() => setShowAll(!showAll)}>
                    {showAll ? 'Show less' : 'Show more'}
                </p>}
            </div>}
        </React.Fragment>
    );
}
