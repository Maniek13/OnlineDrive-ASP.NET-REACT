import POST from '../http/post'
import Responde from '../../objects/responde'
import Element from "../../elements/tree/objects/element"
import List from '../../elements/tree/objects/list'


class TreeController{
    static async get_tree(){
        await POST("https://localhost:5001/Elements/Show", {UsserId : Element.element.UsserId});
        
        if(Responde.code === 200){
            List.tree = Responde.data;
        }
    }
    
    static async add_tree_element(){
        await POST("https://localhost:5001/Elements/Add", Element.element);

        console.log(Responde.data);
    }

    static async delete(id){
        await POST("https://localhost:5001/Elements/Delete", {Id : id});
    }

    static async edit(){
        await POST("https://localhost:5001/Elements/Edit", Element.element);
    }

    static async sort_brand(id, type){
        await POST("https://localhost:5001/Elements/Sort", {Id : id, Type : type, UsserId : Element.element.UsserId});

        if(Responde.code === 200){
            List.tree = Responde.data;
            Responde.data = true;
        }
    }
}

export default TreeController;