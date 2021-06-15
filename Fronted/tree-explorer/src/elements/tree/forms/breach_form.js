import React from 'react'
import List from '../objects/list'
import Empty from '../elements/empty'
import File from '../elements/file'
import Folder from '../elements/folder'

class Branch extends React.Component{
  constructor(props) {
    super(props);
    this.state = {
      add: false
    };
    this.tree_calback = this.props.tree_calback.bind(this);
  }

  show(){
    let fields = [];

    if(List.tree.length === 0){
      fields.push(<Empty tree_calback = {this.tree_calback} id={this.props.id} key={"empty"}/> )
    }
    else{
      List.tree.forEach(el => {
        if(el.idW === this.props.id){
          switch(el.type){
            case "file":
              fields.push(<File tree_calback = {this.tree_calback} id={el.id} name={el.name} idW={el.idW} fileType={el.type} key={el.id}/>)
              break;
            case "node":
              fields.push(<Folder tree_calback = {this.tree_calback} id={el.id} name={el.name} idW={el.idW} fileType={el.type} key={el.id}/>)
              break;
          }
        }
      });

      fields.push(<Empty tree_calback = {this.tree_calback} id={this.props.id} key={"empty"}/>)
    }
    
    return fields;
  } 
        

  render() {
      return (
        <React.Fragment>
          {this.show()}
        </React.Fragment>
      );
    }
}

export default Branch;

