# Online drive(IN PROGRES)

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

![image](https://user-images.githubusercontent.com/47826375/131103764-ea90d64f-8637-4527-af08-76cb7922223c.png)

![image](https://user-images.githubusercontent.com/47826375/131124773-65c16d8d-09d1-46c5-9bf0-e5b7cdf14722.png)

![image](https://user-images.githubusercontent.com/47826375/131124807-a143292f-b1cf-4374-be0a-19bd3f1404ab.png)

If want more chars in response (longer name of files/folder) please change ContentLengthLimit in startup.cs 

_____________________IN PROGRES________________________


- modal css (add/delete/etc...)



