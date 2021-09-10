import React from 'react'
import Usser from '../../account_login/objects/usser'
import styles from '../styles/menu.module.css'
import Cookies from 'js-cookie'

class Bar extends React.Component {
    async logout(){
        Cookies.remove('login', { path: '' })
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
