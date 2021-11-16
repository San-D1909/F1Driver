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



    static renderTable(standing) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>title</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        <tr>
                            <td>{standings.meal}</td>
                            <td>{standings.calories}Kcal</td>
                            <td>{standings.protein}g</td>
                            <td>{standings.fats}g</td>
                            <td>{standings.carbohydrates}g</td>
                            <td>{standings.fibers}g</td>
                            <td><a>Choose Meal</a></td>
                        </tr>
                    }
                </tbody>
            </table>
        )
    }




    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : DriverStandings.renderTable(this.state.standings)



        return (
            <div>
                <h1 id="tableLabel">Driver standings</h1>
                <p>Label</p>
                {contents}
            </div>
        )
    }


    populateData = async () => {
        var self = this;
        axios({
            method: 'get',
            url: 'https://localhost:44378/DriverStandings/SendDriverStandings/GetDriverStandings'
        }).then(function (data) {
            console.log(data.data);
            self.setState({ standings: data.data, loading: false });
        }
        );
    }
}
