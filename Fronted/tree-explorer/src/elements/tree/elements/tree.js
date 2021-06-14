import React from 'react'
import List from '../objects/list'
import AddForm from '../forms/add_form';
import styles from '../styles/tree.module.css'
import Element from '../objects/element'


class Tree extends React.Component {
  constructor(props){
    super(props)
    
    this.state = {
      add: false
    };
  }

  show(){
    if(List.tree === ''){
      return this.showAddFormsBtn(0)
    }
    else{
      let fields = [];
    
      List.tree.forEach(el => {
        if(el.idW === 0){
          fields.push(this.showAddFormsBtn(el.id))
        }
      });

      return fields;
    } 
  }

  addForm(evt){
    Element.element.IdW = evt.target.value;
    this.setState({add : true});
  }
  
  showAddFormsBtn(id){
    return (<button value={id} className={styles.add_btn} onClick={this.addForm.bind(this)}></button>);
  }

  render() {
    return (
      <React.Fragment>
          {this.show()}
          {this.state.add ? <AddForm/> : ""}
      </React.Fragment>
      
    );
  }
}

export default Tree;
