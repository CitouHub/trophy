import React, { useState } from 'react';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import Button from '@mui/material/Button';
import LoadingButton from '@mui/lab/LoadingButton';
import TextField from '@mui/material/TextField';
import * as PlayerService from '../service/player.service'

export const AddPlayerDialog = ({ open, closeDialog }) => {
    const [name, setName] = useState('');
    const [saving, setSaving] = useState(false);

    const addPlayer = () => {
        setSaving(true);
        PlayerService.addPlayer(name).then(() => {
            setSaving(false);
            closeDialog();
        });
    }

    const isFormValid = () => {
        return name !== '';
    }

    return (
        <Dialog open={open} onClose={() => closeDialog()}>
            <DialogTitle>App player</DialogTitle>
            <DialogContent>
                <TextField
                    autoFocus
                    margin="dense"
                    id="name"
                    label="Name"
                    type="text"
                    fullWidth
                    variant="outlined"
                    onChange={e => setName(e.target.value)}
                />
            </DialogContent>
            <DialogActions>
                <div className="dialog-action">
                    <Button
                        variant="contained"
                        onClick={() => closeDialog()}
                    >
                        Close
                    </Button>
                    <LoadingButton
                        loading={saving}
                        disabled={!isFormValid()}
                        loadingPosition="start"
                        variant="contained"
                        onClick={addPlayer}
                    >
                        Add player
                    </LoadingButton>
                </div>
            </DialogActions>
        </Dialog>
    );
}
