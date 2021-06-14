import React from 'react'
import styles from '../styles/tree.module.css'
import AddForm from '../forms/add_form'
import EditForm from '../forms/edit_form'
import Element from '../objects/element'


class Folder extends React.Component {
  constructor(props){
    super(props)
    
    this.state = {
      add: false,
      edit: false
    };

    this.onAdd = this.onAdd.bind(this);
    this.onEdit = this.onEdit.bind(this);
  }

  addForm(evt){
    if(this.state.add === false){
        Element.element.IdW = evt.target.value;
        this.setState({add : true});
    } 
  }

  editForm(evt){
    if(this.state.edit === false){
      Element.element.Id = evt.target.id;
      Element.element.IdW = evt.target.idW;
      Element.element.Type = evt.target.fileType;
      Element.element.Name = evt.target.name;
        this.setState({edit : true});
    } 
  }

  onAdd(){
    this.setState({add : false});
  }

  onEdit(){
    this.setState({edit : false});
  }

  render() {
    return (
      <React.Fragment>
        <div className={styles.folder} >
            <a className={styles.label}>{this.props.name}</a>
              <button value={this.props.id}  className={styles.add_btn} onClick={this.addForm.bind(this)}>+</button>
              <button id={this.props.id} idW={this.props.idW} name={this.props.name} fileType={this.props.fileType}  className={styles.edit_btn} onClick={this.editForm.bind(this)}>edit</button>
            {this.state.add ? <AddForm callback = {this.onAdd}/> : ""}
        </div>
         {this.state.edit ? <EditForm idW={this.props.idW} name={this.props.name} node={true} callback = {this.onEdit}/> : ""}
      </React.Fragment> 
    );
  }
}

export default Folder;
