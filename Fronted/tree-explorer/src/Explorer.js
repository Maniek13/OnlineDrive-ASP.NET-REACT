import Tree from './elements/tree'
import React from 'react';
import Element from './elements/tree/objects/element';
import Usser from './elements/account_login/objects/usser';
import AccountLogin from './elements/account_login';

class Explorer  extends React.Component {
  constructor(props){
    super(props)
    
    this.state = {
      loged: false
    };

    this.login = this.login.bind(this);
  }

  login(){
    Element.element.UsserId = Usser.id.Id;
    this.setState({loged : true})
  }
  
  render(){
    return (
      this.state.loged === true ? <Tree/> : <AccountLogin login_callback={this.login} />
    );
  }
}

export default Explorer;
