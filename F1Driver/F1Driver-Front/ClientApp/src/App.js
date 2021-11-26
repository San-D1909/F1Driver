import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { DriverStandings } from './components/DriverStandings';
import { ConstructorStandings } from './components/ConstructorStandings';
import { RaceCalendar } from './components/RaceCalendar';
import { UpcomingRace } from './components/UpcomingRace';
import { Login } from './components/Login';
import { Register } from './components/Register';

import './custom.css'

export default class App extends Component {
    static displayName = App.name;



    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route exact path='/DriverStandings' component={DriverStandings} />
                <Route exact path='/ConstructorStandings' component={ConstructorStandings} />
                <Route exact path='/RaceCalendar' component={RaceCalendar} />
                <Route exact path='/UpcomingRace' component={UpcomingRace} />
                <Route exact path='/Login' component={Login} />
                <Route exact path='/Register' component={Register} />
            </Layout>
        );
    }
}
