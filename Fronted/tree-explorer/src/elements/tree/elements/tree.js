import React from 'react'
import Provider from '../controller/provider'
import Branch from '../forms/branch_form'
import styles from '../styles/tree.module.css'


class Tree extends React.Component {
  constructor(props){
    super(props)
    this.state = {
      show: true
    };
    this.onChange = this.onChange.bind(this);
    
  }

  onChange(){
   if(Provider.show === true){
    this.setState({show : true})
    Provider.show = false;
   }
  }

  render() {
    return (
      <div className={styles.tree} >
        {this.state.show ? <Branch id={0} tree_calback={this.onChange}/> : ""}
      </div>
    );
  }
}

export default Tree;
