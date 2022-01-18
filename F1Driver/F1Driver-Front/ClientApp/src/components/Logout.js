import React, { Component } from 'react';
import { Link, Redirect } from 'react-router-dom';

export class Logout extends Component {
    static displayName = Logout.name;

    componentDidMount() {
        this.clearCache();
        console.log(1)
    }

    clearCache = function () {
        localStorage.clear();
        console.log(2)
        return (
            <Redirect exact to="/login" />
        );
    }
    render() {
        if (localStorage.getItem("token")) {
            return (
                this.clearCache()
            )
        }
    }
}