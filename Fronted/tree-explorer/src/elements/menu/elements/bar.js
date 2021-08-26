import React from 'react'
import AccountController from '../../../controllers/account/account_controller'
import Usser from '../../account_login/objects/usser';
import styles from '../styles/menu.module.css'

class Bar extends React.Component {
    async logout(){
        await AccountController.remove_data();
        window.location.reload();
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
