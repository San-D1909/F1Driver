import React, { Component } from 'react';
import axios from 'axios';



export class UpcomingRace extends Component {
    static displayName = UpcomingRace;

    constructor(props) {
        super(props);

        this.state = {
            Race: [],
            loading: true
        };
    }
    componentDidMount() {
        this.populateData();
    }

    static renderTable(UpcomingRace) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <tbody>
{/*                    {UpcomingRace.map(Race =>
                        <tr key={Race.circuitId}>
                            <td style={{ color: 'white' }}>Race: {Race.raceName}</td>
                            <td style={{ color: 'white' }}>Round: {Race.round}</td>
                            <td style={{ color: 'white' }}>Date: {Race.date}</td>
                            <td><a href={Race.url}>Go to wiki</a></td>
                        </tr>
                    )}*/}
                </tbody>
            </table>
        )
    }

    render() {
        let contents = this.state.loading
            ? <p style={{ color: 'white' }}><em>Loading...</em></p>
            : UpcomingRace.renderTable(this.state.Race)



        return (
            <div>
                <h1 style={{ color: 'white' }} id="tableLabel">UpcomingRace</h1>
                <p style={{ color: 'white' }}>This years races</p>
                {contents}
            </div>
        )
    }

    populateData = async () => {
        var self = this;
        axios({
            method: 'get',
            url: 'https://localhost:44378/Races/SendUpcomingRace/UpcomingRace'
        }).then(function (data) {
            console.log(data.data);
            self.setState({ Race: data.data, loading: false });
        });
    }
}
