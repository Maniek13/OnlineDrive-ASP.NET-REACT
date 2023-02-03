import File from '../http/file'
import Element from '../../elements/tree/objects/element'
import Usser from '../../elements/account_login/objects/usser';
import Settings from '../../objects/settings';

class FileController{
    static async download(){
        await File(Settings.baseUrl+"/Elements/GetFile", {Id : Element.element.Id, UsserId : Usser.id.Id, Password : Usser.usser.Password});
    }
}

export default FileController;