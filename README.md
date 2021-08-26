# Online disk (IN PROGRES)

Backend
1. Create new project: ASP.NET Core (Model-View-Controller) instal pasckage: use this from nuget console: Install-Package Microsoft.EntityFrameworkCore.Design
2. Copy files from repo
3. In menager nuget packet console run: 

a.
Add-Migration Tree -Context TreeExplorerContext

Update-Database -Context TreeExplorerContext

b.
Add-Migration Usser -Context UsserContext

Update-Database -Context UsserContext

c.
Add-Migration UsserData -Context UsserDataContext

Update-Database -Context UsserDataContext

4. Start app
5. Please set data first

![image](https://user-images.githubusercontent.com/47826375/130889690-d0f1c302-386e-4d5b-a257-f1a44729659e.png)

Frontend
npm -v: 6.14.5
node -v: 14.4.0

1. Create new React project: npx create-react-app tree-explorer
2. Delete folders public and src
3. Instal ClickAwayListener: npm i react-click-away-listener
4. Copy files from repo to your app folder
5. Start server: npm start

![image](https://user-images.githubusercontent.com/47826375/130794215-999fd332-4780-4e29-8f4a-44a3448ef6eb.png)

![image](https://user-images.githubusercontent.com/47826375/130889397-b6e7950d-fee5-4c58-9230-dc5f38768ef1.png)

![image](https://user-images.githubusercontent.com/47826375/130889604-7aa99509-a9e0-496a-9d7c-e117027ffe7a.png)

If want more chars in response (longer name of files/folder) please change ContentLengthLimit in startup.cs 

_____________________IN PROGRES________________________

- the appearance of the file manager (css)

??? stoped working /// dont add els?????



