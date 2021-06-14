import React from 'react'
import styles from '../styles/tree.module.css'
import POST from '../../controllers/http/post'
import Responde from '../../controllers/http/objects/responde'

class DelForm extends React.Component{
    constructor(props) {
      super(props);

      this.state = {
        error : false
      };
    }


    async delete(){
      await POST("https://localhost:5001/Elements/Delete", {Id : this.props.id});
      
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
                <p id="name" className={styles.input}>{this.props.name}</p>
                <div className={styles.btn_div}>
                    <button className={styles.form_btn} onClick={this.delete.bind(this)}>Delete</button>
                </div>
                
                {this.state.error ? <div className={styles.error}><a>Server error</a></div> : ""}
            </div>
        );
    }
}

export default DelForm;

