import React, { Component } from 'react';
import axios from 'axios';
import Card from 'reactstrap/lib/Card';
import CardBody from 'reactstrap/lib/CardBody';
import Form from 'reactstrap/lib/Form';
import Label from 'reactstrap/lib/Label';
import { Input } from 'reactstrap';

export class Group extends Component {
    static displayName = Group.name;

    constructor(props) {
        super(props)

        this.state = {
            user: [],
            test: '',
            groupName: '',
            noGroup: false,
            searchString: '',
            invite: false,
            message: '',
            error: false,
        }
    }
    componentDidMount() {
        if (!localStorage.getItem("token")) {
            return (
                window.location.assign("../login")
            )
        }
        this.GetUser();
        this.GetNotifications();
    }


    GetUser = async () => {
        var self = this;
        axios({
            method: 'GET',
            url: 'http://localhost:5000/UserAuth/GetUserByToken/GetUserByToken',
            params: {
                token: localStorage.getItem("token"),
            }
        }).then(function (data) {
            console.log(data.data);
            self.setState({ user: data.data, test: "ok" });
            if (data.data.friendGroup === '0' || data.data.friendGroup == null) {
                self.setState({ noGroup: true });
            }
        });
    }

    JoinGroup = (event) => {
        var self = this;
        var userAndGroupDTO = {
            User: this.state.user,
            FriendGroup: null,
            searchString: this.state.searchString,
        }
        axios({
            method: 'POST',
            url: 'http://localhost:5001/Group/JoinGroup/JoinGroup',
            dataType: "json",
            data: userAndGroupDTO,
        }).then(function (data) {

            if (data.data == false) {
                self.setState({ message: 'Inviting the user failed'});
            }
            else {
                self.setState({ message: 'User invited'});
            }
        });
    }

    InviteToGroup = (event) => {
        var self = this;
        var userAndGroupDTO = {
            User: this.state.user,
            FriendGroup: null,
            searchString: this.state.searchString,
        }
        axios({
            method: 'POST',
            url: 'http://localhost:5001/Group/InviteToGroup/InviteToGroup',
            dataType: "json",
            data: userAndGroupDTO,
        }).then(function (data) {
            if (data.data == false) {
                self.setState({ message: 'Inviting the user failed'});
            }
            else {
                self.setState({ message: 'User invited'});
            }
        });
    }

    GetNotifications = (event) => {
        axios({
            method: 'get',
            url: 'http://localhost:5001/Group/GetNotifications/GetNotifications',
            dataType: "json",
            params: {
                notificationType: "Group_inv",
                userID: this.state.user.ID
            }
        }).then(function (data) {
            if (data.data == false) {
                self.setState({ message: 'Inviting the user failed' });
            }
            else {
                self.setState({ message: 'User invited' });
            }
        });
    }

    createGroup = async () => {
        var FriendGroup = {
            GroupName: this.state.groupName,
        }
        var userAndGroupDTO = {
            User: this.state.user,
            FriendGroup: FriendGroup,
        }
        console.log(userAndGroupDTO)
        axios({
            method: 'POST',
            url: 'http://localhost:5001/Group/CreateGroup/CreateGroup',
            dataType: "json",
            data: userAndGroupDTO,
        }).then(function (data) {
            console.log(data.data);
        });
    }

    changePage = () => {
        this.setState({ invite: !this.state.invite })
    }

    render() {
        if (this.state.user.friendGroup == 0 || this.state.user.friendGroup == null) {
            return (
                <Card>
                    {this.state.message.length !== 0 &&
                        <div className="alert alert-warning" >
                            <strong>{this.state.message}</strong>
                        </div>
                    }
                    <CardBody>
                        <h1 className="text-center">Create a group</h1>
                        <div className="col-12">
                            <Form>
                                <div className="py-2">
                                    <Label for="groupName">Groupname</Label>
                                    <Input type="text" onChange={(e) => this.setState({ groupName: e.target.value })} name="groupName" />
                                </div>
                                <div className="py-2">
                                    <button className="my-2 mr-2 ml-0" onClick={(e) => this.createGroup(e)}>Create</button>
                                </div>
                            </Form>
                        </div>
                    </CardBody>
                </Card>
            );
        }
        else {
            if (this.state.invite) {
                return (
                    <body>
                        {this.state.message.length !== 0 &&
                            < div className="alert alert-warning" >
                                <strong>{this.state.message}</strong>
                            </div >
                        }
                        <div>
                            <h1 style={{ color: "white" }}>Invite a friend by email</h1>
                            <button onClick={this.changePage}>Back to group</button>
                        </div>
                        <Card>
                            <CardBody>
                                <div >
                                    <input onChange={(e) => this.setState({ searchString: e.target.value })} placeholder="Insert an emailadress"></input>
                                    <button onClick={this.InviteToGroup}>Invite</button>
                                </div>
                            </CardBody>
                        </Card>
                    </body>
                );
            }
            return (
                <body>
                    <div>
                        <h1 style={{ color: "white" }}>Welcome to your group</h1>
                        <button onClick={this.changePage}>Inivite friends</button>
                    </div>
                </body >
            );
        }
    }
}