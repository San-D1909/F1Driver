import React, { Component } from 'react';
import axios from 'axios';
import "./CSS/Cards.css";
import "./CSS/Login.css";
import Button from 'reactstrap/lib/Button';
import Card from 'reactstrap/lib/Card';
import CardBody from 'reactstrap/lib/CardBody';
import Form from 'reactstrap/lib/Form';
import Label from 'reactstrap/lib/Label';
import { Link, Redirect } from 'react-router-dom';
import { Input } from 'reactstrap';

export class Register extends Component {
    static displayName = Register.name;

    constructor(props) {
        super(props)
        this.state = {
            email: '',
            password: '',
            username: '',
            token: '',
            hasError: false,
            errorMessage: '',
            loggedIn: false
        }
        this.RegisterEvent = this.RegisterEvent.bind(this)
    }

    setSession = (token) => {
        localStorage.setItem("loggedin", true);
        localStorage.setItem("token", token.data);
        this.setState({ token: token.data, loggedIn: true });
    }

    RegisterEvent = (event) => {
        console.log(test)
        event.preventDefault();
        var self = this;
        var user = {
            email: this.state.email,
            password: this.state.password,
            username: this.state.username,
        };
        this.setState({ hasError: false, errorMessage: '' })
        if (user.email === '' || user.email === null) {
            this.setState({ hasError: true, errorMessage: "Field 'email' must be filled in!" })
            console.log(this.state.haserror);
            return;
        } else if (user.password === '' || user.password === null) {
            this.setState({ hasError: true, errorMessage: "Field 'password' must be filled in!" })
            return;
        } else if (user.username === '' || user.username === null) {
            this.setState({ hasError: true, errorMessage: "Field 'username' must be filled in!" })
            return;
        }
        axios({
            method: 'post',
            url: 'https://localhost:44378/UserAuth/Register/RegisterFunction',
            dataType: "json",
            data: user
        }).then(token => this.setSession(token)).catch(function (error) {
            if (error.message == "Request failed with status code 401") {
                console.log(error.message)
            }
            self.setState({ hasError: true, errorMessage: "Username or Password is incorrect." })
            return;
        });
    }
    render() {
        return (
            <Card>
                <CardBody>
                    <h1 className="text-center">RegisterForm</h1>
                    <div className="col-12">
                        <Form>
                            {this.state.hasError && (
                                <div className="py-2 col-12">
                                    <Label className="alert alert-danger col-12" role="alert">{this.state.errorMessage}</Label>
                                </div>
                            )}
                            <div className="py-2">
                                <Label for="email">Email</Label>
                                <Input type="text" onChange={(e) => this.setState({ email: e.target.value })} name="email" />
                            </div>
                            <div className="py-2">
                                <Label for="username">UserName</Label>
                                <Input type="username" onChange={(e) => this.setState({ username: e.target.value })} name="username" />
                            </div>
                            <div className="py-2">
                                <Label for="password">Password</Label>
                                <Input type="password" onChange={(e) => this.setState({ password: e.target.value })} name="password" />
                            </div>
                            <div className="py-2">
                                <Button className="my-2 mr-2 ml-0" onClick={(e) => this.RegisterEvent(e)}>Register</Button>
                            </div>
                        </Form>
                    </div>
                </CardBody>
            </Card>
        );
    }
}
