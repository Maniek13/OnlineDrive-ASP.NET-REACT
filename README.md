# Explorer tree

Backend
1. Create new project: ASP.NET Core (Model-View-Controller) instal pasckage: use this from nuget console: Install-Package Microsoft.EntityFrameworkCore.Design
2. Copy files from repo
3. In menager nuget packet console run: 

a.
Add-Migration InitialCreate -Context TreeExplorerContext

Update-Database -Context TreeExplorerContext
b.
Add-Migration InitialCreate -Context UsserContext

Update-Database -Context UsserContext

4. Start app
5. Please set data first

Frontend
npm -v: 6.14.5
node -v: 14.4.0

1. Create new React project: npx create-react-app tree-explorer
2. Delete folders public and src
3. Instal ClickAwayListener: npm i react-click-away-listener
4. Copy files from repo to your app folder
5. Start server: npm start

![image](https://user-images.githubusercontent.com/47826375/130794215-999fd332-4780-4e29-8f4a-44a3448ef6eb.png)

![image](https://user-images.githubusercontent.com/47826375/127868547-1f4ee6bf-75ee-4eab-8c50-356a7aac62c6.png)

If want more chars in response (longer name of files/folder) please change ContentLengthLimit in startup.cs 


_____________________________IN PROGRES___________________________________________________

- don;t show error in login page
- add save and download files

