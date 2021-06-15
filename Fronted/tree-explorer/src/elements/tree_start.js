import React from 'react'
import GET from './controllers/http/get'
import Responde from './controllers/http/objects/responde'
import List from './tree/objects/list'
import Tree from './tree/elements/tree'

class Start extends React.Component {
  constructor(props){
    super(props)
    
    this.state = {
      loaded: false
    };
  }

  async componentDidMount(){
    await GET("https://localhost:5001/Elements/Show");
      if(typeof Responde.data.Error == 'undefined'){
        List.tree = Responde.data;
      }
      else{
        Responde.code = 200;
        Responde.data = Responde.data.Error;
      }
      
    this.setState({loaded : true})  
  }


  render() {
    return (
      <React.Fragment >
        {this.state.loaded ? <Tree/> : "loading"}
      </React.Fragment>
    );
  }
}

export default Start;