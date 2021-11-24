import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
    static displayName = Layout.name;

    /*--------------------
    Items
    --------------------*/
    items = [
        {
            name: "Upcoming",
            color: "#C80000 ",
            href: "\UpcomingRace"
        },
        {
            name: "Drivers",
            color: "#C80000 ",
            href: "\DriverStandings"
        },
        {
            name: "Constructors",
            color: "#C80000 ",
            href: "\ConstructorStandings"
        },
        {
            name: "Circuits",
            color: "#C80000 ",
            href: "\RaceCalendar"
        },
        {
            name: "Login",
            color: "#C80000 ",
            href: "\Login"
        }
    ];

    render() {
        return (
            <div>
                <NavMenu items={this.items} />
                <Container>
                    {this.props.children}
                </Container>
            </div>
        );
    }
}
