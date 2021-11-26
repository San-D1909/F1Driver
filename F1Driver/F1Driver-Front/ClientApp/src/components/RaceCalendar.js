import React, { Component } from 'react';
import axios from 'axios';
import "./CSS/Cards.css";

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

    static render(RaceCalendar) {
        return (
            <body>
                <div class="row" >
                    {RaceCalendar.map(Race =>
                        <div class="col-sm-4 mt-4">
                            <div class="card" style={{ backgroundColor: "white", minHeight: "520px", maxHeight: "520px", borderColor: "#FF1801" }}>
                                <div class="card-body">
                                    <img class="card-img-top" style={{ maxHeight: "200px" }} src={Race.imageUrl} alt="CircuitPic" width="auto" height="auto" ></img>
                                    <h4 style={{ textAlign: "center", fontWeight: "bold" }} class="card-title">{Race.raceName}</h4>
                                    <hr class="solid"></hr>
                                    <p style={{ textAlign: "center" }} class="card-text">Date: {Race.date}</p>
                                    <p style={{ textAlign: "center" }} class="card-text">Round: {Race.round}</p>
                                    <p style={{ textAlign: "center" }} class="card-text">Country: {Race.country}</p>
                                </div>
                                <div class="card-footer" style={{ backgroundColor: "darkgray", Height: "auto", maxHeight: "auto" }} >
                                    <img src={Race.flagUrl} alt="FlagPic" width="auto" Height="50px"  ></img>
                                </div>
                            </div>
                            <p><button class="buttonCard" onClick={Race.url}>Go to wiki</button></p>
                        </div>
                    )}
                </div>
            </body>
        )
    }


    render() {
        let contents = this.state.loading
            ? <p style={{ color: 'white' }}><em>Loading...</em></p>
            : RaceCalendar.render(this.state.Races)



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
