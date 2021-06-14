import React from 'react'
import styles from '../styles/tree.module.css'
import EditForm from '../forms/edit_form'
import Element from '../objects/element'

class File extends React.Component {
  constructor(props){
    super(props)
    
    this.state = {
      edit: false
    };
    this.onEdit = this.onEdit.bind(this);
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
  

  onEdit(){
    this.setState({edit : false});
  }

  render() {
    
    return (
      <React.Fragment>
        <div className={styles.file}>
          <a className={styles.label}>{this.props.name}</a>
          <button id={this.props.id} idW={this.props.idW} name={this.props.name} fileType={this.props.fileType}  className={styles.edit_btn} onClick={this.editForm.bind(this)}>edit</button>
        </div>
        {this.state.edit ? <EditForm idW={this.props.idW} name={this.props.name} node={false} callback = {this.onEdit}/> : ""}
      </React.Fragment>
    );
  }
}

export default File;
