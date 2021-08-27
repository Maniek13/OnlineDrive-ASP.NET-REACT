import React from 'react'
import AccountController from '../../../controllers/account/account_controller'
import Responde from '../../../objects/responde'
import Usser from '../objects/usser'
import styles from '../styles/account.module.css'
import Error from './error'

class Form extends React.Component {
  constructor(props){
    super(props)

    this.state = {
      error: false,
      remember: false,
      next: false
    };

    this.login_callback = this.props.login_callback.bind(this);
  }

  login_data(evt){
    Usser.usser.Name = evt.target.value;
  }

  password_data(evt){
    Usser.usser.Password = evt.target.value;
  }

  async login(){
    await AccountController.confirm();
      if(Usser.id.Id !== "" && Responde.code === 200){
        await this.save_data();
        this.login_callback();
      }
      else{
        this.setState({error : true});
      }
  }

  async save_data(){
    if(this.state.remember === true){
      await AccountController.save_usser_data();
    }
  }

  async remember(evt){
    this.setState({remember : !this.state.remember});
  }

  async register(){
    await AccountController.add();
    if(Usser.id.Id !== "" && Responde.code === 200){
      await this.save_data();
      this.login_callback();
    }
    else{
      this.setState({error : true});
    }
  }

  render() {
    return (
      <div className={styles.login_form}>
        <p>Login to online drive</p>
        <div className={styles.input_container}> 
          <input className={styles.input_form} id="login" placeholder="Login" onChange={this.login_data.bind(this)}></input>
          <input className={styles.input_form} id="password" placeholder="Password" onChange={this.password_data.bind(this)}></input>
        </div>
          
        <div className={styles.remember_me_form}>
              <label>Remember me</label> 
              <input value={this.state.remember} type="checkbox" id="type"  defaultChecked={false} onChange={this.remember.bind(this)} className={styles.input}/>
        </div>
        <div className={styles.btn_container}>
          <button className={styles.btn} onClick={this.login.bind(this)}>Login</button>
          <button className={styles.btn} onClick={this.register.bind(this)}>Register</button>
        </div>
        
        {this.state.error ? <Error/> : ""}
      </div>
    );
  }
}

export default Form;
