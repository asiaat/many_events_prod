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
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select, { SelectChangeEvent } from '@mui/material/Select';




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
    const [age, setAge] = React.useState('');

    const [user, setUser] = useState();
    const [guest, setGuest] = useState();
    const [eventId, setEventId] = useState();

    const [persons, setPersons] = useState([]);

    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);
    const handleGuestOpen = () => setOpenGuest(true);
    const handleGuestClose = () => setOpenGuest(false);


    const handleGuestSubmit = async (event) => {
        event.preventDefault();


        console.log("eventId: " + eventId + " , guestId:" + age)


        await axios.get("https://localhost:44450/api/mevents/addguest/" + eventId + "/" +age)
            .then((response) => {
                console.log(response.data);

            })
        

        handleGuestClose()

    };

    const handleChange = (event: SelectChangeEvent) => {
        setAge(event.target.value);
        console.log("age: " + age);
    };

    const handleEventSelect = (event) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);

        var eventId = data.get('eventid')
        console.log("handleEventSelect eventId: " + eventId);
        setEventId(eventId);

        handleGuestOpen()

    };

    const retrievePersons = async () => {
        await axios.get("https://localhost:44450/api/mpersons/persons")
            .then((response) => {
                console.log(response.data);
                setPersons(response.data);

            })
    }

    useEffect(() => {
        
        retrievePersons()

    }, []);
    

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
                    <Box sx={{ minWidth: 120 }}>
                        <FormControl fullWidth>
                            <InputLabel id="demo-simple-select-label">Isik</InputLabel>
                            <Select
                                labelId="demo-simple-select-label"
                                id="demo-simple-select"
                                value={age}
                                label="Isik"
                                onChange={handleChange}
                            >
 
                                {persons.map((person) => (
                                    <MenuItem value={person.id}>                                    
                                        {person.firstName}  {person.lastName}  {person.personalCodeAsString}    
                                    </MenuItem>
                                ))}

                            </Select>
                        </FormControl>
                    </Box>

                    <Box component="form" onSubmit={handleGuestSubmit} noValidate sx={{ mt: 1 }}>
                      

                        <Button

                            type="submit"
                            variant="contained"
                            sx={{ mt: 6, mb: 7 }}

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
                    {row.title}
                </TableCell>
                <TableCell align="right">{row.place}</TableCell>
                <TableCell align="right">{row.releaseDate}</TableCell>
                <TableCell align="right">{row.price}</TableCell>
                <TableCell align="right">{row.guestCount}</TableCell>
                <TableCell align="right">
                    <Box component="form" onSubmit={handleEventSelect}>
                        <input type="hidden" name="eventid" value={row.id} />
                    <Button
                        
                        type="submit"
                        variant="contained"
                        sx={{ mt: 0.1, mb: 0.1 }}

                        >Lisa uus külastaja</Button>
                    </Box>

                </TableCell>

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
                                    {row.personsList.map((historyRow) => (
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



//
export default function Events() {

    const [rows, setRows] = useState([]);
    const [user, setUser] = useState();

    const [open, setOpen] = React.useState(false);
    const [persons, setPersons] = useState([]);

    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);



    const handleSubmit = async (event) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);

        const newEvent = {
            title: data.get('eventname'),
            place: data.get('eventplace'),
            releaseDate: data.get('eventtime'),

        };
        console.log(newEvent)

        await axios.post("https://localhost:44450/api/mevents/create",
            newEvent)
            .then((response) => {
                console.log(response.data);

            })


        handleClose()
    }

    const retrieveEvents = async () => {
        await axios.get("https://localhost:44450/api/mevents/events")
            .then((response) => {
                console.log(response.data);
                setRows(response.data);

            })
    }

    const retrievePersons = async () => {
        await axios.get("https://localhost:44450/api/mpersons/persons")
            .then((response) => {
                console.log(response.data);
                setPersons(response.data);

            })
    }


    useEffect(() => {
        setUser(localStorage.getItem("user"))
        retrieveEvents()
        retrievePersons()

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
                        Uus üritus
                    </Typography>
                    <Typography id="modal-modal-description" sx={{ mt: 2 }}>

                    </Typography>
                    <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
                        Uue ürituse lisamine
                        <TextField id="eventname"
                            name="eventname"
                            label="ürituse nimi"
                            variant="outlined" />
                        <TextField id="eventplace"
                            name="eventplace"
                            label="Asukoht"
                            variant="outlined" />
                        <TextField id="eventtime"
                            name="eventtime"
                            label="Aeg"
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

                    >Lisa uus üritus</Button>
                </h1>
            );
        }
    }

    return (
        <div>
            {renderReturn()}
            {renderModal()}


            <TableContainer component={Paper}>
                <Table aria-label="collapsible table">
                    <TableHead>
                        <TableRow>
                            <TableCell />
                            <TableCell> Nimi</TableCell>
                            <TableCell align="right">Koht</TableCell>
                            <TableCell align="right">Aeg</TableCell>
                            <TableCell align="right">Hind</TableCell>
                            <TableCell align="right">Külastajate arv</TableCell>
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

