import React from 'react'
import styles from '../styles/tree.module.css'
import Responde from '../../controllers/http/objects/responde'

class Error extends React.Component {
  render() {
    return (
      <React.Fragment>
        <div className={styles.error_page}>
            <div className={styles.error}><a>{Responde.code} : {Responde.data}</a></div>
        </div>
      </React.Fragment>
    );
  }
}

export default Error;
