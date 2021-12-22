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
        if (!localStorage.getItem("token")) {
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
        else {
            var logoutItem =
                [
                    {
                        name: "Logout",
                        color: "#C80000 ",
                        href: "Logout",
                    }
                ]
            var groupItem =
                [
                    {
                        name: "Groups",
                        color: "#C80000 ",
                        href: "Group",
                    }
                ]
            var BetItem =
                [
                    {
                        name: "Bet",
                        color: "#C80000 ",
                        href: "Bet",
                    }
                ]
            items = items.concat(groupItem);
            items = items.concat(BetItem);
            items = items.concat(logoutItem);
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


