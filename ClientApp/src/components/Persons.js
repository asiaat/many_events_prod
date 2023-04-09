import React, { Component, useState, useEffect } from 'react';
import Box from '@mui/material/Box';
import Collapse from '@mui/material/Collapse';
import IconButton from '@mui/material/IconButton';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Typography from '@mui/material/Typography';
import Paper from '@mui/material/Paper';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import Button from '@mui/material/Button';
import Modal from '@mui/material/Modal';
import TextField from '@mui/material/TextField';
import DeleteForeverOutlinedIcon from '@mui/icons-material/DeleteForeverOutlined';
import CreateOutlinedIcon from '@mui/icons-material/CreateOutlined';

import axios from "axios";

const style = {
    position: 'absolute',
    top: '30%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 600,
    bgcolor: 'background.paper',
    border: '1px  #000',
    boxShadow: 4,
    p: 4,
};


function Row(props: { row: ReturnType<typeof createData> }) {
    const { row } = props;
    const [open, setOpen] = React.useState(false);
    const [openGuest, setOpenGuest] = React.useState(false);

    const [user, setUser] = useState();

    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);
    const handleGuestOpen = () => setOpenGuest(true);
    const handleGuestClose = () => setOpenGuest(false);


    const handleGuestSubmit = async (event) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);

        const newGuest = {
            title: data.get('eventname'),
            place: data.get('eventplace'),
            releaseDate: data.get('eventtime'),

        };
        console.log(newGuest)

        await axios.get("https://localhost:44450/api/mevents/addguest/2/2")
            .then((response) => {
                console.log(response.data);

            })


        handleGuestClose()

    };

    const renderGuestModal = () => {
        return (
            <Modal
                open={openGuest}
                onClose={handleGuestClose}
                aria-labelledby="modal-guest-title"
                aria-describedby="modal-guest-description"
            >
                <Box sx={style}>
                    <Typography id="modal-guest-title" variant="h6" component="h2">
                        Uus Külastaja
                    </Typography>
                    <Typography id="modal-guest-description" sx={{ mt: 2 }}>

                    </Typography>
                    <Box component="form" onSubmit={handleGuestSubmit} noValidate sx={{ mt: 1 }}>
                        Külastaja
                        
                        <TextField id="feename"
                            name="feename"
                            label="makseviis"
                            variant="outlined" />
                        <TextField id="feeremarks"
                            name="feeremarks"
                            label="märkused"
                            variant="outlined" />

                        <Button

                            type="submit"
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}

                        >Salvesta</Button>
                    </Box>
                </Box>
            </Modal>
        );
    }



    return (
        <React.Fragment>
            {renderGuestModal()}
            <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
                <TableCell>
                    <IconButton
                        aria-label="expand row"
                        size="small"
                        onClick={() => setOpen(!open)}
                    >
                        {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
                    </IconButton>
                </TableCell>
                <TableCell component="th" scope="row">
                    {row.firstName}
                </TableCell>
                <TableCell align="right">{row.lastName}</TableCell>
                <TableCell align="right">{row.personalCodeAsString}</TableCell>
                
                


            </TableRow>
            <TableRow>
                <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
                    <Collapse in={open} timeout="auto" unmountOnExit>
                        <Box sx={{ margin: 1 }}>
                            <Typography variant="h6" gutterBottom component="div">
                                Külastajad
              </Typography>
                            <Table size="small" aria-label="purchases">
                                <TableHead>
                                    <TableRow>
                                        <TableCell>Eesnimi</TableCell>
                                        <TableCell>Perekonnanimi</TableCell>
                                        <TableCell align="right">Isikukood</TableCell>

                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {row.eventsList.map((historyRow) => (
                                        <TableRow key={historyRow.firstName}>
                                            <TableCell component="th" scope="row">
                                                {historyRow.firstName}
                                            </TableCell>
                                            <TableCell>{historyRow.lastName}</TableCell>
                                            <TableCell align="right">{historyRow.personalCodeAsString}</TableCell>

                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>
                        </Box>
                    </Collapse>
                </TableCell>
            </TableRow>
        </React.Fragment>
    );
}


export default function Persons() {

    const [rows, setRows] = useState([]);
    const [user, setUser] = useState();

    const [open, setOpen] = React.useState(false);

    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const handleSubmit = async (event) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);

        
      

        const newPerson = {

            firstName: data.get('firstName'),
            lastName: data.get('lastName'),
            personalCodeAsString: data.get('personalCodeAsString'),

        }
        console.log(newPerson)

        await axios.post("https://localhost:44450/api/mpersons/create",
            newPerson)
            .then((response) => {
                console.log(response.data);

            })
        

        handleClose()
    }

    const retrieveEvents = async () => {
        await axios.get("https://localhost:44450/api/mpersons/persons")
            .then((response) => {
                console.log(response.data);
                setRows(response.data);

            })
    }

    useEffect(() => {
        setUser(localStorage.getItem("user"))
        retrieveEvents()

    }, []);

    const renderModal = () => {
        return (
            <Modal
                open={open}
                onClose={handleClose}
                aria-labelledby="modal-modal-title"
                aria-describedby="modal-modal-description"
            >
                <Box sx={style}>
                    <Typography id="modal-modal-title" variant="h6" component="h2">
                        Uus Külastaja
                    </Typography>
                    <Typography id="modal-modal-description" sx={{ mt: 2 }}>

                    </Typography>
                    <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
                        Uue makseviisi lisamine
                        <TextField id="firstName"
                            name="firstName"
                            label="eesnimi"
                            variant="outlined" />
                        <TextField id="lastName"
                            name="lastName"
                            label="perekonnanimi"
                            variant="outlined" />
                        <TextField id="personalCodeAsString"
                            name="personalCodeAsString"
                            label="isikukood"
                            variant="outlined" />

                        <Button

                            type="submit"
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}

                        >Salvesta</Button>
                    </Box>
                </Box>
            </Modal>
        );
    }



    const renderReturn = () => {

        if (user) {

            return (
                <h1>

                    <Button
                        onClick={handleOpen}
                        type="submit"
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}

                    >Lisa uus külastaja</Button>
                </h1>
            );
        }
    }

    return (
        <div>
            <h1>Osavõtjad</h1>
            {renderReturn()}
            {renderModal()}

            <TableContainer component={Paper}>
                <Table aria-label="collapsible table">
                    <TableHead>
                        <TableRow>
                            <TableCell />
                            <TableCell> Isikukood</TableCell>
                            <TableCell align="right">Eesnimi</TableCell>
                            <TableCell align="right">Perekonnanimi</TableCell>
                            <TableCell align="right">Makseviis</TableCell>
                           
                            <TableCell align="right"></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {rows.map((row) => (
                            <Row key={row.name} row={row} />
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </div>

    );
}

