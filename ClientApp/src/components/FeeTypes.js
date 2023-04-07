import React, { Component, useState, useEffect } from 'react';
import Button from '@mui/material/Button';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Modal from '@mui/material/Modal';
import TextField from '@mui/material/TextField';
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


export default function FeeTypes() {

    const [displayName, setDisplauName] = useState("Fee Types");
    const [user, setUser] = useState();
    const [feeTypes, setFeeTypes] = useState([]);
    const [open, setOpen] = React.useState(false);

    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);
   

    const retrieveFeeTypes = async () => {
        await axios.get("https://localhost:44450/api/mfeetypes/feetypes")
            .then((response) => {
                //console.log(response.data);
                setFeeTypes(response.data)
            })
    }
    const addNewFeeType = async () => {
        const newFeeType = { name: 'Bitcoins', remarks : 'BTC' };
        await axios.post("https://localhost:44450/api/mfeetypes/new",
                            newFeeType)
            .then((response) => {
                console.log(response.data);
                //setFeeTypes(response.data)
            })

        handleClose()
    }

    useEffect(() => {
        setUser(localStorage.getItem("user"))
        retrieveFeeTypes()
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
                        Text in a modal
                    </Typography>
                    <Typography id="modal-modal-description" sx={{ mt: 2 }}>
                        
                    </Typography>
                    Uue makseviisi lisamine
                        <TextField id="outlined-basic" label="makseviis" variant="outlined" />
                    <TextField id="filled-basic" label="märkused" variant="outlined" />
                    <Button
                        onClick={addNewFeeType}
                        type="submit"
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}

                    >Salvesta</Button>
                </Box>
            </Modal>
        );
    }

    const renderFeeTypesTable = (ftypes) => {

        if (user) {

            return (
                <div>
                    <Button
                        onClick={handleOpen}
                        type="submit"                    
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}

                    >Lisa uus makseviis</Button>
                    {renderModal()}


                <table className="table table-striped" aria-labelledby="tableLabel">
                    <thead>
                        <tr>

                            <th>Name</th>
                            <th>Remarks</th>

                        </tr>
                    </thead>
                    <tbody>
                        {ftypes.map(ftypes =>
                            <tr key={ftypes.id}>
                                <td>{ftypes.name}</td>
                                <td>{ftypes.remarks}</td>

                            </tr>
                        )}
                    </tbody>
                    </table>
                </div>
            )
        } else {
            return (
                <div>
                    <h1>Puudub teabevajadus</h1>
                    
                </div>
                
            )
        }

  }

 

    return (
        <div>
         
        <h1 id="tableLabel">Maksevõimalused</h1>
         
            {renderFeeTypesTable(feeTypes)}
      </div>
    );
 

  
}
