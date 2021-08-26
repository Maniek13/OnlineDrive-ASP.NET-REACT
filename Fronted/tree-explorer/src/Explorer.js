import Tree from './elements/tree'
import React from 'react';
import Element from './elements/tree/objects/element';
import Usser from './elements/account_login/objects/usser';
import AccountLogin from './elements/account_login';
import AccountController from './controllers/account/account_controller';
import Responde from './objects/responde';
import Menu from './elements/menu';

class Explorer  extends React.Component {
  constructor(props){
    super(props)
    
    this.state = {
      loged: false
    };

    this.login = this.login.bind(this);
  }

  async componentDidMount(){
    await AccountController.is_saved();
      if(Responde.code === 200 && Responde.data !== 0){
        Element.element.UsserId = Responde.data.id;
        Element.element.Name = Responde.data.name;
        this.setState({loged : true});
      }
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
      </React.Fragment> : <AccountLogin login_callback={this.login} />
    );
  }
}

export default Explorer;
