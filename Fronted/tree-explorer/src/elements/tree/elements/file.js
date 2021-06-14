import React from 'react'
import styles from '../styles/tree.module.css'

class File extends React.Component {
  constructor(props){
    super(props)
    
    this.state = {
      add: false
    };
  }

  render() {
    return (
          <div className={styles.file}>
            <a className={styles.label}>{this.props.name}</a>
          </div>

    );
  }
}

export default File;
