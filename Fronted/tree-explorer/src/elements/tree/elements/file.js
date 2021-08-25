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
    this.tree_calback = this.props.tree_calback.bind(this);
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
        <div>
          <div className={styles.file}>
            <p className={styles.name}>{this.props.name}</p>
            <button id={this.props.id}  name={this.props.name} className={styles.del_btn} onClick={this.delForm.bind(this)}>x</button>
            <button id={this.props.id} idw={this.props.idw} name={this.props.name} className={styles.edit_btn} onClick={this.editForm.bind(this)}></button>
          </div>
        </div>
        
        {this.state.edit ? <EditForm tree_calback = {this.tree_calback} idw={this.props.idw} name={this.props.name} node={false} callback = {this.onEdit}/> : ""}
        {this.state.delete ? <DelForm tree_calback = {this.tree_calback} id={this.props.id} name={this.props.name} callback = {this.onDelete}/> : ""}
      </React.Fragment>
    );
  }
}

export default File;
