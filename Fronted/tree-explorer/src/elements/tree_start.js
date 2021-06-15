import React from 'react'
import Responde from './controllers/http/objects/responde'
import Tree from './tree/elements/tree'
import Error from './tree/elements/error'
import GetList from './tree/controller/get_list'

class Start extends React.Component {
  constructor(props){
    super(props)
    
    this.state = {
      loaded: false
    };
  }

  async componentDidMount(){
    await GetList();

    if(typeof Responde.data.Error == 'undefined'){
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