import React from 'react'
import AccountController from '../../../controllers/account/account_controller';
import Responde from '../../../objects/responde';
import Usser from '../objects/usser';
import styles from '../styles/account.module.css'
import Error from './error';

class Form extends React.Component {
  constructor(props){
    super(props)

    this.state = {
      error: false
    };

    this.login_callback = this.props.login_callback.bind(this);
  }

  login_data(evt){
    Usser.usser.Name = evt.target.value;
  }

  password_data(evt){
    Usser.usser.Password = evt.target.value;
  }

  async login(evt){
    await AccountController.confirm();
      if(Usser.id.Id !== "" && Responde.code === 200){
        this.login_callback();
      }
      else{
        this.state.error = true;
      }
  }

  async register(evt){
    await AccountController.add();
    if(Usser.id.Id !== "" && Responde.code === 200){
      this.login_callback();
    }
    else{
      this.state.error = true;
    }
  }

  render() {
    return (
        <div className={styles.login_form}>
            <input className={styles.input_form} id="login" placeholder="Login" onChange={this.login_data.bind(this)}></input>
            <input className={styles.input_form} id="password" placeholder="Password" onChange={this.password_data.bind(this)}></input>
            <button className={styles.btn} onClick={this.login.bind(this)}>Login</button>
            <button className={styles.btn} onClick={this.register.bind(this)}>Register</button>
            {this.state.error === true ? <Error/> : ""}
        </div>
    );
  }
}

export default Form;
