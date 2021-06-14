import React from 'react'
import Element from '../objects/element'
import Add from '../controllers/add_controller'
import styles from '../styles/tree.module.css';

class AddForm extends React.Component  {
    constructor(props) {
        super(props);

        this.state = {
          isChecked: false,
        };
      }

    toggleChange(){
        this.setState({
          isChecked: !this.state.isChecked,
        });
        Element.type = this.state.isChecked;
    }



    render(evt) {
        Element.idw = evt.target.value;
        
        return (
        <React.Fragment>
            <div className={styles.add_form}>
                <label>Name:</label>
                <input id="name" type="text" onChange={e => Element.name = e.target.value}/>
                <label>Is folder?</label> 
                <input type="checkbox" id="type"  defaultChecked={this.state.isChecked} onChange={this.toggleChange()}/>
            </div>

            <button className={styles.add_btn} onClick={Add()}></button>
        </React.Fragment>
        
        );
    }
}

export default AddForm;

