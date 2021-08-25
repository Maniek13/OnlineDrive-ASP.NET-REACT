import React from 'react'
import Form from './elements/form'

class Start extends React.Component {
  constructor(props){
    super(props)

    this.login_callback = this.props.login_callback.bind(this);
  }

  render() {
    return (
      <Form login_callback={this.login_callback}/>
    );
  }
}

export default Start;