import React, { Component } from 'react';
import axios from 'axios';
import "./CSS/Cards.css";

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



    static render(UpcomingRace) {
        return (
            <div class="row" >
                {UpcomingRace.map(Race =>
                    <div class="col-sm-4 mt-4">
                        <div class="card" style={{ backgroundColor: "white", minHeight: "520px", maxHeight: "520px", borderColor: "#FF1801" }}>
                            <div class="card-body">
                                <img class="card-img-top" style={{ maxHeight: "200px" }} src={Race.imageUrl} alt="CircuitPic" width="auto" height="auto" ></img>
                                <h4 style={{ textAlign: "center", fontWeight: "bold" }} class="card-title">{Race.raceName}</h4>
                                <hr class="solid"></hr>
                                <p property="date" style={{ textAlign: "center" }} class="card-text">Date: {Race.date}</p>
                                <p property="round" style={{ textAlign: "center" }} class="card-text">Round: {Race.round}</p>
                                <p property="country" style={{ textAlign: "center" }} class="card-text">Country: {Race.country}</p>
                            </div>
                            <div class="card-footer" style={{ backgroundColor: "darkgray", Height: "auto", maxHeight: "auto" }} >
                                <img src={Race.flagUrl} alt="FlagPic" width="auto" Height="50px"  ></img>
                            </div>
                        </div>
                        <p><a class="buttonCard" href={Race.url}>Go to wiki</a></p>
                    </div>
                )}
            </div>

        )
    }


    render() {
        let contents = this.state.loading
            ? <p style={{ color: 'white' }}><em>Loading...</em></p>
            : UpcomingRace.render(this.state.Race)



        return (
            <div>

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
