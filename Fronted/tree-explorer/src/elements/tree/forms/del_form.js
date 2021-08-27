import React from 'react'
import styles from '../styles/tree.module.css'
import Responde from '../../../objects/responde'
import Provider from '../controller/provider'
import TreeController from '../../../controllers/tree/tree_controller'
import ClickAwayListener from 'react-click-away-listener'

class DelForm extends React.Component{
    constructor(props) {
      super(props);

      this.state = {
        error : false
      };

      this.id = this.props.id;
      this.name = this.props.name;
      
      this.tree_calback = this.props.tree_calback.bind(this);
      this.callback = this.props.callback.bind(this);
    }

    async delete(){
      await TreeController.delete(this.id);
      
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

    exit(){
        this.callback();
    }

    render() {
        return (
          <ClickAwayListener onClickAway={this.exit.bind(this)}>
            <div className={styles.add_form}>
                <button className={styles.exit} onClick={this.exit.bind(this)}></button>
                <p id="name" className={styles.input}>{this.name}</p>
                <div className={styles.btn_div}>
                    <button className={styles.form_btn} onClick={this.delete.bind(this)}>Delete</button>
                </div>
                
                {this.state.error ? <div className={styles.error}><a>{Responde.data}</a></div> : ""}
            </div>
          </ClickAwayListener>
        );
    }
}

export default DelForm;

