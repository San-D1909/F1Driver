import React, { Component } from 'react';
import axios from 'axios';



export class ConstructorStandings extends Component {
    static displayName = ConstructorStandings;



    constructor(props) {
        super(props);



        this.state = {
            standings: [],
            loading: true
        };
    }
    componentDidMount() {
        this.populateData();
    }



    static renderTable(ConstructorStandings) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <tbody>
                    {ConstructorStandings.map(standing =>
                        <tr key={standing.constructor}>
                            <td style={{ color: 'white' }}>position: {standing.position}</td>
                            <td style={{ color: 'white' }}>Constructor: {standing.constructor.name}</td>
                            <td style={{ color: 'white' }}>Points: {standing.points}</td>
                            <td><a href={standing.constructor.url}>Go to wiki</a></td>
                        </tr>
                    )}
                </tbody>
            </table>
        )
    }




    render() {
        let contents = this.state.loading
            ? <p style={{ color: 'white' }}><em>Loading...</em></p>
            : ConstructorStandings.renderTable(this.state.standings)



        return (
            <div>
                <h1 style={{ color: 'white' }} id="tableLabel">ConstructorStandings</h1>
                <p style={{ color: 'white' }}>Current standings</p>
                {contents}
            </div>
        )
    }


    populateData = async () => {
        var self = this;
        axios({
            method: 'get',
            url: 'https://localhost:44378/ConstructorStandings/SendConstructorStandings/GetConstructorStandings'
        }).then(function (data) {
            console.log(data.data);
            self.setState({ standings: data.data, loading: false });
        }
        );
    }
}
