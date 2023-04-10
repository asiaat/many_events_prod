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
    const [openDelGuest, setOpenDelGuest] = React.useState(false);
    const [openUpdateGuest, setOpenUpdateGuest] = React.useState(false);

    const [user, setUser] = useState();
    const [personId, setPersonId] = useState();
    const [person, setPerson] = useState([]);
    const [company, setCompany] = useState([]);

    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);
    const handleGuestOpen = () => setOpenGuest(true);
    const handleGuestClose = () => setOpenGuest(false);
    const handleDelGuestOpen = () => setOpenDelGuest(true);
    const handleDelGuestClose = () => setOpenDelGuest(false);

    const handleUpdateGuestOpen = () => setOpenUpdateGuest(true);
    const handleUpdateGuestClose = () => setOpenUpdateGuest(false);


    const handleGuestSubmit = async (event) => {
        event.preventDefault();
 
        await axios.delete("https://localhost:44450/api/mcompanies/delete/" + personId)
            .then((response) => {
                console.log(response.data);
                handleDelGuestClose()
            })

    };

    const handleGuestDeleteDialog = async (event) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);

        var guestId = data.get('guestid')
        console.log("Guest Id: " + guestId);
        setPersonId(guestId);

        handleDelGuestOpen()

    };

    
    const handleGuestUpdateDialog = async (event) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);       

        var updateComp = {
            id: parseInt(data.get('compid')),
            jurName: data.get('compjurname'),
            regCode: data.get('compregcode'),
            guestsCount: parseInt(data.get('compguestscount')),
        }
        setCompany(updateComp);

        console.log(updateComp);
       
        handleUpdateGuestOpen()

    };

    const handleGuestSave = async (event) => {
       
        event.preventDefault();
        const data = new FormData(event.currentTarget);


        var updatedComp = {
            Id: parseInt(data.get('guestid')),
            JurName: data.get('jurName'),
            RegCode: data.get('regCode'),
            GuestsCount: parseInt(data.get('guestsCount')),
        }
        console.log("handleGuestSave: " + JSON.stringify(updatedComp));
     
        await axios.put("https://localhost:44450/api/mcompanies/update",
            updatedComp)
            .then((response) => {
                console.log(response.data);
                handleUpdateGuestClose()
            })       

    };


    const renderUpdateModal = () => {
        return (
            <Modal
                open={openUpdateGuest}
                onClose={handleUpdateGuestClose}
                aria-labelledby="modal-update-title"
                aria-describedby="modal-update-description"
            >
                <Box sx={style}>
                    <Typography id="modal-update-title" variant="h6" component="h2">
                       Firma andmete muutmine
                    </Typography>
                    <Typography id="modal-update-description" sx={{ mt: 2 }}>

                    </Typography>
                    <Box component="form" onSubmit={handleGuestSave} noValidate sx={{ mt: 1 }}>

                        <input type="hidden" name="guestid" value={company.id} />
                        <TextField id="jurName"
                            name="jurName"
                            label="Juriidiline nimi"
                            defaultValue={company.jurName}
                            variant="outlined" />
                        <TextField id="regCode"
                            name="regCode"
                            label="Registrikood"
                            defaultValue={company.regCode}
                            variant="outlined" />
                        <TextField id="guestsCount"
                            name="guestsCount"
                            label="Külastajate arv"
                            defaultValue={company.guestsCount}
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
                        Uus Firma
                    </Typography>
                    <Typography id="modal-guest-description" sx={{ mt: 2 }}>

                    </Typography>
                    <Box component="form" onSubmit={handleGuestSubmit} noValidate sx={{ mt: 1 }}>
                        Firma
                        
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


    const renderDelModal = () => {
        return (
            <Modal
                open={openDelGuest}
                onClose={handleDelGuestClose}
                aria-labelledby="modal-guest-title"
                aria-describedby="modal-guest-description"
            >
                <Box sx={style}>
                    <Typography id="modal-guest-title" variant="h6" component="h2">
                       Firma eemaldamine kogu andmebaasist
                    </Typography>
                    <Typography id="modal-guest-description" sx={{ mt: 2 }}>

                    </Typography>
                    <Box noValidate sx={{ mt: 1 }}>
                        Kas soovite firmat eemaldada? (kustutatakse isiku andmed ja tema seotus üritustega)                        

                        <Box component="form" onSubmit={handleGuestSubmit}>
                        
                            <Button 
                                
                                type="submit"
                                variant="contained"
                                sx={{ mt: 3, mb: 2 }}

                            >Eemalda</Button>
                        </Box>
                        
                        <Button
                            onClick={handleDelGuestClose}
                            type="submit"
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}

                        >Ära eemalda</Button>
                    </Box>
                </Box>
            </Modal>
        );
    }


    return (
        <React.Fragment>
            {renderGuestModal()}
            {renderDelModal()}
            {renderUpdateModal()}
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
                    {row.jurName}
                </TableCell>
                <TableCell align="right">{row.regCode}</TableCell>
                <TableCell align="right">{row.guestsCount}</TableCell>
                <TableCell align="right" >
                    <Box component="form" onSubmit={handleGuestUpdateDialog}>
                        <input type="hidden" name="compid" value={row.id} />
                        <input type="hidden" name="compjurname" value={row.jurName} />
                        <input type="hidden" name="compregcode" value={row.regCode} />
                        <input type="hidden" name="compguestscount" value={row.guestsCount} />
                        
                        <Button
                        
                            type="submit"
                            variant="contained"
                            sx={{ mt: .12, mb: .12 }}

                            >muuda</Button>
                     </Box>
                </TableCell>
                <TableCell align="right" >
                    <Box component="form" onSubmit={handleGuestDeleteDialog}>
                        <input type="hidden" name="guestid" value={row.id} />
                        <Button

                            type="submit"
                            variant="contained"
                            sx={{ mt: .12, mb: .12 }}

                        >eemalda</Button>
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
                                        <TableCell>Juriidiline nimi</TableCell>
                                        <TableCell>Registrikood</TableCell>
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


export default function Companies() {

    const [rows, setRows] = useState([]);
    const [user, setUser] = useState();

    const [open, setOpen] = React.useState(false);

    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const handleSubmit = async (event) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);   

        
        const newCompany = {

            JurName: data.get('jurName'),
            RegCode: data.get('regCode'),
            GuestsCount: parseInt(data.get('guestsCount')),
            Remarks: "midagi"

        }

        console.log(newCompany)

        
        await axios.post("https://localhost:44450/api/mcompanies/create",
            newCompany)
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

    const retrieveCompanies = async () => {
        await axios.get("https://localhost:44450/api/mcompanies/companies")
            .then((response) => {
                console.log(response.data);
                setRows(response.data);

            })
    }

    

    useEffect(() => {
        setUser(localStorage.getItem("user"))
        retrieveCompanies()

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
                        Uus Firma
                    </Typography>
                    <Typography id="modal-modal-description" sx={{ mt: 2 }}>

                    </Typography>
                    <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
                       Uue firma lisamine
                        <TextField id="jurName"
                            name="jurName"
                            label="Juriidiline nimi"
                            variant="outlined" />
                        <TextField id="regCode"
                            name="regCode"
                            label="Registrikood"
                            variant="outlined" />
                        <TextField id="guestsCount"
                            name="guestsCount"
                            label="Osavõtjate arv"
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

                    >Lisa uus firma</Button>
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
                            <TableCell> Juriidiline nimi</TableCell>
                            <TableCell align="right">Registrikood</TableCell>
                            <TableCell align="right">Külaliste arv</TableCell>
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

