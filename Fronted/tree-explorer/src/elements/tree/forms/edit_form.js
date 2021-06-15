import React from 'react'
import Element from '../objects/element'
import styles from '../styles/tree.module.css'
import POST from '../../controllers/http/post'
import Responde from '../../controllers/http/objects/responde'
import List from '../objects/list'
import Provider from '../controller/provider'
import GetList from '../controller/get_list'


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
                if(el.id !== this.props.id){
                  fields.push(  <option value={el.id} id={el.name}>{el.name} </option> )
                }
                
            }
        })

        fields.push(  <option value={0} id="root" >Root</option> )
        return fields;
    }

    async edit(){
      await POST("https://localhost:5001/Elements/Edit", Element.element);
      
      if(Responde.data === true){
        await GetList();
    
        if(typeof Responde.data.Error == 'undefined'){
          this.setState({error : false});
          this.props.callback();
          Provider.show = true;
          this.props.tree_calback();
        }
        else{
          this.setState({error : true});
        }
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
                <select id="node" className={styles.type } defaultValue={this.props.idW}  onChange={this.node.bind(this)} >
                  {this.nodes()}
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

