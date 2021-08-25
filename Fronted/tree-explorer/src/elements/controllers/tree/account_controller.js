import POST from '../http/post'
import Usser from '../../account/objects/usser';
import Responde from '../http/objects/responde';


class TreeController{
    static async add(){
        await Post("https://localhost:5001/Users/Add", Usser.usser);

        if(Responde.code == 1){
            Usser.id.Id = Responde.data;
        }
    }
    
    static async confirm(){
        await POST("https://localhost:5001/Elements/Add", Usser.usser);
        if(Responde.code == 1){
            Usser.id.Id = Responde.data;
        }
    }
}

export default TreeController;