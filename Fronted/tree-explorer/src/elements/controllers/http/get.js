import Responde from './objects/responde'

async function GET(adres){
    try{
        await fetch(adres)
           .then(response => response.json())
           .then(data => ({
               data: data
           }))
           .then(res => {
               if(typeof res.data.error == 'undefined'){
                Responde.code = 1;
                Responde.data = res.data;
               }
               else{
                Responde.code = 420;
                Responde.data = res.data;
               }
               
           });
       }
       catch(err){
           Responde.code = 420;
           Responde.data = {error : 'server error'};
       }
}



export default GET;