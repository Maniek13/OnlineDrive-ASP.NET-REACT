import React from 'react'
import List from '../objects/list'
import ShowAddFormsBtn from '../controllers/show_add_form_btn'


class Tree extends React.Component {
  show(){
    if(List.tree === ''){
      return ShowAddFormsBtn(0)
    }
    else{
      let fields = [];
    
      List.tree.forEach(el => {
        if(el.idW === 0){
          fields.push(ShowAddFormsBtn(el.id))
          
        }
      });

      return fields;
    } 
  }

  render() {
    return (
      <React.Fragment>
          {this.show()}
      </React.Fragment>
      
    );
  }
}

export default Tree;
