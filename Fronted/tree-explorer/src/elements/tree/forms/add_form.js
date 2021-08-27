import React from 'react'
import Element from '../objects/element'
import styles from '../styles/tree.module.css'
import Responde from '../../../objects/responde'
import Provider from '../controller/provider'
import TreeController from '../../../controllers/tree/tree_controller'
import ClickAwayListener from 'react-click-away-listener'

class AddForm extends React.Component{
    constructor(props) {
      super(props);
      
      this.state = {
        checked : false,
        error : false
      };

      this.tree_calback = this.props.tree_calback.bind(this);
      this.callback = this.props.callback.bind(this);

      Element.element.Type = this.state.checked ? "node" : "file";
    }

    type(evt){
      Element.element.Type = this.state.checked ? "file" : "node";
      this.setState({checked : !this.state.checked});
    }

    name(evt){
      Element.element.Name = evt.target.value;
    }

    async add(){
      await TreeController.add_tree_element();
    
      if(Responde.code === 200){
        Responde.code = 100;
        await TreeController.get_tree();
    
        if(Responde.code === 200){
          this.setState({error : false});
          this.callback();
          Provider.show = true;
          this.tree_calback();
        }
        else{
          this.setState({error : true});
        }
      }
      this.setState({error : true});
    }
    

    file(evt){
      Element.element.Name = evt.target.files[0].name;
      Element.element.File = evt.target.files[0];
    }

    exit(){
      this.callback();
    }

    render() {
        return (
          <ClickAwayListener onClickAway={this.exit.bind(this)}>
              <div className={styles.add_form}>
              <button className={styles.exit} onClick={this.exit.bind(this)}></button>

              {
              this.state.checked ? 
              <div className={styles.el_form}>
                <label className={styles.label}>Name:</label>
                <input id="name" type="text" onChange={this.name.bind(this)} className={styles.input}/>
              </div> : 
              <input type="file" name="file" onChange={this.file.bind(this)}/>
              }

              <div className={styles.el_form}>
                <label>Is folder?</label> 
                <input value={this.state.checked} type="checkbox" id="type"  defaultChecked={false} onChange={this.type.bind(this)} className={styles.input}/>
              </div>
              <div className={styles.btn_div}>
                <button className={styles.form_btn} onClick={this.add.bind(this)}>Add</button>
              </div>
              
              {this.state.error ? <div className={styles.error}><a>{Responde.data === false? "Enter name": Responde.data}</a></div> : ""}
            </div>
          </ClickAwayListener>
        );
    }
}

export default AddForm;

