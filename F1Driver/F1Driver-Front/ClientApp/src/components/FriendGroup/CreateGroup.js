import React, { Component } from 'react';
import axios from 'axios';
import Button from 'reactstrap/lib/Button';
import Card from 'reactstrap/lib/Card';
import CardBody from 'reactstrap/lib/CardBody';
import Form from 'reactstrap/lib/Form';
import Label from 'reactstrap/lib/Label';
import { Input } from 'reactstrap';

export class CreateGroup extends Component {
    static displayName = CreateGroup.name;

    constructor(props) {
        super(props)
        this.state = {
            user: [],
            groupName: '',
        }
    }

    componentDidMount() {
        if (!localStorage.getItem("token")) {
            return (
                window.location.assign("../login")
            )
        }
        this.GetUser();
    }


    createGroup = async () => {
        var self = this;
        var FriendGroup = {
            GroupName: this.state.groupName,
        }
        var userAndGroupDTO = {
            User: this.state.user,
            FriendGroup: FriendGroup,
        }
        var User = this.state.user
        console.log(userAndGroupDTO)
        axios({
            method: 'POST',
            url: 'https://localhost:44322/Group/CreateGroup/CreateGroup',
            dataType: "json",
            data: userAndGroupDTO,
        }).then(function (data) {
            console.log(data.data);
            self.setState({});
        });
    }
    GetUser = async () => {
        var self = this;
        axios({
            method: 'GET',
            url: 'https://localhost:44378/UserAuth/GetUserByToken/GetUserByToken',
            params: {
                token: localStorage.getItem("token"),
            }
        }).then(function (data) {
            console.log(data.data);
            self.setState({ user: data.data });
        });
    }
    render() {
        return (
            <Card>
                <CardBody>
                    <h1 className="text-center">Create a group</h1>
                    <div className="col-12">
                        <Form>
                            <div className="py-2">
                                <Label for="groupName">Groupname</Label>
                                <Input type="text" onChange={(e) => this.setState({ groupName: e.target.value })} name="groupName" />
                            </div>
                            <div className="py-2">
                                <Button className="my-2 mr-2 ml-0" onClick={(e) => this.createGroup(e)}>Create</Button>
                            </div>
                        </Form>
                    </div>
                </CardBody>
            </Card>
        );
    }
}