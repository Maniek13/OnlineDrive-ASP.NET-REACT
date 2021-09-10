import Tree from './elements/tree'
import React from 'react'
import Element from './elements/tree/objects/element'
import Usser from './elements/account_login/objects/usser'
import AccountLogin from './elements/account_login'
import Menu from './elements/menu'

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
      this.state.loged === true ? 
      <React.Fragment>
        <Menu/>
        <Tree/>
      </React.Fragment> :
      <AccountLogin login_callback={this.login} />
    );
  }
}

export default Explorer;
