import Responde from './objects/responde'

async function GET(adres){
    try{
        await fetch(adres)
           .then(response => response.json())
           .then(data => ({
               data: data
           }))
           .then(res => {
               Responde.code = 1;
               Responde.data = res.data;
           });
       }
       catch(err){
           Responde.code = 420;
           Responde.data = {error : 'server error'};
       }
}



export default GET;