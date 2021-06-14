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
        <div className={styles.empty}>
            <button value={this.props.id} className={styles.add_btn} onClick={this.addForm.bind(this)}>+</button>
            {this.state.add ? <AddForm callback = {this.onAdd} /> : ""}
        </div>
    );
  }
}

export default Empty;
