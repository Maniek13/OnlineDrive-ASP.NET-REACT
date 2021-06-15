import Responde from '../../controllers/http/objects/responde'
import List from '../objects/list'
import GET from '../../controllers/http/get'


async function GetList(){
    await GET("https://localhost:5001/Elements/Show");

    if(typeof Responde.data.Error == 'undefined'){
        List.tree = Responde.data;
    }
    else{
        Responde.code = 200;
        Responde.data = Responde.data.Error;
    }

}

export default GetList;