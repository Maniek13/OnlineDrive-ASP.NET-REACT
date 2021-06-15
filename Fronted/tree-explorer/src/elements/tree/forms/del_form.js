import React from 'react'
import styles from '../styles/tree.module.css'
import Responde from '../../controllers/http/objects/responde'
import Provider from '../controller/provider'
import TreeController from '../../controllers/tree/tree_controller'

class DelForm extends React.Component{
    constructor(props) {
      super(props);

      this.state = {
        error : false
      };
    }


    async delete(){
      await TreeController.delete(this.props.id);
      
      if(Responde.data === true){
        
        await TreeController.get_tree();
    
        if(typeof Responde.data.error === 'undefined'){
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
                <p id="name" className={styles.input}>{this.props.name}</p>
                <div className={styles.btn_div}>
                    <button className={styles.form_btn} onClick={this.delete.bind(this)}>Delete</button>
                </div>
                
                {this.state.error ? <div className={styles.error}><a>{Responde.data}</a></div> : ""}
            </div>
        );
    }
}

export default DelForm;

