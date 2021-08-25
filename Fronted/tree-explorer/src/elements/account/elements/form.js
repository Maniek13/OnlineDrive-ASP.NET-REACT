import React from 'react'
import styles from '../styles/account.module.css'

class Form extends React.Component {
  constructor(props){
    super(props)
  }

  
  login(evt){
  }

  register(evt){
  }

  render() {
    return (
        <div className={styles.login_form}>
            <input className={styles.input_form} id="login" placeholder="Login"></input>
            <input className={styles.input_form} id="password" placeholder="Password"></input>
            <button className={styles.btn} onClick={this.login.bind(this)}>Login</button>
            <button className={styles.btn} onClick={this.register.bind(this)}>Register</button>
        </div>
    );
  }
}

export default Form;
