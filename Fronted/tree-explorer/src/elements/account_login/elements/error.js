import React from 'react'
import Responde from '../../../objects/responde';
import styles from '../styles/account.module.css'

class Error extends React.Component {

  render() {
    return (
        <div className={styles.error}>{Responde.data}</div>
    );
  }
}

export default Error;
