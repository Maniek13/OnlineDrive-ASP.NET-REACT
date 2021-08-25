import React from 'react'
import Responde from '../controllers/http/objects/responde'
import Tree from './elements/tree'
import Error from './elements/error'
import TreeController from '../controllers/tree/tree_controller'

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
    if(Responde.code === 1){
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
        {this.state.error === false && this.state.loaded === true ? <Tree/> : this.state.loaded === true && this.state.error === true ? <Error/> : "loading"}
      </React.Fragment>
    );
  }
}

export default Start;