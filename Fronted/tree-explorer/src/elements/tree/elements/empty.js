import React from 'react'
import styles from '../styles/tree.module.css'
import AddForm from '../forms/add_form'
import Element from '../objects/element'
import Provider from '../controller/provider'

class Empty extends React.Component {
  constructor(props){
    super(props)
    
    this.state = {
      add: false
    };

    this.onAdd = this.onAdd.bind(this);
    this.tree_calback = this.props.tree_calback.bind(this);
  }

  addForm(evt){
    if(Provider.modal === false){
      Provider.modal = true;
      Element.element.IdW = evt.target.value;
      this.setState({add : true});
    }  
  }

  onAdd(){
    Provider.modal = false;
    this.setState({add : false});
  }

  render() {
    return (
      <React.Fragment>
        <div className={styles.empty}>
        <button value={this.props.id} className={styles.add_btn} onClick={this.addForm.bind(this)}></button>
        </div>
        {this.state.add ? <AddForm tree_calback = {this.tree_calback} callback = {this.onAdd} /> : ""}
      </React.Fragment>
    );
  }
}

export default Empty;
