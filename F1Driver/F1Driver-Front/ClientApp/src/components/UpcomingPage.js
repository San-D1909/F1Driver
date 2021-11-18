import React, { Component } from 'react';
import axios from 'axios';



export class UpcomingPage extends Component {
    static displayName = UpcomingPage;



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

    populateData = async () => {
        var self = this;
        axios({
            method: 'get',
            url: 'https://localhost:44378/controller/method/urlbovenmethod'
        }).then(function (data) {
            console.log(data.data);
            self.setState({ standings: data.data, loading: false });
        });
    }
}
