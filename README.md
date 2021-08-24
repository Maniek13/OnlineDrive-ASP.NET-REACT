# Explorer tree

Backend
1. Create new project: ASP.NET Core (Model-View-Controller) instal pasckage: use this from nuget console: Install-Package Microsoft.EntityFrameworkCore.Design
2. Copy files from repo
3. In menager nuget packet console run: 

Add-Migration InitialCreate -Context TreeExplorerContext

Update-Database -Context TreeExplorerContext

4. Start app

Frontend
npm -v: 6.14.5
node -v: 14.4.0

1. Create new React project: npx create-react-app tree-explorer
2. Delete folders public and src
3. Instal ClickAwayListener: npm i react-click-away-listener
4. Copy files from repo to your app folder
5. Start server: npm start

![image](https://user-images.githubusercontent.com/47826375/127868547-1f4ee6bf-75ee-4eab-8c50-356a7aac62c6.png)

If want more chars in response (longer name of files/folder) please change ContentLengthLimit in startup.cs 


To add users database

3. In menager nuget packet console run:  (But this is in progres on fronted, but workin without. Mayby I add this tomorow)
Add-Migration InitialCreate -Context UsserContext
Update-Database -Context UsserContext