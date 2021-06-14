import List from "../objects/list";

function GetName(id){
    let name = "root";
    List.tree.forEach(el =>
        {
            if(el.id === id ){
                
                name = el.name;
            }
        }
    )

    return name;
}

export default GetName;