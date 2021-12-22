import React, { Component } from 'react';
import axios from 'axios';
import Card from 'reactstrap/lib/Card';
import CardBody from 'reactstrap/lib/CardBody';
import Form from 'reactstrap/lib/Form';
import Label from 'reactstrap/lib/Label';
import { Input } from 'reactstrap';
import { Layout } from '../Layout';


export class Bet extends Component {
    static displayName = Bet.name;

    constructor(props) {
        super(props)

        this.state = {
            message: '',
            drivers: [],
            user: [],
            races: [],
            page: '',
        }
    }

    componentDidMount() {
        this.GetDrivers();
        this.GetUpcomingRaces();
        this.GetUser();
    }

    GetDrivers = async () => {
        var self = this;
        axios({
            method: 'get',
            url: 'http://localhost:5000/DriverStandings/SendDriverStandings/DriverStandings'
        }).then(function (data) {
            self.setState({ drivers: data.data });
        });
    }
    SetBet = (driver, category,race) => {
        var self = this;
        var user = this.state.user.id
        console.log(user)
        axios({
            method: 'POST',
            url: 'http://localhost:5001/Betting/SetBet/SetBet',
            params: {
                driver: driver,
                category: category,
                userID: user,
                race: race
            }
        }).then(function (data) {

        });
    }

    GetUpcomingRaces = async () => {
        var self = this;
        axios({
            method: 'get',
            url: 'http://localhost:5000/Races/SendUpcomingRace/UpcomingRace'
        }).then(function (data) {
            console.log(data.data);
            var first = data.data[0]
            console.log(first)
            self.setState({ races: data.data, page: first.id });
        });
    }

    GetUser = async () => {
        var self = this;
        axios({
            method: 'GET',
            url: 'http://localhost:5000/UserAuth/GetUserByToken/GetUserByToken',
            params: {
                token: localStorage.getItem("token"),
            }
        }).then(function (data) {
            self.setState({ user: data.data });
        });
    }
    NextPage(page) {
        this.setState({ page: page + 1 })
    }
    PrevPage(page) {
        this.setState({ page: page - 1 })
    }

    render() {
        if (!localStorage.getItem("token")) {
            return (
                window.location.assign("login")
            )
        }
        return (
            <Card>
                {this.state.message.length !== 0 &&
                    <div className="alert alert-warning" >
                        <strong>{this.state.message}</strong>
                    </div>
                }
                <CardBody>
                    <button style={{ margin: 10 }} className="btn btn-danger" onClick={(e) => window.location.assign("Group")}>Grouppage</button>
                    <div style={{ display: 'inline-block', float: 'right', margin:0 }}>
                        <button style={{ display: 'inline-block', margin: 10, fontWeight: 'bold' }} className="btn btn-success" onClick={(e) => this.PrevPage(this.state.page)}>{"<"}</button>
                        <p style={{ display: 'inline-block' }}>Page {this.state.page}</p>
                        <button style={{ display: 'inline-block', margin: 10, fontWeight: 'bold'}} className="btn btn-success" onClick={(e) => this.NextPage(this.state.page)}>{">"}</button>
                    </div>
                    <table className='table table-striped' aria-labelledby="tabelLabel">
                        <tbody>
                            <tr>
                                <th>Driver</th>
                                <th>First</th>
                                <th>Second</th>
                                <th>Third</th>
                                <th>FastestLap</th>
                            </tr>
                            {this.state.drivers.map(driver =>
                                <tr key={driver.driver.id}>
                                    <td>{driver.driver.givenName} {driver.driver.familyName}</td>
                                    <td style={{ fontWeight: 'bold' }} className="btn btn-danger" onClick={(e) => this.SetBet(driver.driver.id, "1", this.state.page)}>X</td>
                                    <td style={{ fontWeight: 'bold'  }} className="btn btn-danger" onClick={(e) => this.SetBet(driver.driver.id, "2", this.state.page)}>X</td>
                                    <td style={{ fontWeight: 'bold' }} className="btn btn-danger" onClick={(e) => this.SetBet(driver.driver.id, "3", this.state.page)}>X</td>
                                    <td style={{ fontWeight: 'bold' }} className="btn btn-danger" onClick={(e) => this.SetBet(driver.driver.id, "4", this.state.page)}>X</td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </CardBody>
            </Card>
        );
    }
}