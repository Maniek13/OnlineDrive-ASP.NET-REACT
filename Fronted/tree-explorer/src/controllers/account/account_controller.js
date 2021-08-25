import POST from '../http/post'
import Usser from '../../elements/account_login/objects/usser';
import Responde from '../../objects/responde';


class AccountController{
    static async add(){
        await POST("https://localhost:5001/Ussers/Add", Usser.usser);

        if(Responde.code == 200){
            Usser.id.Id = Responde.data;
        }
    }
    
    static async confirm(){
        await POST("https://localhost:5001/Ussers/Confirm", Usser.usser);
        
        if(Responde.code == 200){
            Usser.id.Id = Responde.data;
        }
    }
}

export default AccountController;