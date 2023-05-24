import React, { useState, useEffect } from 'react';
import { useNavigate } from "react-router-dom";
import TextField from '@mui/material/TextField';
import LoadingButton from '@mui/lab/LoadingButton';
import LibraryAddIcon from '@mui/icons-material/LibraryAdd';
import { PlayerResult } from '../components/player-result.input';
import { TrophyLoader } from '../components/trophy-loader';
import { isEnter } from '../util/keyboard.aid';
import * as PlayerService from '../service/player.service';
import * as GameService from '../service/game.service';

import trophy from '../assets/trophy.png';

export const Register = () => {
    const [loading, setLoading] = useState(true);
    const [saving, setSaving] = useState(false);
    const [players, setPlayers] = useState([]);
    const [player1Result, setPlayer1Result] = useState({ player: { id: 0 }, score: 0 });
    const [player2Result, setPlayer2Result] = useState({ player: { id: 0 }, score: 0 });
    const [game, setGame] = useState({ location: '', mathcDate: new Date() });

    const navigate = useNavigate();

    useEffect(() => {
        setLoading(true);
        PlayerService.getPlayers().then((result) => {
            setPlayers(result);
            setLoading(false);
        });
    }, []);

    const checkEnter = (e) => {
        if (isEnter(e) && isFormValid()) {
            submitGame();
        }
    }

    const isFormValid = () => {
        return game.location !== '' &&
            player1Result.player.id > 0 &&
            player2Result.player.id > 0 &&
            player1Result.score >= 0 &&
            player2Result.score >= 0 &&
            player1Result.score !== player2Result.score;
    }

    const submitGame = () => {
        setSaving(true);
        GameService.addGame({
            ...game,
            playerResults: [player1Result, player2Result]
        }).then(() => {
            setSaving(false);
            navigate('/');
        });
    }

    const updatePlayerList = (player) => {
        let newPlayers = [...players];
        newPlayers.push(player);
        setPlayers(newPlayers);
    }

    if (!loading) {
        return (
            <div className='center flex-column h-100'>
                <img src={trophy} alt="Trophy" className='mb-4' />
                <PlayerResult
                    title={'Player 1'}
                    players={players}
                    playerResult={player1Result}
                    setPlayerResult={result => setPlayer1Result(result)}
                    updatePlayerList={updatePlayerList}
                    onKeyDown={checkEnter}
                />
                <h1>vs.</h1>
                <PlayerResult
                    title={'Player 2'}
                    players={players}
                    playerResult={player2Result}
                    setPlayerResult={result => setPlayer2Result(result)}
                    updatePlayerList={updatePlayerList}
                    onKeyDown={checkEnter}
                />
                <TextField
                    sx={{ marginTop: '1rem', width: '100%' }}
                    label="At location"
                    id="game-location"
                    size="small"
                    type="text"
                    onChange={e => setGame({ ...game, location: e.target.value })}
                    onKeyDown={checkEnter}
                />
                <LoadingButton
                    sx={{ marginTop: '1rem', width: '100%' }}
                    loading={saving}
                    disabled={!isFormValid()}
                    loadingPosition="start"
                    startIcon={<LibraryAddIcon />}
                    variant="contained"
                    onClick={submitGame}
                    onKeyDown={checkEnter}
                >
                    Submit game
                </LoadingButton>
            </div>
        );
    } else {
        return (
            <TrophyLoader center={true} />
        )
    }
}
