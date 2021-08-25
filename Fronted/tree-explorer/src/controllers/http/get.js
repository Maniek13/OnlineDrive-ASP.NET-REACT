import Responde from '../../objects/responde'

async function GET(adres){
    try{
        await fetch(adres)
           .then(response => response.json())
           .then(data => ({
               data: data
           }))
           .then(res => {
                Responde.code = res.data.status;
                Responde.data = res.data.message;
            });
    }
    catch(err){
        Responde.code = 500;
        Responde.data = 'server error';
    }
}

export default GET;