import React from 'react'
import styles from '../styles/tree.module.css'
import EditForm from '../forms/edit_form'
import Element from '../objects/element'
import DelForm from '../forms/del_form'
import NotFound from '../forms/not_found_form'
import Provider from '../controller/provider'
import FileController from '../../../controllers/file/file_controller'
import Responde from '../../../objects/responde'

class File extends React.Component {
  constructor(props){
    super(props)
    
    this.state = {
      edit: false,
      delete : false,
      file: true
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

  async download(evt){
    Element.element.Id = this.props.id;
    Element.element.Name = this.props.name;
    await FileController.download();
    if(Responde.code !== 200){
      this.setState({file : false});
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

  onCloseMessage(){
    this.setState({file : true});
  }


  render() {
    return (
      <React.Fragment>
        <div className={styles.file_cont}>
          <div className={styles.file}>
            <p className={styles.name}>{this.props.name}</p>
            <button id={this.props.id}  name={this.props.name} className={styles.del_btn} onClick={this.delForm.bind(this)}></button>
            <button id={this.props.id} idw={this.props.idw} name={this.props.name} className={styles.edit_btn} onClick={this.editForm.bind(this)}></button>
            <button className={styles.download_btn} onClick={this.download.bind(this)}></button>
          </div>
        </div>
        
        {this.state.edit ? <EditForm tree_calback = {this.tree_calback} idw={this.props.idw} name={this.props.name} node={false} callback = {this.onEdit}/> : ""}
        {this.state.delete ? <DelForm tree_calback = {this.tree_calback} id={this.props.id} name={this.props.name} callback = {this.onDelete}/> : ""}
        {this.state.file ? "" : <NotFound callback = {this.onCloseMessage.bind(this)}/>}
      </React.Fragment>
    );
  }
}

export default File;
