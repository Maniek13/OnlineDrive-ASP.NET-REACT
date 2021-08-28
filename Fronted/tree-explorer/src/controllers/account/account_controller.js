import POST from '../http/post'
import Usser from '../../elements/account_login/objects/usser';
import Responde from '../../objects/responde';
import {browserName} from 'react-device-detect';


class AccountController{
    static async add(){
        await POST("https://localhost:5001/Ussers/Add", Usser.usser);

        if(Responde.code === 200){
            Usser.id.Id = Responde.data.id;
            Usser.usser.Password = Responde.data.password;
        }
    }
    
    static async confirm(){
        await POST("https://localhost:5001/Ussers/Confirm", Usser.usser);
        
        if(Responde.code === 200){
            Usser.id.Id = Responde.data;
        }
    }

    static async save_usser_data(){
        await this.data();
        await POST("https://localhost:5001/UsserDatas/SaveUsserData", {UsserId : Usser.id.Id, IpV4 : Usser.usser_data.IpV4, Browser : Usser.usser_data.Browser } );
    }

    static async is_saved(){
        await this.data();
        await POST("https://localhost:5001/UsserDatas/IsSaved", Usser.usser_data);
        if(Responde.code === 200){
            Usser.id.Id = Responde.data.id;
            Usser.usser.Name = Responde.data.name;
            Usser.usser.Password = Responde.data.password
        }
    }

    static async remove_data(){
        await POST("https://localhost:5001/UsserDatas/RemoveData", Usser.usser_data);  
    }

    static async data(){
        const publicIp = require('public-ip');
        Usser.usser_data.IpV4 = await publicIp.v4();
        Usser.usser_data.Browser = browserName;
    }
}

export default AccountController;