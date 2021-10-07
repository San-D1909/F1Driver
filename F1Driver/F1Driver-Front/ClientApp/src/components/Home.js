import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div>
                <h1>Welcome</h1>
                <p>This website is currently under devolopment, to see current F1 news go to:</p>
                <ul>
                    <a href='https://www.formula1.com/'>Official F1 website</a>
                </ul>
                    <iframe src="https://giphy.com/embed/342Zsv5S4W8XC" width="480" height="252"></iframe>
            </div>
        );
    }
}
