import React, { Component } from 'react';
import axios from 'axios';



export class DriverStandings extends Component {
    static displayName = DriverStandings;



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



    static renderTable(DriverStandings) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <tbody>
                    {DriverStandings.map(standing =>
                        <tr key={standing.Position}>
                            <td style={{ color: 'white' }}>position: {standing.position}</td>
                            <td style={{ color: 'white' }}>DriverID: {standing.driver.givenName} {standing.driver.familyName}</td>
                            <td style={{ color: 'white' }}>Image: <img src={standing.driver.imageUrl} alt="DriverPic" width="auto" height="170"></img></td>
                            <td style={{ color: 'white' }}>Points: {standing.points}</td>
                            <td><a href={standing.driver.url}>Go to wiki</a></td>
                        </tr>
                    )}
                </tbody>
            </table>
        )
    }




    render() {
        let contents = this.state.loading
            ? <p style={{ color: 'white' }}><em>Loading...</em></p>
            : DriverStandings.renderTable(this.state.standings)



        return (
            <div>
                <h1 style={{ color: 'white' }} id="tableLabel">DriverStandings</h1>
                <p style={{ color: 'white' }}>Current standings</p>
                {contents}
            </div>
        )
    }


    populateData = async () => {
        var self = this;
        axios({
            method: 'get',
            url: 'https://localhost:44378/DriverStandings/SendDriverStandings/DriverStandings'
        }).then(function (data) {
            console.log(data.data);
            self.setState({ standings: data.data, loading: false });
        }
        );
    }
}
