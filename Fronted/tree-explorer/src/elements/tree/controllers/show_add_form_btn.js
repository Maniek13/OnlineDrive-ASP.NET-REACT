import styles from '../styles/tree.module.css'; 
import AddForm from '../forms/add_form'; 

function ShowAddFormsBtn(id){
    return (<button value={id} className={styles.add_btn} onClick={AddForm.bind(this)}></button>);
}


export default ShowAddFormsBtn;