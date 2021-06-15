import Responde from './objects/responde'

async function POST(adres, object) {
    const formData = new FormData()

    for (var i in object){
        formData.append(i, object[i]);
    }
    
    const requestOptions = {
        method: 'POST',
        body: formData
    };
    try{
     await fetch(adres, requestOptions)
        .then(response => response.json().then(data => ({
            data: data
        })).then(res => {
            if(typeof res.data.error == 'undefined'){
                Responde.code = 1;
                Responde.data = res.data.ok;
            }
            else{
                Responde.code = 200;
                Responde.data = res.data.error;
            }    
        }));
    }
    catch(err){
        Responde.code = 500;
        Responde.data = {Error : 'server error'};
    }
}

export default POST;