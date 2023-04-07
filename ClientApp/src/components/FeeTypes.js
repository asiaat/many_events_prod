import React, { Component, useState, useEffect } from 'react';


export default function FeeTypes() {

    const [displayName, setDisplauName] = useState("Fee Types");
    const [user, setUser] = useState();
    const [feeTypes, setFeeTypes] = useState([]);

    const populateFeeTypes = async () => {
        const response = await fetch('api/mfeetypes/feetypes');
        const data = await response.json();
        setFeeTypes(data);
    }


    useEffect(() => {
        setUser(localStorage.getItem("user"))
        populateFeeTypes()
    }, []);

    const renderFeeTypesTable = (ftypes) => {

        if (user) {

            return (
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
            )
        } else {
            return (
                <h1>Puudub teabevajadus</h1>
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
