import React from 'react'
import Element from '../objects/element'
import styles from '../styles/tree.module.css'
import POST from '../../controllers/http/post'
import Responde from '../../controllers/http/objects/responde'

class AddForm extends React.Component{
    constructor(props) {
      super(props);

      this.state = {checked : false};
    }

    type(evt){
      Element.element.Type = this.state.checked ? "node" : "file";
      this.setState({checked : !this.state.checked});
    }

    name(evt){
      Element.element.Name = evt.target.value;
    }

    async add(){
    
      await POST("https://localhost:5001/Elements/Add", Element.element);
      console.log(Responde.data);
    }


    render() {
        return (
        <React.Fragment>
            <div className={styles.add_form}>
              <div className={styles.el_form}>
                 <label className={styles.label}>Name:</label>
                <input id="name" type="text" onChange={this.name.bind(this)} className={styles.input}/>
              </div>
              <div className={styles.el_form}>
                <label className={styles.label}>Is folder?</label> 
                <input value={this.state.checked} type="checkbox" id="type"  defaultChecked={false} onChange={this.type.bind(this)} className={styles.input}/>
              </div>
              <button className={styles.add_form_btn} onClick={this.add}>Add</button>
            </div>
        </React.Fragment>
        );
    }
}

export default AddForm;

