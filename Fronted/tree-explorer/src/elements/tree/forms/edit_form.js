import React from 'react'
import Element from '../objects/element'
import styles from '../styles/tree.module.css'
import POST from '../../controllers/http/post'
import Responde from '../../controllers/http/objects/responde'
import List from '../objects/list'

class EditForm extends React.Component{
    constructor(props) {
      super(props);

      this.state = {checked : false};
    }

    type(evt){
      Element.element.Type = this.state.checked ? "file" : "node";
      this.setState({checked : !evt.target.value});
    }

    name(evt){
      Element.element.Name = evt.target.value;
    }

    node(evt){
        Element.element.IdW = evt.target.value;
    }

    async add(){
      await POST("https://localhost:5001/Elements/Add", Element.element);
      
      if(Responde.data === true){
      }
      this.props.callback();
      
    }

    getName(id){
        return List.tree.forEach(el =>
            {
                if(el.id === id ){
                    return el.name;
                }
            }
        )
    }

    nodes(){
        let fields = [];

        List.tree.forEach(el => {
            if(el.type === "node"){
                fields.push(  <option value={el.name} id={el.id}>{el.name} </option> )
            }
        })
        return fields;
    }


    render() {
    

        return (
            <div className={styles.add_form}>
              <div className={styles.el_form}>
                 <label className={styles.label}>Name:</label>
                <input id="name" type="text" onChange={this.name.bind(this)} className={styles.input}/>
              </div>
              <div className={styles.el_form}>
                <label className={styles.label}>Is folder?</label> 
                <input value={this.props.node} type="checkbox" id="type"  defaultChecked={this.props.node} onChange={this.type.bind(this)} className={styles.input}/>
              </div>
              <div className={styles.el_form}>
                        <select id="node" className={styles.type } defaultValue={this.getName(this.props.idw)}  onChange={this.node.bind(this)} >
                                { this.nodes() }
                        </select>
              </div>
              <button className={styles.edit_form_btn} onClick={this.add.bind(this)}>Edit</button>
            </div>
        );
    }
}

export default EditForm;

