import Element from '../../elements/tree/objects/element';
import Responde from '../../objects/responde'

async function File(adres, object){
    const formData = new FormData()
    for (var i in object){
        formData.append(i, object[i]);
    }
    try{
        fetch(adres, {
            method: 'POST',
            body: formData
        })
        .then(response => response.blob())
        .then(blob => {
            if(blob.size > 36){
                Responde.code = 200;
                var url = window.URL.createObjectURL(blob);
                var a = document.createElement('a');
                a.href = url;
                a.download = Element.element.Name;
                document.body.appendChild(a); 
                a.click();    
                a.remove();    
            }
            else{
                Responde.code = 420;
                Responde.data = 'Not found';
            }
                
        });
    }
    catch(err){
        Responde.code = 500;
        Responde.data = 'server error';
    }
}

export default File;