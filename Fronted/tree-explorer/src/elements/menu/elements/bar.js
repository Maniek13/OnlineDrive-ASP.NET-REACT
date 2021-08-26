import React from 'react'
import AccountController from '../../../controllers/account/account_controller'
import Responde from '../../../objects/responde';
import Usser from '../../account_login/objects/usser';
import styles from '../styles/menu.module.css'

class Bar extends React.Component {
    constructor(props){
        super(props)
    }

    async logout(){
        await AccountController.remove_data();
        if(Responde.code === 200){
            window.location.reload();
        }
    }

    render() {
    return (
        <div className={styles.bar}>
            <div className={styles.name}>{Usser.usser.Name}</div>
            <button className={styles.logout_btn} onClick={this.logout}>LogOut</button>
        </div>
    );
    }
}

export default Bar;
