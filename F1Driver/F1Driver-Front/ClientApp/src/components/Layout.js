import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

var
    items = [
        {
            name: "Upcoming",
            color: "#C80000 ",
            href: "UpcomingRace",
        },
        {
            name: "Drivers",
            color: "#C80000 ",
            href: "DriverStandings",
        },
        {
            name: "Constructors",
            color: "#C80000 ",
            href: "ConstructorStandings",
        },
        {
            name: "Circuits",
            color: "#C80000 ",
            href: "RaceCalendar",
        },
    ];

export class Layout extends Component {
    static displayName = Layout.name;

    LogInCheck= ()=> {
        if (!localStorage.getItem("loggedin")) {
            var loginItem =
                [
                    {
                        name: "Login",
                        color: "#C80000 ",
                        href: "Login",
                    }
                ]
            items = items.concat(loginItem);
        }
    }

    render() {
        this.LogInCheck();
        return (
            <div>
                <NavMenu items={items} />
                <Container>
                    {this.props.children}
                </Container>
            </div>
        );
    }
}


