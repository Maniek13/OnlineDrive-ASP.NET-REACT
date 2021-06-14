import POST from '../../controllers/http/post'
import Element from '../objects/element'

async function Add(){
        await POST("https://localhost:5001/Elements/Show", JSON.stringify(Element));
}

export default Add;