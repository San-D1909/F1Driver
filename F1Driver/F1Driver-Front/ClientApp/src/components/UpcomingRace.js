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



    static render(Race) {
        return (
            <body>
                <div class="row" >
                    {Race.map(race =>
                        <div class="col-sm-4">
                            <div class="card">
                                <div class="card-body">
                                    <img class="card-img-top" src={race.imageUrl} alt="Card image cap" width="auto" height="auto"></img>
                                    <h5 class="card-title">{race.raceName}</h5>
                                    <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</p>
                                    <div>
                                        <p class="card-text">{race.country}<img style={{ float: "right" }} src={race.flagUrl} width="100" height="auto"></img></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    )}
                </div>
            </body>
        )
    }


    render() {
        let contents = this.state.loading
            ? <p style={{ color: 'white' }}><em>Loading...</em></p>
            : UpcomingRace.render(this.state.Race)



        return (
            <div>
                <h1 style={{ color: 'white' }} id="tableLabel">UpcomingRace</h1>
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
