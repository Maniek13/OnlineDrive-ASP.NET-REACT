import React from 'react'
import styles from '../styles/tree.module.css'
import AddForm from '../forms/add_form'
import EditForm from '../forms/edit_form'
import DelForm from '../forms/del_form'
import Element from '../objects/element'
import Provider from '../controller/provider'
import Branch from '../forms/branch_form'

class Folder extends React.Component {
  constructor(props){
    super(props)
    
    this.state = {
      add: false,
      edit: false,
      delete: false,
      show: false
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
      Element.element.IdW = this.props.idw;
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

  onAdd(){
    Provider.modal = false;
    this.setState({add : false});
    this.showBranch();
  }

  onEdit(){
    Provider.modal = false;
    this.setState({edit : false});
  }

  onDelete(){
    Provider.modal = false;
    this.setState({delete : false});
  }

  render() {
    return (
      <React.Fragment>
        <div className={styles.branch} >
          
          <div className={styles.folder_contener} >
            <div className={styles.folder} >
              <p className={styles.name}>{this.props.name}</p>
              <button id={this.props.id}  name={this.props.name} className={styles.del_btn} onClick={this.delForm.bind(this)}></button>  
              <button id={this.props.id} idw={this.props.idw} name={this.props.name} className={styles.edit_btn} onClick={this.editForm.bind(this)}></button> 
              <button className={styles.show_btn} id="show" onClick={this.showBranch.bind(this)}></button>  
            </div>

            <div className={styles.folder_add}>
              <button value={this.props.id}  className={styles.add_btn} onClick={this.addForm.bind(this)}></button>
            </div>

          </div>

          {this.state.show ? <Branch tree_calback = {this.tree_calback} id={this.props.id}  show={this.showBranch.bind(this)}/> : ""}
        </div>

        {this.state.add ? <AddForm  tree_calback = {this.tree_calback} callback = {this.onAdd}/> : ""}
        {this.state.edit ? <EditForm tree_calback = {this.tree_calback} id={this.props.id} idw={this.props.idw} name={this.props.name} node={true} callback = {this.onEdit}/> : ""}
        {this.state.delete ? <DelForm tree_calback = {this.tree_calback}  id={this.props.id} name={this.props.name} callback = {this.onDelete}/> : ""}
      </React.Fragment> 
    );
  }
}

export default Folder;
