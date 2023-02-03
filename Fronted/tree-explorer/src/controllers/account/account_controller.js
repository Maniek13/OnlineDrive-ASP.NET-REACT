import POST from '../http/post'
import Usser from '../../elements/account_login/objects/usser';
import Responde from '../../objects/responde';
import Settings from '../../objects/settings';

class AccountController{
    static async add(){
        await POST(Settings.baseUrl+"/Ussers/Add", Usser.usser);

        if(Responde.code === 200){
            Usser.id.Id = Responde.data.id;
            Usser.usser.Password = Responde.data.password;
        }
    }
    
    static async confirm(){
        await POST(Settings.baseUrl+"/Ussers/Confirm", Usser.usser);
        
        if(Responde.code === 200){
            Usser.id.Id = Responde.data.id;
            Usser.usser.Password = Responde.data.password;
        }
    }
}

export default AccountController;