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
            first: [],
            second: [],
            third: [],
            fastestLap: [],
            minPage: ' ',
            maxPage: '',
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
            console.log(data.data)
        });
    }

    SetBet = (driver, category, race) => {
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
            var max = first.id + data.data.length - 1;
            self.setState({ races: data.data, page: first.id, minPage: first.id, maxPage: max });
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

    GetBet =(page)=> {
        var self = this;
        var user = this.state.user
        axios({
            method: 'POST',
            url: 'http://localhost:5001/Betting/GetBet/GetBet',
            params: {
                race: page,
                userID: user.id
            }
        }).then(function (data) {
            if (data.data.userID != null && data.data.userID != 0) {
                self.setState({ first: data.data.first, second: data.data.second, third: data.data.third, fastestLap: data.data.fastestLap })
            }
        });
    }

    NextPage(page) {
        if (page == this.state.maxPage) {
            return;
        }
        this.setState({ page: page + 1, first: [], second: [], third: [], fastestLap: [], message: '' })
    }
    PrevPage(page) {
        if (page == this.state.minPage) {
            return;
        }
        this.setState({ page: page - 1, first: [], second: [], third: [], fastestLap: [], message: '' })
    }

    handleFirst = (id) => {
        let obj = JSON.parse(id.target.value);
        this.setState({ first: obj.driver })
        console.log(obj)
        this.setState({ message: obj.driver.givenName + ' is going to be first' });
        this.SetBet(obj.driver.id, 1, this.state.page)
    }
    handleSecond = (id) => {
        let obj = JSON.parse(id.target.value);
        this.setState({ second: obj.driver })
        console.log(obj)
        this.setState({ message: obj.driver.givenName + ' is going to be second' });
        this.SetBet(obj.driver.id, 2, this.state.page)
    }
    handleThird = (id) => {
        let obj = JSON.parse(id.target.value);
        this.setState({ third: obj.driver })
        console.log(obj)
        this.setState({ message: obj.driver.givenName + ' is going to be third' });
        this.SetBet(obj.driver.id, 3, this.state.page)
    }
    handleFasestLap = (id) => {
        let obj = JSON.parse(id.target.value);
        this.setState({ fastestLap: obj.driver })
        console.log(obj)
        this.setState({ message: obj.driver.givenName + ' is going to have the fastest lap' });
        this.SetBet(obj.driver.id, 4, this.state.page)
    }

    render() {
        if (!localStorage.getItem("token")) {
            return (
                window.location.assign("login")
            )
        }
        this.GetBet(this.state.page)
        var raceInt = this.state.page - this.state.minPage
        var race = this.state.races[raceInt]
        return (
            <Card className=".mx-auto" >
                {this.state.message.length !== 0 &&
                    <div className="alert alert-warning" >
                        <strong>{this.state.message}</strong>
                    </div>
                }
                <CardBody>
                    <button style={{ margin: 10 }} className="btn btn-danger" onClick={(e) => window.location.assign("Group")}>Grouppage</button>
                    <div style={{ display: 'inline-block', float: 'right', margin: 0 }}>
                        <button style={{ display: 'inline-block', margin: 10, fontWeight: 'bold' }} className="btn btn-success" onClick={(e) => this.PrevPage(this.state.page)}>{"<"}</button>
                        <p style={{ display: 'inline-block' }}>Page {this.state.page}</p>
                        <button style={{ display: 'inline-block', margin: 10, fontWeight: 'bold' }} className="btn btn-success" onClick={(e) => this.NextPage(this.state.page)}>{">"}</button>
                    </div>
                    {race !== undefined &&
                        <h1>{race.raceName}</h1>
                    }
                    <div className="row">
                        <Card style={{ backgroundColor: "black", minHeight: "300px", minWidth: "200px", maxWidth: '200px', borderColor: "red", marginRight: '10px' }}>
                            <CardBody>
                                <p style={{ color: 'white', textAlign: 'center' }}>First place:</p>
                                <select className="btn btn-danger" style={{ maxWidth: '160px', minWidth: "160px", marginRight: '10px' }} name="drivers" onChange={this.handleFirst} value={this.state.drivers}>
                                    <option>{this.state.first.givenName} {this.state.first.familyName}</option>
                                    {this.state.drivers.map((driver) => <option key={driver.driver.id} value={JSON.stringify(driver)}>{driver.driver.givenName} {driver.driver.familyName}</option>)}
                                </select>
                                {this.state.first.id > 0 &&
                                    <img style={{ minWidth: "auto", minHeight: 'auto', marginTop: '15px', alignContent: 'center' }} src={this.state.first.imageUrl} alt="DriverPic" width="auto" height="170"></img>
                                }
                            </CardBody>
                        </Card>
                        <Card style={{ backgroundColor: "black", minHeight: "300px", minWidth: "200px", maxWidth: '200px', borderColor: "red", marginRight: '10px' }}>
                            <CardBody>
                                <p style={{ color: 'white', textAlign: 'center' }}>Second place:</p>
                                <select className="btn btn-danger" style={{ maxWidth: '160px', minWidth: "160px", marginRight: '10px' }} name="drivers" onChange={this.handleSecond} value={this.state.drivers}>
                                    <option value="select">{this.state.second.givenName} {this.state.second.familyName}</option>
                                    {this.state.drivers.map((driver) => <option key={driver.driver.id} value={JSON.stringify(driver)}>{driver.driver.givenName} {driver.driver.familyName}</option>)}
                                </select>
                                {this.state.second.id > 0 &&
                                    <img style={{ minWidth: "auto", minHeight: 'auto', marginTop: '15px', alignContent: 'center' }} src={this.state.second.imageUrl} alt="DriverPic" width="auto" height="170"></img>
                                }
                            </CardBody>
                        </Card>
                        <Card style={{ backgroundColor: "black", minHeight: "300px", minWidth: "200px", maxWidth: '200px', borderColor: "red", marginRight: '10px' }}>
                            <CardBody>
                                <p style={{ color: 'white', textAlign: 'center' }}>Third place:</p>
                                <select className="btn btn-danger" style={{ maxWidth: '160px', minWidth: "160px", marginRight: '10px' }} name="drivers" onChange={this.handleThird} value={this.state.drivers}>
                                    <option value="select">{this.state.third.givenName} {this.state.third.familyName}</option>
                                    {this.state.drivers.map((driver) => <option key={driver.driver.id} value={JSON.stringify(driver)}>{driver.driver.givenName} {driver.driver.familyName}</option>)}
                                </select>
                                {this.state.third.id > 0 &&
                                    <img style={{ minWidth: "auto", minHeight: 'auto', marginTop: '15px', alignContent: 'center' }} src={this.state.third.imageUrl} alt="DriverPic" width="auto" height="170"></img>
                                }
                            </CardBody>
                        </Card>
                        <Card style={{ backgroundColor: "black", minHeight: "300px", minWidth: "200px", maxWidth: '200px', borderColor: "red", marginRight: '10px' }}>
                            <CardBody>
                                <p style={{ color: 'white', textAlign: 'center' }}>Fastest lap:</p>
                                <select className="btn btn-danger" style={{ maxWidth: '160px', minWidth: "160px", marginRight: '10px' }} name="drivers" onChange={this.handleFasestLap} value={this.state.drivers}>
                                    <option value="select">{this.state.fastestLap.givenName} {this.state.fastestLap.familyName}</option>
                                    {this.state.drivers.map((driver) => <option key={driver.driver.id} value={JSON.stringify(driver)}>{driver.driver.givenName} {driver.driver.familyName}</option>)}
                                </select>
                                {this.state.fastestLap.id > 0 &&
                                    <img style={{ minWidth: "auto", minHeight: 'auto', marginTop: '15px', alignContent: 'center' }} src={this.state.fastestLap.imageUrl} alt="DriverPic" width="auto" height="170"></img>
                                }
                            </CardBody>
                        </Card>
                    </div>
                </CardBody>
            </Card >
        );
    }
}