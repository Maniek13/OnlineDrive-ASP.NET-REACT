# Dysk online ASP.NET CORE / REACT
To see visit (my pc must running, sory I do not have other server):
https://178.235.60.107:444/

login: admin
password: 12345

Project

1.  Copy files from repo

Backend

create file in wwwroot/js/server named index.js like to add action to reset data:

___________________________________________________________________________________________
function set() {

    document.getElementById("set_button").disabled = true;
    document.getElementById("responde").innerText = "Working in progres";   

    fetch('https://.../Elements/Set')
        .then(response => response.json())
        .then(data => {
            if (data.status === 200) {
                document.getElementById("responde").innerText = "The data has been reset";    
            }
            else {
                document.getElementById("responde").innerText = "The server is not responding";
            }
            document.getElementById("set_button").disabled = false;
    });
}
_____________________________________________________________________________________________

change serverURL to serverURL

2. In menager nuget packet console run: 

Run command: Add-Migration init

Run command: Update-Database

3. Start app

Frontend

Create file settings.js in src/objects like:
_____________________________________
class Setings{
    static baseUrl = "baseurl"
    
}

export default Setings;
_____________________________________

baseurl is url to server site like: baseurl/Elements/Show

npm -v: 6.14.5
node -v: 14.4.0

1. In cmd go to Fronted\tree-explorer
2. Run command: npm inatall
3. Run command: npm i -g npm-check-updates
4. Run command: ncu -u
5. Run command: npm install
3. Run command: npm start
4. Have fun! :)


![image](https://user-images.githubusercontent.com/47826375/131267365-14391242-8f58-4fc7-acd2-322ab369782a.png)

![image](https://user-images.githubusercontent.com/47826375/131267612-ef25b18f-27fa-485c-ad8c-9f86d3bf26e0.png)

