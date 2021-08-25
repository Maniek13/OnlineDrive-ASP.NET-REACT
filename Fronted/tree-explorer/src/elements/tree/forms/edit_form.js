import React from 'react'
import Element from '../objects/element'
import styles from '../styles/tree.module.css'
import Responde from '../../../objects/responde'
import List from '../objects/list'
import Provider from '../controller/provider'
import TreeController from '../../../controllers/tree/tree_controller'
import ClickAwayListener from 'react-click-away-listener'

class EditForm extends React.Component{
    constructor(props) {
      super(props);

      this.state = {
        checked : this.props.node,
        error : false
      };

      this.id = this.props.id;
      this.idW = this.props.idw;
      this.name = this.props.name;

      
      this.tree_calback = this.props.tree_calback.bind(this);
      this.callback = this.props.callback.bind(this);

      Element.element.Type = this.state.checked ? "node" : "file";
    }

    type(evt){
      Element.element.Type = this.state.checked ? "file" : "node";
      this.setState({checked : !evt.target.value});
    }

    getName(evt){
      Element.element.Name = evt.target.value;
    }

    node(evt){
      Element.element.IdW = evt.target.value;
    }

    nodes(){
      let fields = [];
      List.tree.forEach(el => {
          if(el.type === "node"){
              if(el.id !== this.id){
                fields.push(  <option value={el.id} id={el.name}>{el.name} </option> )
              }
          }
      })

      fields.push(  <option value={0} id="root" >Root</option> )
      return fields;
    }

    async edit(){
      await TreeController.edit();
      if(Responde.code === 200){
        Responde.code = 100;
        await TreeController.get_tree();

        if(Responde.code === 200){
          this.setState({error : false});
          this.callback();
          Provider.show = true;
          Element.element.Name = "";
          Element.element.Id = "";
          Element.element.IdW = "";
          Element.element.Type = "file";
          this.tree_calback();
        }
        else{
          this.setState({error : true});
        }
      }  
      this.setState({error : true});
    }

    exit(){
      Element.element.Name = "";
      Element.element.Id = "";
      Element.element.IdW = "";
      Element.element.Type = "file";
      this.callback();
    }

    render() {
      return (
        <ClickAwayListener onClickAway={this.exit.bind(this)}>
            <div className={styles.add_form}>
              <button className={styles.exit} onClick={this.exit.bind(this)}>X</button>
            <div className={styles.el_form}>
              <label className={styles.label}>Name:</label>
              <input id="name" type="text" defaultValue={this.name} onChange={this.getName.bind(this)} className={styles.input}/>
            </div>
            <div className={styles.el_form}>
              <select id="node" className={styles.type } defaultValue={this.idw}  onChange={this.node.bind(this)} >
                {this.nodes()}
              </select>
            </div>
            <div className={styles.btn_div}>
              <button className={styles.form_btn} onClick={this.edit.bind(this)}>Edit</button>
            </div>
            {this.state.error ? <div className={styles.error}><a>{Responde.data === false? "Enter wrong data": Responde.data}</a></div> : ""}
          </div>
        </ClickAwayListener> 
      );
    }
}

export default EditForm;

