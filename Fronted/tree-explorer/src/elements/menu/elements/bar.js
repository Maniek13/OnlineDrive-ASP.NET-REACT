import React from 'react'
import AccountController from '../../../controllers/account/account_controller'
import Responde from '../../../objects/responde';
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
            <button className={styles.logout_btn} onClick={this.logout}>LogOut</button>
        </div>
    );
    }
}

export default Bar;
