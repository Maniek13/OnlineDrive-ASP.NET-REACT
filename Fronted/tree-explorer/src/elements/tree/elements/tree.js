import React from 'react'
import List from '../objects/list'
import Folder from './folder'
import Empty from './empty'
import File from './file'


class Tree extends React.Component {
  constructor(props){
    super(props)
    
    this.state = {
      add: false
    };
  }

  show(){
    if(List.tree.length === 0){
      return <Empty id={0} key={"empty"}/> }
    else{
      let fields = [];

      List.tree.forEach(el => {
        if(el.idW === 0){

          switch(el.type){
            case "file":
                fields.push(<File id={el.id} name={el.name} idW={el.idW} fileType={el.type} key={el.id}/>)
              break;
            case "node":
                fields.push(<Folder id={el.id} name={el.name} idW={el.idW} fileType={el.type} key={el.id}/>)
              break;
          }
        }
      });

      fields.push(<Empty id={0} key={"empty"}/>)
      return fields;
    } 
  }

  render() {
    return (
      <React.Fragment>
          {this.show()}
      </React.Fragment>
      
    );
  }
}

export default Tree;
