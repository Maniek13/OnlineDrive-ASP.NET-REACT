import React from 'react'
import styles from '../styles/tree.module.css'
import AddForm from '../forms/add_form'
import Element from '../objects/element'


class Empty extends React.Component {
  constructor(props){
    super(props)
    
    this.state = {
      add: false
    };
  }

  addForm(evt){
    Element.element.IdW = evt.target.value;
    this.setState({add : true});
  }

  render() {
    return (
        <div className={styles.empty}>
            <button value={this.props.id} className={styles.add_btn} onClick={this.addForm.bind(this)}>+</button>
            {this.state.add ? <AddForm/> : ""}
        </div>
    );
  }
}

export default Empty;