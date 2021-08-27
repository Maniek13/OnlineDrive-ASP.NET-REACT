import React from 'react'
import List from '../objects/list'
import Empty from '../elements/empty'
import File from '../elements/file'
import Folder from '../elements/folder'
import styles from '../styles/tree.module.css'
import TreeController from '../../../controllers/tree/tree_controller'
import Responde from '../../../objects/responde'

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
      if(Responde.code === 200){
        this.setState({sortType : !this.state.sortType})
      }
  }

  show(){
    let fields = [];
    if(this.id == 0){
      fields.push(<div className={styles.sort_bar} key={"sort"}> <div className={styles.bar_name}>Online drive</div> <div className={styles.sort_btn_cont} ><button className={styles.sort} onClick={this.sortBranch.bind(this)}>&uarr;&darr;</button></div></div>);
    }
    else{
      fields.push(<div className={styles.sort_bar} key={"sort"}><button className={styles.sort} onClick={this.sortBranch.bind(this)}>&uarr;&darr;</button></div>);
    }
    
    if(List.tree.length === 0){
      fields.push(<Empty tree_calback = {this.tree_calback} id={this.id} key={"empty"}/> )
    }
    else{
      List.tree.forEach(el => {
        if(el.idW === this.id){
          switch(el.type){
            case "file":
              fields.push(<File tree_calback = {this.tree_calback} id={el.id} name={el.name} idw={el.idW} key={el.id}/>)
              break;
            case "node":
              fields.push(<Folder tree_calback = {this.tree_calback} id={el.id} name={el.name} idw={el.idW} key={el.id}/>)
              break;
            default:
              break;
          }
        }
      });
      if(this.id === 0){
        fields.push(<Empty tree_calback = {this.tree_calback} id={this.id} key={"empty"}/>)
      }
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

