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
    console.log(List.tree)
    if(List.tree.length === 0){
      return <Empty id={0}/> }
    else{
      let fields = [];

      List.tree.forEach(el => {
        if(el.idW === 0){

          switch(el.type){
            case "file":
                fields.push(<File name={el.name}/>)
              break;
            case "node":
                fields.push(<Folder id={el.id} name={el.name}/>)
              break;
          }
        }
      });

      fields.push(<Empty id={0}/>)
      console.log(fields)
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
