import React, { useState, useEffect } from 'react';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import * as PlayerService from '../service/player.service';
import { isEnter } from '../util/keyboard.aid';

export const AddPlayerDialog = ({ players, open, closeDialog, handleNewPlayerAdded }) => {
    const [name, setName] = useState('');
    const [saving, setSaving] = useState(false);

    useEffect(() => {
        setName('');
        setSaving(false);
    }, [open]);

    const checkEnter = (e) => {
        if (isEnter(e) && !isDisabled()) {
            addPlayer();
        }
    }

    const isDisabled = () => {
        return name === '' || saving;
    }

    const addPlayer = () => {
        let player = players.find(_ => _.name.toLowerCase() === name.toLowerCase());
        if (player) {
            handleNewPlayerAdded(player, true);
            closeDialog();
        } else {
            setSaving(true);
            PlayerService.addPlayer(name).then((result) => {
                handleNewPlayerAdded(result, false);
                setSaving(false);
                closeDialog();
            });
        }
    }

    return (
        <Dialog open={open} onClose={closeDialog}>
            <DialogTitle>App player</DialogTitle>
            <DialogContent>
                <TextField
                    autoFocus
                    margin="dense"
                    id="name"
                    label="Name"
                    type="text"
                    size="small"
                    fullWidth
                    variant="outlined"
                    onChange={e => setName(e.target.value)}
                    onKeyDown={checkEnter}
                />
            </DialogContent>
            <DialogActions>
                <div className="dialog-action">
                    <Button variant="contained" onClick={closeDialog}>
                        Close
                    </Button>
                    <Button disabled={isDisabled()} variant="contained" onClick={addPlayer} onKeyDown={checkEnter}>
                        Add
                    </Button>
                </div>
            </DialogActions>
        </Dialog>
    );
}
