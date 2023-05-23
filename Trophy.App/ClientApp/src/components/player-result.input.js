import React, { useState } from 'react';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import TextField from '@mui/material/TextField';
import { AddPlayerDialog } from '../components/add-player.dialog';

export const PlayerResult = ({ title, players, playerResult, setPlayerResult, updatePlayerList, onKeyDown }) => {
    const [addPlayerDialogOpen, setAddPlayerDialogOpen] = useState(false);

    const handleNewPlayerAdded = (player, existing) => {
        if (!existing) {
            updatePlayerList(player);
        }
        setPlayerResult({ ...playerResult, player: { id: player.id } })
    }

    return (
        <div className='center flex-row'>
            <FormControl sx={{ width: '80%' }} size="small">
                <InputLabel id="player-select-label">{title}</InputLabel>
                <Select
                    labelId="player-select-label"
                    id="player-select"
                    value={playerResult.player.id > 0 ? playerResult.player.id : ''}
                    label={title}
                    onChange={e => {
                        if (e.target.value !== -1) {
                            setPlayerResult({ ...playerResult, player: { id: e.target.value } });
                        } else {
                            setAddPlayerDialogOpen(true);
                        }
                    }}>
                    {players.map(p => (
                        <MenuItem key={p.id} value={p.id}>{p.name}</MenuItem>
                    ))}
                    <MenuItem key="new" value={-1}>+ Add player</MenuItem>
                </Select>
            </FormControl>
            <TextField
                sx={{ width: '20%' }}
                label="Score"
                id="player-score"
                type="tel"
                size="small"
                InputLabelProps={{ shrink: true }}
                inputProps={{ maxLength: 3 }}
                onChange={e => setPlayerResult({ ...playerResult, score: e.target.value })}
                onKeyDown={onKeyDown}
            />
            <AddPlayerDialog
                players={players}
                open={addPlayerDialogOpen}
                closeDialog={() => setAddPlayerDialogOpen(false)}
                handleNewPlayerAdded={handleNewPlayerAdded}
            />
        </div>
    );
}
