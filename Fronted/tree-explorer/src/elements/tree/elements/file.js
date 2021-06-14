import React from 'react'
import styles from '../styles/tree.module.css'
import EditForm from '../forms/edit_form'
import Element from '../objects/element'
import DelForm from '../forms/del_form'
import Provider from '../controller/provider'

class File extends React.Component {
  constructor(props){
    super(props)
    
    this.state = {
      edit: false,
      delete : false
    };
    this.onEdit = this.onEdit.bind(this);
    this.onDelete = this.onDelete.bind(this);
  }

  
  editForm(evt){
    if(Provider.modal == false){
      Provider.modal = true;
      if(this.state.edit === false){
        Element.element.Id = evt.target.id;
        Element.element.IdW = evt.target.idW;
        Element.element.Type = evt.target.fileType;
        Element.element.Name = evt.target.name;
        this.setState({edit : true});
      } 
    }
  }

  delForm(evt){
    if(Provider.modal == false){
      Provider.modal = true;
      if(this.state.delete === false){
        this.setState({delete : true});
      } 
    } 
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
        <div className={styles.file}>
          <a className={styles.label}>{this.props.name}</a>
                    <button id={this.props.id}  name={this.props.name} className={styles.del_btn} onClick={this.delForm.bind(this)}>-</button>
                    <button id={this.props.id} idW={this.props.idW} name={this.props.name} fileType={this.props.fileType}  className={styles.edit_btn} onClick={this.editForm.bind(this)}>edit</button>
        </div>
        {this.state.edit ? <EditForm idW={this.props.idW} name={this.props.name} node={false} callback = {this.onEdit}/> : ""}
        {this.state.delete ? <DelForm id={this.props.id} name={this.props.name} callback = {this.onDelete}/> : ""}
      </React.Fragment>
    );
  }
}

export default File;
