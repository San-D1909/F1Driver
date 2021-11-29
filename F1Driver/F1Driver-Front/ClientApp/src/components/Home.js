import React, { Component } from 'react';
import { NavMenu } from '../components/NavMenu';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div>s
                <h1 style={{ color: "white" }}>Welcome</h1>
                <p style={{ color: "white" }}>This website is currently under devolopment, to see current F1 news go to: <a href='https://www.formula1.com/'>Official F1 website</a></p>
                <iframe src="https://giphy.com/embed/342Zsv5S4W8XC" alt="MaxWheely" title="MaxWheelyGif" width="480" height="252"></iframe>
            </div>
        );
    }
}