import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { DriverStandings } from './components/DriverStandings';
import { ConstructorStandings } from './components/ConstructorStandings';

import './custom.css'

export default class App extends Component {
    static displayName = App.name;



    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route exact path='/DriverStandings' component={DriverStandings} />
                <Route exact path='/ConstructorStandings' component={ConstructorStandings} />
            </Layout>
        );
    }
}
