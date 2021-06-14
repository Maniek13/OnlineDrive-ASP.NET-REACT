import React from 'react'
import Element from '../objects/element'
import styles from '../styles/tree.module.css'
import POST from '../../controllers/http/post'
import Responde from '../../controllers/http/objects/responde'
import List from '../objects/list'
import GetName from '../controller/get_name'

class EditForm extends React.Component{
    constructor(props) {
      super(props);
      this.state = {
          checked : this.props.node,
          error : false
        };
      Element.element.Type = this.state.checked ? "node" : "file";
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

    nodes(){
        let fields = [];

        List.tree.forEach(el => {
            if(el.type === "node"){
                fields.push(  <option value={el.name} id={el.id}>{el.name} </option> )
            }
        })

        fields.push(  <option value="root" id={0} >Root</option> )
        return fields;
    }

    async edit(){
      await POST("https://localhost:5001/Elements/Edit", Element.element);
      
      if(Responde.data === true){
        this.setState({error : false});
        this.props.callback();
      }  
        this.setState({error : true});
      
    }

    exit(){
        this.props.callback();
    }

    render() {
        return (
            <div className={styles.add_form}>
                <button className={styles.exit} onClick={this.exit.bind(this)}>X</button>
              <div className={styles.el_form}>
                 <label className={styles.label}>Name:</label>
                <input id="name" type="text" defaultValue={this.props.name} onChange={this.name.bind(this)} className={styles.input}/>
              </div>
              <div className={styles.el_form}>
                <label className={styles.label}>Is folder?</label> 
                <input value={this.state.checked} type="checkbox" id="type"  defaultChecked={this.props.node} onChange={this.type.bind(this)} className={styles.input}/>
              </div>
              <div className={styles.el_form}>
                        <select id="node" className={styles.type } defaultValue={GetName(this.props.idw)}  onChange={this.node.bind(this)} >
                                { this.nodes() }
                        </select>
              </div>
              <div className={styles.btn_div}>
                <button className={styles.form_btn} onClick={this.edit.bind(this)}>Edit</button>
              </div>
              
              {this.state.error ? <div className={styles.error}><a>Plese enter name </a></div> : ""}
            </div>
        );
    }
}

export default EditForm;

