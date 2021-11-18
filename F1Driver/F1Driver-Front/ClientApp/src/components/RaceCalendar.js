import React, { Component } from 'react';
import axios from 'axios';



export class RaceCalendar extends Component {
    static displayName = RaceCalendar;



    constructor(props) {
        super(props);



        this.state = {
            Races: [],
            loading: true
        };
    }
    componentDidMount() {
        this.populateData();
    }



    static renderTable(RaceCalendar) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <tbody>
                    {RaceCalendar.map(Race =>
                        <tr key={Race.circuitId}>
                            <td style={{ color: 'white' }}>Race: {Race.raceName}</td>
                            <td style={{ color: 'white' }}>Round: {Race.round}</td>
                            <td style={{ color: 'white' }}>Date: {Race.date}</td>
                            <td style={{ color: 'white' }}>Image: <img src={Race.imageUrl}></img></td>
                            <td><a href={Race.url}>Go to wiki</a></td>
                        </tr>
                    )}
                </tbody>
            </table>
        )
    }




    render() {
        let contents = this.state.loading
            ? <p style={{ color: 'white' }}><em>Loading...</em></p>
            : RaceCalendar.renderTable(this.state.Races)



        return (
            <div>
                <h1 style={{ color: 'white' }} id="tableLabel">RaceCalendar</h1>
                <p style={{ color: 'white' }}>This years races</p>
                {contents}
            </div>
        )
    }


    populateData = async () => {
        var self = this;
        axios({
            method: 'get',
            url: 'https://localhost:44378/Races/SendRaces/SeasonsRaces'
        }).then(function (data) {
            console.log(data.data);
            self.setState({ Races: data.data, loading: false });
        }
        );
    }
}
