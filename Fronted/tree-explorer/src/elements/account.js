import React from 'react'
import Start from './account/account_start'

class Account extends React.Component {
  constructor(props){
    super(props)

    this.login_callback = this.props.login_callback.bind(this);
  }

  render() {
    return (
      <Start login_callback={this.login_callback}/>
    );
  }
}

export default Account;