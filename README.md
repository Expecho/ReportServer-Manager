## Project Description
This application lets you manage a Microsoft Reporting Service server. It supports SQL 2000,2005, 2008, 2008R2 & 2012, native or in SharePoint mode.


## Features
- Download reports stored on the server, optionally including any (sub)folders by recreating the folder structure on the server by selecting a folder on the server. It will then download all reports and subfolders to the client. 
- Upload multiple reports to the server, optionally you can upload a folder structure to the report server by selected a folder with reports and subfolders stored on the client. 
- Get detailed information about the items on the server, like used datasources, parameters and permissions. 
- Create and delete folders on the server. 
- Move reports, datasources and folders. 
- Rename reports, datasources and folders. 
- Delete reports, datasources and folders. 
- Set the datasource of multiple report on the server by first selecting folders and reports on the server and then the datasource to use. When targeting a folder all reports in that folder and any subfolder will be updated. 
- Get an overview of item properties (used datasources, security settings, parameters and properties) 
- Create or edit datasource 
- Supports models: upload new models, replace existing (with compatibility check, great when administrating big sites w many users)

## Remarks
- When downloading items and the item already exists on the client the item will be overwritten. 
- When downloading folders, empty folders will be skipped. 
- Datasources cannot be downloaded
