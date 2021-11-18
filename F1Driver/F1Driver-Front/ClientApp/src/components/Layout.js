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
            href: "#"
        },
        {
            name: "Driver Cup",
            color: "#C80000 ",
            href: "\DriverStandings"
        },
        {
            name: "Constructor Cup",
            color: "#C80000 ",
            href: "\ConstructorStandings"
        },
        {
            name: "Circuits",
            color: "#C80000 ",
            href: "\RaceCalendar"
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
