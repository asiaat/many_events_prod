import React, { Component } from 'react';

export class FeeTypes extends Component {
  static displayName = "Fee Types";

  constructor(props) {
    super(props);
    this.state = { feetypes: [], loading: true };
  }

  componentDidMount() {
      this.populateFeeTypes();
  }

  static renderFeeTypesTable(ftypes) {
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
    );
  }

  render() {
    let contents = this.state.loading
        ? <p><em>Loading...</em></p>
        : FeeTypes.renderFeeTypesTable(this.state.feetypes);

    return (
      <div>
        <h1 id="tableLabel">Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateFeeTypes() {
    const response = await fetch('api/mfeetypes/feetypes');
    const data = await response.json();
    this.setState({ feetypes: data, loading: false });
  }
}
