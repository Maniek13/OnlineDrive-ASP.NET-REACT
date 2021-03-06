import React from 'react'
import List from '../objects/list'
import Empty from '../elements/empty'
import File from '../elements/file'
import Folder from '../elements/folder'
import styles from '../styles/tree.module.css'
import TreeController from '../../../controllers/tree/tree_controller'
import Responde from '../../../objects/responde'
import ClickAwayListener from 'react-click-away-listener'

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

  close(evt){
    this.props.show();
  }

  show(){
    let fields = [];
    let branch = [];
    if(this.id === 0){
      fields.push(<div className={styles.sort_bar} key={"sort"}> <div className={styles.bar_name}>Online drive</div> <div className={styles.sort_btn_cont} ><button className={styles.sort} onClick={this.sortBranch.bind(this)}></button></div></div>);
    }
    else{
      if(List.tree.find(el => el.id === this.id) !== undefined){
        let name = List.tree.find(el => el.id === this.id).name;  
        fields.push(
        <div className={styles.sort_bar} key={"sort"}>
          <div className={styles.bar_name}>{name}</div>
          <button className={styles.sort} onClick={this.sortBranch.bind(this)}></button>
          <div  className={styles.exit} onClick={this.close.bind(this)}></div>
        </div>
        );
      }
    }
    
    if(List.tree.length === 0){
      fields.push(<Empty tree_calback = {this.tree_calback} id={this.id} key={"empty"}/> )
    }
    else{
      let i = 0;
      List.tree.forEach(el => {
        if(el.idW === this.id){
          i++;
          switch(el.type){
            case "file":
              fields.push(<File tree_calback = {this.tree_calback} id={el.id} name={el.name} idw={el.idW} key={el.id}/>)
              break;
            case "node":
              fields.push(<Folder tree_calback = {this.tree_calback} id={el.id} name={el.name} idw={el.idW} key={el.id} />)
              break;
            default:
              break;
          }
        }
      }
      );
      if(i === 0){
        fields.push(<div>No elements</div>)
      }

      if(this.id === 0){
        fields.push(<Empty tree_calback = {this.tree_calback} id={this.id} key={"empty"}/>)
      }
    }

    let el = <div  className={styles.single_branch} key={"sb" + this.id}> {fields} </div>;
    if(List.tree.find(el => el.id === this.id) !== undefined){
      branch.push(<ClickAwayListener onClickAway={this.close.bind(this)} >{el}</ClickAwayListener> );
    }
    else{
      branch.push(el);
    }
    
    return branch;
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

