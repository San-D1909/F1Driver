import React, { Component } from 'react';
import axios from 'axios';
import Card from 'reactstrap/lib/Card';
import CardBody from 'reactstrap/lib/CardBody';
import Form from 'reactstrap/lib/Form';
import Label from 'reactstrap/lib/Label';
import { Input } from 'reactstrap';

function join(parameter) {
    Group.setState({ groupName: parameter })
    Group.JoinGroup()
}

export class Group extends Component {
    static displayName = Group.name;

    constructor(props) {
        super(props)

        this.state = {
            user: [],
            friendGroup: '',
            groupName: '',
            searchString: '',
            invite: false,
            message: '',
            error: false,
            notifications: [],
        }
    }
    componentDidMount() {
        if (!localStorage.getItem("token")) {
            return (
                window.location.assign("../login")
            )
        }
        this.GetUser()
    }
    componentDidUpdate() {
        console.log(this.state.friendGroup);
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
            self.setState({ user: data.data });
            self.GetNotifications();
        });
    }

    JoinGroup = (event) => {
        var userAndGroupDTO = {
            groupID: this.state.friendGroup,
            User: this.state.user,
            FriendGroup: null,
            searchString: null
        }
        var self = this;
        console.log(userAndGroupDTO)
        console.log(this.state.friendGroup)
        axios({
            method: 'POST',
            url: 'http://localhost:5001/Group/JoinGroup/JoinGroup',
            dataType: "json",
            data: userAndGroupDTO,
        }).then(function (data) {
            if (data.data == false) {
                self.setState({ message: 'Inviting the user failed' });
            }
            else {
                self.setState({ message: 'User invited' });
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
                self.setState({ message: 'Inviting the user failed' });
            }
            else {
                self.setState({ message: 'User invited' });
            }
        });
    }

    GetNotifications = async => {
        var self = this;
        axios({
            method: 'POST',
            url: 'http://localhost:5001/Notification/GetNotifications/GetNotifications',
            params: {
                notificationType: "Group_inv",
                userID: this.state.user.id
            }
        }).then(function (data) {
            console.log(data.data)
            self.setState({ notifications: data.data })
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
                <body>
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
                            <hr class="solid"></hr>
                            {this.state.notifications.map(notification =>
                                <tr key={notification.id}>
                                    <td>ID: {notification.id}</td>
                                    <td>Message: {notification.notification}</td>
                                    <button className="my-2 mr-2 ml-0" onClick={(e) => { this.setState({ friendGroup: notification.friendGroup }); this.JoinGroup(); }}>Accept invite</button>
                                </tr>
                            )}
                        </CardBody>
                    </Card>
                </body>
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