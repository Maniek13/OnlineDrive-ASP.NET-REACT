import Responde from '../../objects/responde'

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
        .then(response => response.json()
        .then(data => ({
            data: data
        }))
        .then(res => {
            if(res.data.status === 413){
                Responde.code = 413;
                Responde.data = "Request is to long";
            }
            else {
                Responde.code = res.data.status;
                Responde.data = res.data.message;
            }    
        }));
    }
    catch(err){
        Responde.code = 500;
        Responde.data = 'server error';
    }
}

export default POST;