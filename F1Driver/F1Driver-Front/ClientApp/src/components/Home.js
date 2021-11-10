import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <html>
                <div>
                    <h1 Style="color:White;">Welcome</h1>
                    <p Style="color:White;">This website is currently under devolopment, to see current F1 news go to: <a href='https://www.formula1.com/'>Official F1 website</a></p>
                    <iframe src="https://giphy.com/embed/342Zsv5S4W8XC" width="480" height="252"></iframe>
                </div>
            </html>
        );
    }
}