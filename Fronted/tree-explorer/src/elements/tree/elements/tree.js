import React , { useState } from 'react'
import Provider from '../controller/provider';
import Branch from '../forms/breach_form'


class Tree extends React.Component {
  constructor(props){
    super(props)
    this.state = {
      show: true
    };
    this.onChange = this.onChange.bind(this);
    
  }



  onChange(){
   if(Provider.show == true){
    this.setState({show : false})
    Provider.show = false;
   }

   this.setState({show : true})
  }

  render() {
    return (
    <React.Fragment>
      {this.state.show ? <Branch id={0} tree_calback={this.onChange}/> : ""}
    </React.Fragment>
    );
  }
}

export default Tree;
