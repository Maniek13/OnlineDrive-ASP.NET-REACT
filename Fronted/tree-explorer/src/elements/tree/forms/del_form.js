import React from 'react'
import styles from '../styles/tree.module.css'
import POST from '../../controllers/http/post'
import Responde from '../../controllers/http/objects/responde'

class DelForm extends React.Component{
    constructor(props) {
      super(props);
    }


    async delete(){
      await POST("https://localhost:5001/Elements/Delete", {Id : this.props.id});
      
      if(Responde.data === true){
        this.props.callback();
      }  
    }

    render() {
        return (
            <div className={styles.add_form}>
              
                <a id="name" className={styles.input}>{this.props.name}</a>
             
              <button className={styles.del_form_btn} onClick={this.delete.bind(this)}>Delete</button>
            </div>
        );
    }
}

export default DelForm;

