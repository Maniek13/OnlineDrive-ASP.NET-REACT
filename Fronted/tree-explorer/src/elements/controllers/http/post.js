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