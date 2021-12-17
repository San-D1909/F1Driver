import React, { Component } from 'react';
import axios from 'axios';
import Card from 'reactstrap/lib/Card';
import CardBody from 'reactstrap/lib/CardBody';
import Form from 'reactstrap/lib/Form';
import Label from 'reactstrap/lib/Label';
import { Input } from 'reactstrap';
import "../CSS/Grouppage.css";


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
            group: [],
            users: [],
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
            if (data.data.friendGroup != null) {
                self.GetGroupDetails(data.data.friendGroup)
            }
        });
    }

    JoinGroup = (notification) => {
        var userAndGroupDTO = {
            groupID: notification.friendGroup,
            User: this.state.user,
        }
        var self = this;
        axios({
            method: 'POST',
            url: 'http://localhost:5001/Group/JoinGroup/JoinGroup',
            dataType: "json",
            data: userAndGroupDTO,
        }).then(function (data) {
            if (data.data == false) {
                self.setState({ message: 'Joining group failed, group might not exist!' });
            }
            else {
                self.setState({ message: 'Success' });
                self.DeleteNotification(notification.id)
                window.location.assign("../Group")
            }
        });
    }

    LeaveGroup = (userID) => {
        if (userID == "") {
            userID = this.state.user.id
        }
        console.log(userID)
        var self = this;
        axios({
            method: 'POST',
            url: 'http://localhost:5001/Group/LeaveGroup/LeaveGroup',
            params: {
                userID: userID
            }
        }).then(function (data) {
            window.location.assign("../Group")
        });
    }

    DeleteNotification = (event) => {
        var notificationID = event
        var self = this;
        axios({
            method: 'POST',
            url: 'http://localhost:5001/Notification/DeleteNotification/DeleteNotification',
            params: {
                notificationID: notificationID
            }
        }).then(function (data) {
            if (data.data == false) {
                self.setState({ message: 'Could not delete invite' });
            }
            else {
                self.setState({ message: 'Success' });
                window.location.assign("../Group")
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

    GetGroupDetails = (group) => {
        var self = this;
        axios({
            method: 'POST',
            url: 'http://localhost:5001/Group/GetGroupDetails/GetGroupDetails',
            params: {
                groupID: group
            }
        }).then(function (data) {
            console.log(data.data)
            self.setState({ group: data.data, users: data.data.users })
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
        this.setState({ invite: !this.state.invite, message: "" })
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
                                    <Label style={{ fontWeight: 'bold' }} for="groupName">Groupname</Label>
                                    <Input className="input-group-text" type="text" onChange={(e) => this.setState({ groupName: e.target.value })} name="groupName" />
                                </div>
                                <div className="py-2">
                                    <button className="btn btn-danger" onClick={(e) => this.createGroup(e)}>Create</button>
                                </div>
                            </Form>
                        </div>
                        <h4 className="text-center">Messages</h4>
                        <table className='table table-striped' aria-labelledby="tabelLabel">
                            {this.state.notifications.map(notification =>
                                <tr key={notification.id}>
                                    <td>{notification.notification}</td>
                                    <td style={{ cursor: 'pointer', fontWeight: 'bold' }} onClick={(e) => this.JoinGroup(notification)}>Accept invite</td>
                                    <td style={{ cursor: 'pointer', fontWeight: 'bold' }} className="my-2 mr-2 ml-0" onClick={(e) => this.DeleteNotification(notification.id)}>X</td>
                                </tr>
                            )}
                        </table>
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
                        <h1 style={{ color: "white" }}>Invite a friend by email</h1>
                        <Card>
                            <CardBody>
                                <div>
                                    <button className="btn btn-danger" onClick={this.changePage}>Back to group</button>
                                </div>
                                <div >
                                    <input onChange={(e) => this.setState({ searchString: e.target.value })} placeholder="Insert an emailadress"></input>
                                    <button className="btn btn-danger" onClick={this.InviteToGroup}>Invite</button>
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
                        <Card>
                            <CardBody>
                                <button className="btn btn-danger" onClick={this.changePage}>Invite friends</button>
                                <button style={{ margin: 10 }} className="btn btn-danger" onClick={(e) => this.LeaveGroup("")}>Leave this group</button>
                                <table className='table table-striped' aria-labelledby="tabelLabel">
                                    <tbody>
                                        <tr>
                                            <th>Username</th>
                                            <th>Pool score</th>
                                            <th>FavoriteDriver</th>
                                            <th>Remove from group</th>
                                        </tr>
                                        {this.state.users.map(user =>
                                            <tr key={user.id}>
                                                <td>{user.userName}</td>
                                                <td>{user.bettingScore}</td>
                                                <td>{user.favoriteDriver}</td>
                                                <td style={{ cursor: 'pointer', fontWeight: 'bold' }} className="my-2 mr-2 ml-0" onClick={(e) => this.LeaveGroup(user.id)}>X</td>
                                            </tr>
                                        )}
                                    </tbody>
                                </table>
                            </CardBody>
                        </Card>
                    </div>
                </body >
            );
        }
    }
}