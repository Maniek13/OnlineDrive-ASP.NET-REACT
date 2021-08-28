import React from 'react'
import styles from '../styles/tree.module.css'
import Responde from '../../../objects/responde'
import ClickAwayListener from 'react-click-away-listener'

class NotFound extends React.Component{
    constructor(props) {
      super(props);

      this.state = {
        error : false
      };
      this.callback = this.props.callback.bind(this);
    }

    exit(){
        this.callback();
    }

    render() {
        return (
          <ClickAwayListener onClickAway={this.exit.bind(this)}>
            <div className={styles.add_form}>
                <button className={styles.exit} onClick={this.exit.bind(this)}></button>
                <div className={styles.error_msg}>{Responde.data}</div>
            </div>
          </ClickAwayListener>
        );
    }
}

export default NotFound;

