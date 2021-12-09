import React, { Component } from 'react';

export class Logout extends Component {
    static displayName = Logout.name;

    componentDidMount() {
        this.clearCache();
        console.log(1)
    }

    clearCache = function () {
        localStorage.clear();
        window.location.assign("");
        console.log(2)
    }
    render() {
        if (localStorage.getItem("token")) {
            return (
                this.clearCache()
            )
        }
    }
}