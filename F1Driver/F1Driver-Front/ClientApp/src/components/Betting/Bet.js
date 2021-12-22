import React, { Component } from 'react';
import axios from 'axios';
import Card from 'reactstrap/lib/Card';
import CardBody from 'reactstrap/lib/CardBody';
import Form from 'reactstrap/lib/Form';
import Label from 'reactstrap/lib/Label';
import { Input } from 'reactstrap';


export class Bet extends Component {
    static displayName = Bet.name;

    constructor(props) {
        super(props)

        this.state = {
            message: '',
        }
    }
    render() {
        if (!localStorage.getItem("token")) {
            return (
                window.location.assign("login")
            )
        }
        return (
            <Card>
                {this.state.message.length !== 0 &&
                    <div className="alert alert-warning" >
                        <strong>{this.state.message}</strong>
                    </div>
                }
                <CardBody>
                    <button style={{ margin: 10 }} className="btn btn-danger" onClick={(e) => window.location.assign("Group")}>Grouppage</button>
                </CardBody>
            </Card>
        );
    }
}