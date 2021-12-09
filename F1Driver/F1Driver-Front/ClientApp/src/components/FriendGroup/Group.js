import React, { Component } from 'react';
import axios from 'axios';

export class Group extends Component {
    static displayName = Group.name;

    constructor(props) {
        super(props)

        this.state = {
            user: [],
            test: '',
        }
    }
    componentDidMount() {
        if (!localStorage.getItem("token")) {
            return (
                window.location.assign("../login")
            )
        }
        this.GetUser();

    }
    /*
        GetGroup = async () => {
            var self = this;
            axios({
                method: 'get',
                url: 'https://localhost:44322/Group/CreateGroup/CreateGroup'
            }).then(function (data) {
                console.log(data.data);
                self.setState({  });
            });
        }*/

    GetUser = async () => {
        var self = this;
        axios({
            method: 'GET',
            url: 'https://localhost:44378/UserAuth/GetUserByToken/GetUserByToken',
            params: {
                token: localStorage.getItem("token"),
            }
        }).then(function (data) {
            console.log(data.data);
            self.setState({ user: data.data, test: "ok" });
            if (self.state.user.friendGroup === "0"||self.state.user.friendGroup == null) {
                window.location.assign("../CreateGroup")
            }
        });
    }

    render() {
        return (
            <div>
                <h1 style={{ color: "white" }}>Welcome</h1>
            </div>
        );
    }
}