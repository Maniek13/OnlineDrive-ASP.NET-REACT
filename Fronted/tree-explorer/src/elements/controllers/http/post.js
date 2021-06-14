import Responde from './objects/responde'

async function POST(adres, object) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json'},
        body: JSON.stringify(object)
    };
    try{
     await fetch(adres, requestOptions)
        .then(response => response.json().then(data => ({
            data: data
        })).then(res => {
            Responde.code = 1;
            Responde.data = res.data
        }));
    }
    catch(err){
        Responde.code = 420;
        Responde.data = {Error : 'server error'};
    }
   

}

export default POST;