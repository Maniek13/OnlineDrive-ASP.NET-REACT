import POST from '../http/post'
import Responde from '../../objects/responde'
import Element from "../../elements/tree/objects/element"
import List from '../../elements/tree/objects/list'
import Usser from '../../elements/account_login/objects/usser';
import Settings from '../../objects/settings';


class TreeController{
    static async get_tree(){
        await POST(Settings.baseUrl+"/Elements/Show", {UsserId : Usser.id.Id, Password : Usser.usser.Password});
        
        if(Responde.code === 200){
            List.tree = Responde.data;
        }
    }
    
    static async add_tree_element(){
        await POST(Settings.baseUrl+"/Elements/Add", {Name : Element.element.Name, Type : Element.element.Type, IdW : Element.element.IdW, UsserId : Element.element.UsserId, File : Element.element.File, Password : Usser.usser.Password});
    }

    static async delete(id){
        await POST(Settings.baseUrl+"/Elements/Delete", {Id : id, UsserId : Element.element.UsserId, Password : Usser.usser.Password});
    }

    static async edit(){
        await POST(Settings.baseUrl+"/Elements/Edit", {Id : Element.element.Id, Name : Element.element.Name, Type : Element.element.Type, IdW : Element.element.IdW, UsserId : Element.element.UsserId, Password : Usser.usser.Password});
    }

    static async sort_brand(id, type){
        await POST(Settings.baseUrl+"/Elements/Sort", {Id : id, Type : type, UsserId : Element.element.UsserId, Password : Usser.usser.Password});
        if(Responde.code === 200){
            List.tree = Responde.data;
            Responde.data = true;
        }
    }
}

export default TreeController;