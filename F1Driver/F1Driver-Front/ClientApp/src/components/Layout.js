import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;

    /*--------------------
    Items
    --------------------*/
    items = [
        {
            name: "Freelance",
            color: "#f44336",
            href: "#"
        },
        {
            name: "Design",
            color: "#e91e63",
            href: "#"
        },
        {
            name: "Director",
            color: "#9c27b0",
            href: "#"
        },
        {
            name: "Experience",
            color: "#673ab7",
            href: "#"
        },
        {
            name: "Interface",
            color: "#3f51b5",
            href: "#"
        }
    ];

  render () {
    return (
      <div>
            <NavMenu items={ this.items }/>
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
