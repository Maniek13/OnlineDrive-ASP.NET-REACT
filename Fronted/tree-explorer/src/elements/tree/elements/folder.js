import React from 'react'
import styles from '../styles/tree.module.css'
import AddForm from '../forms/add_form'
import EditForm from '../forms/edit_form'
import DelForm from '../forms/del_form'
import Element from '../objects/element'
import Provider from '../controller/provider'
import Branch from '../forms/branch_form'
import TreeController from '../../controllers/tree/tree_controller'
import Responde from '../../controllers/http/objects/responde'

import List from '../objects/list'


class Folder extends React.Component {
  constructor(props){
    super(props)
    
    this.state = {
      add: false,
      edit: false,
      delete: false,
      show: false,
      sortType: true
    };

    this.onAdd = this.onAdd.bind(this);
    this.onEdit = this.onEdit.bind(this);
    this.onDelete = this.onDelete.bind(this);
    this.tree_calback = this.props.tree_calback.bind(this);
  }

  addForm(evt){
    if(Provider.modal === false){
      Provider.modal = true;
      if(this.state.add === false){
          Element.element.IdW = evt.target.value;
          this.setState({add : true});
      } 
    }
  }

  editForm(evt){
    if(Provider.modal === false){
      Provider.modal = true;
      Element.element.Id = this.props.id;
      Element.element.IdW = this.props.idW;
      Element.element.Name = this.props.name;
      if(this.state.edit === false){
          this.setState({edit : true});
      } 
    }
  }

  delForm(evt){
    if(Provider.modal === false){
      Provider.modal = true;
      if(this.state.delete === false){
        this.setState({delete : true});
      } 
    }
  }

  showBranch(){
    this.setState({show : !this.state.show});
  }

  async sortBranch(){
    await TreeController.sort_brand(this.props.id, this.state.sortType? "ASC" : "DESC");
      if(Responde.data === true){
        if(typeof Responde.data.Error == 'undefined'){
          this.setState({show : true})
          this.setState({sortType : !this.state.sortType})
        }
        console.log(List.tree);
      }
  }

  onAdd(){
    Provider.modal = false;
    this.setState({add : false});
    this.showBranch();
  }

  onEdit(){
    Provider.modal = false;
    this.setState({edit : false});
    this.showBranch();
  }

  onDelete(){
    Provider.modal = false;
    this.setState({delete : false});
    this.showBranch();
  }

  render() {
    return (
      <React.Fragment>
        <div className={styles.branch}>
          
          <div className={styles.folder_contener}>
            <div className={styles.folder} >
              <a className={styles.label}>{this.props.name}</a>
              <button id={this.props.id}  name={this.props.name} className={styles.del_btn} onClick={this.delForm.bind(this)}>x</button>
              <button className={styles.show_btn} onClick={this.showBranch.bind(this)}>&lsaquo;&rsaquo;</button>  
              <button className={styles.show_btn} onClick={this.sortBranch.bind(this)}>&uarr;&darr;</button>  
              <button id={this.props.id} idW={this.props.idW} name={this.props.name} fileType={this.props.fileType}  className={styles.edit_btn} onClick={this.editForm.bind(this)}></button> 
            </div>

            <div className={styles.folder_add}>
              <button value={this.props.id}  className={styles.add_btn} onClick={this.addForm.bind(this)}>+</button>
            </div>

          </div>

          {this.state.show ? <Branch tree_calback = {this.tree_calback} id={this.props.id} /> : ""}
        </div>

        {this.state.add ? <AddForm  tree_calback = {this.tree_calback} callback = {this.onAdd}/> : ""}
        {this.state.edit ? <EditForm tree_calback = {this.tree_calback} id={this.props.id} idW={this.props.idW} name={this.props.name} node={true} callback = {this.onEdit}/> : ""}
        {this.state.delete ? <DelForm tree_calback = {this.tree_calback}  id={this.props.id} name={this.props.name} callback = {this.onDelete}/> : ""}
      </React.Fragment> 
    );
  }
}

export default Folder;
