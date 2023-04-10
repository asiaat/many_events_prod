import React, { Component, useState, useEffect } from 'react';
import Button from '@mui/material/Button';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
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


export default function FeeTypes() {

    const [displayName, setDisplauName]         = useState("Fee Types");
    const [user, setUser]                       = useState();
    const [feeTypes, setFeeTypes] = useState([]);

    const [open, setOpen]                       = useState(false);
    const [openDelModal, setOpenDelModal]       = useState(false);
    const [openChangeModal, setOpenChangeModal] = useState(false);

    const [feeID, setFeeID]                     = useState();

    const [feeTypeID, setFeeTypeID]             = useState();
    const [feeTypeName, setFeeTypeName]         = useState();
    const [feeTypeRemarks, setFeeTypeRemarks]   = useState();

    const handleOpen  = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const handleOpenDelModal  = () => setOpenDelModal(true);
    const handleCloseDelModal = () => setOpenDelModal(false);

    const handleOpenChangeModal  = () => setOpenChangeModal(true);
    const handleCloseChangeModal = () => setOpenChangeModal(false);
   

    const retrieveFeeTypes = async () => {
        await axios.get("https://localhost:44450/api/mfeetypes/feetypes")
            .then((response) => {
                //console.log(response.data);
                setFeeTypes(response.data)
            })
    }

    
    const handleSubmit = async (event) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);
        const newFeeType = {
            name: data.get('feename'),
            remarks: data.get('feeremarks')
        };
        await axios.post("https://localhost:44450/api/mfeetypes/new",
            newFeeType)
            .then((response) => {
                console.log(response.data);

            })

        handleClose()

    };

    const handleChange = async (event) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);
        const feeType = {
            id: data.get('feetypeid'),
            name: data.get('feetypename'),
            remarks: data.get('feetyperemarks')
        };

        console.log(feeType);

        setFeeTypeID(feeType.id);
        setFeeTypeName(feeType.name);
        setFeeTypeRemarks(feeType.remarks);

        handleOpenChangeModal();

    };

    const handleSaveChanges = async (event) => {
        event.preventDefault();
        
        const data = new FormData(event.currentTarget);
        var objectID = feeTypeID;
        const updatedFeeType = {
            id: feeTypeID ,
            name: data.get('feetypename'),
            remarks: data.get('feetyperemarks')
        };
        

        console.log(updatedFeeType);
        console.log(feeTypeID);

        
        await axios.put("https://localhost:44450/api/mfeetypes/update",
            updatedFeeType)
            .then((response) => {
                console.log(response.data);

            })
        

        handleCloseChangeModal();

    };

    const handleDelete = async (event) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);
        

        var objId = data.get("feetypeid");
        var url = "https://localhost:44450/api/mfeetypes/delete/" + objId;

        console.log(url);
        
        await axios.delete(url)
            .then((response) => {
                console.log(response.data);

            })
        
        //handleCloseDelModal()

    };

    useEffect(() => {
        setUser(localStorage.getItem("user"))
        retrieveFeeTypes()
    }, []);


    const renderDelModal = () => {
        return (
            <Modal
                open={openDelModal}
                onClose={handleCloseDelModal}
                aria-labelledby="modal-del-title"
                aria-describedby="modal-del-description"
            >
                <Box sx={style}>
                    <Typography id="modal-del-title" variant="h6" component="h2">
                        Text in a modal
                    </Typography>
                    <Typography id="modal-del-description" sx={{ mt: 2 }}>

                    </Typography>
                    <Box component="form" onSubmit={handleDelete} noValidate sx={{ mt: 1 }}>
                        Kas soovite makseviisi eemaldada?
                        (Kustutatakse tabelist)
                         
                        <Button

                            type="submit"
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}

                        >Eemalda</Button>
                    </Box>
                </Box>
            </Modal>
        );
    }


    const renderModalChange = () => {
        return (
            <Modal
                open={openChangeModal}
                onClose={handleCloseChangeModal}
                aria-labelledby="modal-change-title"
                aria-describedby="modal-change-description"
            >
                <Box sx={style}>
                    <Typography id="modal-change-title" variant="h6" component="h2">
                        Text in a modal
                    </Typography>
                    <Typography id="modal-change-description" sx={{ mt: 2 }}>

                    </Typography>
                    <Box component="form" onSubmit={handleSaveChanges} noValidate sx={{ mt: 1 }}>
                        Uue makseviisi lisamine
                         
                        <TextField id="feename"
                            name="feetypename"
                            label="makseviis"
                            variant="outlined"
                            defaultValue={feeTypeName}
                            
                        />
                        <TextField id="feeremarks"
                            name="feetyperemarks"
                            label="märkused"
                            variant="outlined"
                            defaultValue={feeTypeRemarks}
                            
                        />

                        <Button

                            type="submit"
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}

                        >Salvesta muudatused</Button>
                    </Box>
                </Box>
            </Modal>
        );
    }


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
                    <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
                        Uue makseviisi lisamine
                        <TextField id="feename"
                            name="feename"
                            label="makseviis"
                            variant="outlined" />
                        <TextField id="feeremarks"
                            name = "feeremarks"
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

    const renderFeeTypesTable = (ftypes) => {

            return (
                <div>
                    <Button
                        onClick={handleOpen}
                        type="submit"                    
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}

                    >Lisa uus makseviis</Button>
                    {renderModal()}
                    {renderDelModal()}
                    {renderModalChange()}


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
                                <td>
                                    <Box component="form" onSubmit={handleChange} noValidate sx={{ mt: 0.5 }}>

                                        <input type="hidden" id="feetypeid" name="feetypeid" value={ftypes.id} />
                                        <input type="hidden" id="feetypename" name="feetypename" value={ftypes.name} />
                                        <input type="hidden" id="feetyperemarks" name="feetyperemarks" value={ftypes.remarks} />

                                        <Button

                                            type="submit"
                                            variant="contained"
                                            sx={{ mt: 0.1, mb: 0.25 }}

                                        >Muuda <CreateOutlinedIcon /></Button>
                                    </Box>
                                </td>
                                <td>
                                    <Box component="form" onSubmit={handleDelete} noValidate sx={{ mt: 0.5 }}>
                                        
                                        <input type="hidden" id="feetypeid" name="feetypeid" value={ftypes.id} />

                                        <Button

                                            type="submit"
                                            variant="contained"
                                            sx={{ mt: 0.1, mb: 0.25 }}

                                        >Kustuta <DeleteForeverOutlinedIcon /> </Button>
                                    </Box>
                                </td>
                                

                            </tr>
                        )}
                    </tbody>
                    </table>
                </div>
            )
       

  }

 

    return (
        <div>
         
        <h1 id="tableLabel">Maksevõimalused</h1>
         
            {renderFeeTypesTable(feeTypes)}
      </div>
    );
 

  
}
