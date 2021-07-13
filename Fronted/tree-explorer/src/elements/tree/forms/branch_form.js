import React from 'react'
import List from '../objects/list'
import Empty from '../elements/empty'
import File from '../elements/file'
import Folder from '../elements/folder'
import styles from '../styles/tree.module.css'
import TreeController from '../../controllers/tree/tree_controller'
import Responde from '../../controllers/http/objects/responde'


class Branch extends React.Component{
  constructor(props) {
    super(props);

    this.id = this.props.id;

    this.state = {
      add: false,
      sortType: true
    };
    
    this.tree_calback = this.props.tree_calback.bind(this);
  }

  async sortBranch(){
    await TreeController.sort_brand(this.id, this.state.sortType? "ASC" : "DESC");
      if(Responde.data === true){
        if(Responde.code === 1){
          this.setState({sortType : !this.state.sortType})
        }
      }
  }

  show(){
    let fields = [];
    if(List.tree.length === 0){
      fields.push(<Empty tree_calback = {this.tree_calback} id={this.id} key={"first"}/> )
    }
    else{
      fields.push(<button className={styles.sort} key={this.id} onClick={this.sortBranch.bind(this)}>&uarr;&darr;</button>);
      List.tree.forEach(el => {
        if(el.idW === this.id){
          switch(el.type){
            case "file":
              fields.push(<File tree_calback = {this.tree_calback} id={el.id} name={el.name} idW={el.idW} key={el.id}/>)
              break;
            case "node":
              fields.push(<Folder tree_calback = {this.tree_calback} id={el.id} name={el.name} idW={el.idW} key={el.id}/>)
              break;
            default:
              break;
          }
        }
      });
      fields.push(<Empty tree_calback = {this.tree_calback} id={this.id} key={"last"}/>)
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

