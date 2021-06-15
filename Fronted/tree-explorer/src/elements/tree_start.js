import React from 'react'
import Responde from './controllers/http/objects/responde'
import Tree from './tree/elements/tree'
import Error from './tree/elements/error'
import TreeController from './controllers/tree/tree_controller'

class Start extends React.Component {
  constructor(props){
    super(props)
    
    this.state = {
      loaded: false,
      error: false
    };
  }

  async componentDidMount(){
    await TreeController.get_tree();
      if(typeof Responde.data.error == 'undefined'){
        this.setState({error : false});
      }
      else{
        this.setState({error : true});
      }

    this.setState({loaded : true})  
  }


  render() {
    return (
      <React.Fragment >
        {this.state.loaded ?  "" : "loading"}
        {this.state.error === false ? <Tree/> : this.state.error === true ? <Error/> : ""}
      </React.Fragment>
    );
  }
}

export default Start;