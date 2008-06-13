// ****************************************************************************************************
// Created by Peter Bons. Feel free to modify as needed.
// my website: http://www.2000miles.nl
// ****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using RSS_Report_Retrievers.Classes;

namespace RSS_Report_Retrievers
{
    class Controller : IController
    {
        public IRSFacade RsFacade;
        private TreeView tvReportServer = null;
        private ToolStripStatusLabel toolStripStatusLabel = null;
        private ListView lvItems = null;
        ViewItems viewItem = ViewItems.Folders;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="treeView">The treeview to use for building a list of folders on the server</param>
        /// <param name="label">The toolstrip status label to use for information messages</param>
        /// <param name="listView">The listview to use for displaying the folders and report on the server</param>
        public Controller(TreeView treeView, ToolStripStatusLabel label, ListView listView)
        {
            tvReportServer = treeView;
            toolStripStatusLabel = label;
            lvItems = listView;

        }

        /// <summary>
        /// Gets or Sets the items that will be visible in the treeview and listview,
        /// use this to hide for example all datasources or reports
        /// </summary>
        public ViewItems ViewItem
        {
            set
            {
                viewItem = value;
            }
            get
            {
                return viewItem;
            }
        }

        #region Populate TreeView
        /// <summary>
        /// Populate treeview on startup
        /// </summary>
        public void PopulateTreeView()
        {
            tvReportServer.Nodes.Clear();

            TreeNode root = new TreeNode("Root");
            root.Name = "/";
            root.ToolTipText = "/";
            root.Tag = ReportItemTypes.Folder;
            tvReportServer.Nodes.Add(root);

            if (toolStripStatusLabel != null)
            {
                toolStripStatusLabel.Text = "Retrieving server information....";
                Application.DoEvents();
            }

            try
            {
                ExpandNodeContent(root);
                root.Expand();
                tvReportServer.SelectedNode = root;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("An error occured: {0}. Please check the application settings using the menu.", ex.Message));
            }
            finally
            {
                if (toolStripStatusLabel != null)
                {
                    toolStripStatusLabel.Text = "";
                }
            }
        }

        /// <summary>
        /// Traverse through item collection of the report server
        /// </summary>
        /// <param name="parent">TreeNode to which child nodes are appended</param>
        private void ExpandNodeContent(TreeNode parent)
        {
            foreach (ReportItemDTO item in RsFacade.ListChildren(parent.ToolTipText, false))
            {
                if (item.Type == ReportItemTypes.Folder && (viewItem == ViewItems.Folders || viewItem == ViewItems.All || viewItem == ViewItems.Datasources))
                {
                    TreeNode folder = new TreeNode(item.Name);
                    folder.Name = item.Name;
                    folder.ImageIndex = item.Hidden ? 4 : 2;
                    folder.Tag = ReportItemTypes.Folder;
                    folder.ToolTipText = item.Path;
                    parent.Nodes.Add(folder);
                    ExpandNodeContent(folder); //Explore subfolders on the report server
                }
                else if (item.Type == ReportItemTypes.Datasource && (viewItem == ViewItems.Datasources || viewItem == ViewItems.All))
                {
                    TreeNode datasource = new TreeNode(item.Name);
                    datasource.Name = item.Name;
                    datasource.ImageIndex = 0;
                    datasource.Tag = ReportItemTypes.Datasource;
                    datasource.ToolTipText = item.Path;
                    parent.Nodes.Add(datasource);
                }
                else if (item.Type == ReportItemTypes.Report && viewItem == ViewItems.All)
                {
                    TreeNode report = new TreeNode(item.Name);
                    report.Name = item.Name;
                    report.ImageIndex = 1;
                    report.Tag = ReportItemTypes.Report;
                    report.ToolTipText = item.Path;
                    parent.Nodes.Add(report);
                }
            }
        }
        #endregion

        #region Populate Server Items
        /// <summary>
        /// Displays all items in a folder
        /// </summary>
        /// <param name="path">Path to the folder to show</param>
        public void PopulateItems(string path)
        {
            lvItems.Items.Clear();

            foreach (ReportItemDTO item in RsFacade.ListChildren(path, false))
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = item.Name;
                lvi.ToolTipText = item.Path;
              
                switch (item.Type)
                {
                    case ReportItemTypes.Folder:
                        lvi.ImageIndex = item.Hidden ? 4 : 2; ;
                        lvi.Tag = ReportItemTypes.Folder;
                        break;
                    case ReportItemTypes.Report:
                        lvi.ImageIndex = item.Hidden ? 5 : 1;
                        lvi.Tag = ReportItemTypes.Report; 
                        break;
                    case ReportItemTypes.Datasource:
                        lvi.Tag = ReportItemTypes.Datasource; 
                        lvi.ImageIndex = 0;
                        break;
                    case ReportItemTypes.model:
                        lvi.Tag = ReportItemTypes.model;
                        lvi.ImageIndex = 6;
                        break;
                }
                lvItems.Items.Add(lvi);
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="path">item to delete</param>
        public void DeleteItem(string path)
        {
            try
            {
                RsFacade.DeleteItem(path);

                toolStripStatusLabel.Text = String.Format("'{0}' deleted", path);
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("An error occured: {0}", ex.Message));
            }
        }
        #endregion

        #region New folder
        /// <summary>
        /// Create a new folder on the reportserver
        /// </summary>
        /// <param name="name">name of the new folder</param>
        /// <param name="parent">The TreeNode onto which the new node will be attached</param>
        /// <returns>The TreeNode that represent the created folder</returns>
        public TreeNode CreateFolder(string name, TreeNode parent)
        {
            try
            {
                RsFacade.CreateFolder(name, parent.ToolTipText, null);
            }
            catch (Exception ex)
            {
                if (!ex.Message.ToLower().Contains("already exists"))
                {
                    throw new Exception(ex.Message, ex);
                }
            }

            TreeNode folder = new TreeNode(name);
            folder.Name = name;
            folder.ImageIndex = 2;
            folder.Tag = ReportItemTypes.Folder;

            // The ToolTipText contains the full path of the new folder
            if (parent.ToolTipText.EndsWith("/"))
            {
                folder.ToolTipText = parent.ToolTipText + name;
            }
            else
            {
                folder.ToolTipText = parent.ToolTipText + "/" + name;
            }

            parent.Nodes.Add(folder);

            return folder;
        }
        #endregion

        #region Upload
        /// <summary>
        /// Recursively upload reports in a folder
        /// </summary>
        /// <param name="path">folder to process</param>
        /// <param name="destination">destination folder on the report server</param>
        /// <param name="overwrite">overwrite existing items</param>
        /// <param name="parent">parent TreeNode of the created folder</param>
        public void UploadFolder(string path, string destination, bool overwrite, TreeNode parent)
        {
            foreach (string filename in Directory.GetFiles(path, "*.rdl", SearchOption.TopDirectoryOnly))
            {
                UploadItem(filename, destination, overwrite);
            }

            foreach (string foldername in Directory.GetDirectories(path))
            {
                // Create a new folder and upload any files into it
                UploadFolder(foldername, destination.TrimEnd('/') + "/" + Path.GetFileName(foldername), overwrite, CreateFolder(Path.GetFileName(foldername), parent));
            }
        }

        /// <summary>
        /// Create report on the server
        /// </summary>
        /// <param name="filename">file to upload</param>
        /// <param name="destination">folder on the report server to upload</param>
        /// <param name="overwrite">overwrite existing items</param>
        public void UploadItem(string filename, string destination, bool overwrite)
        {
            try
            {
                FileStream stream = File.OpenRead(filename);
                Byte[] definition = new Byte[stream.Length];
                stream.Read(definition, 0, (int)stream.Length);
                stream.Close();

                ReportWarning[] warnings;
                RsFacade.CreateReport(Path.GetFileNameWithoutExtension(filename), destination, overwrite, definition, null,out warnings);

                toolStripStatusLabel.Text = String.Format("Uploaded report {0}", Path.GetFileName(filename));
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("An error has occured: {0}", ex.Message));
            }
        }
        #endregion

        #region Set Datasource
        /// <summary>
        /// Set the datasource of a report
        /// </summary>
        /// <param name="item">path of the item</param>
        /// <param name="datasource">path of the datasource to bind</param>
        public void SetDatasource(string item, string datasource, ReportItemTypes type)
        {
            switch (type)
            {
                case ReportItemTypes.Folder:
                    foreach (ReportItemDTO catalogItem in RsFacade.ListChildren(item, true))
                    {
                        SetDatasource(catalogItem.Path, datasource, catalogItem.Type);
                    }
                    break;
                case ReportItemTypes.Report:
                case ReportItemTypes.model:

                    RsFacade.SetItemDataSources(item, datasource);

                    toolStripStatusLabel.Text = String.Format("Updated datasource of {0}", item);
                    
                    break;

                default:
                    toolStripStatusLabel.Text = String.Format("Cannot set datasource of item {0}", item);
                    break;
            }
        }

        #endregion

        #region Move
        /// <summary>
        /// Move items on the ReportServer
        /// </summary>
        /// <param name="source">source path</param>
        /// <param name="destination">destionation path</param>
        /// <param name="type">Type of item</param> 
        public void MoveItem(string source, string destination, ReportItemTypes type)
        {
            RsFacade.MoveItem(source, destination,type);
        }
        #endregion

        #region Get Item Information
        /// <summary>
        /// Get a list of parameters and their information
        /// </summary>
        /// <param name="path">The path of the item to query</param>
        /// <returns>A list with parameters and their information</returns>
        public List<List<String>> GetReportParameters(string path)
        {
            return RsFacade.GetReportParameters(path);
        }

        /// <summary>
        /// Get a list of datasource names used in a report
        /// </summary>
        /// <param name="path">path of the item to query</param>
        /// <returns>A list of datasource names used in a report</returns>
        public List<String> GetReportDatasources(string path)
        {
            return RsFacade.GetReportDatasources(path);
        }

        /// <summary>
        /// Get a list of properties an their values
        /// </summary>
        /// <param name="path">path of the item to query</param>
        /// <returns>A list of item properties and their values</returns>
        public List<List<String>> GetItemProperties(string path)
        {

            return RsFacade.GetItemProperties(path); 
        }

        /// <summary>
        /// Get a list of item permissions and their roles
        /// </summary>
        /// <param name="path">path of the item to query</param>
        /// <returns>A list of item permissions and their roles</returns>
        public List<List<String>> GetItemSecurity(string path)
        {
            return RsFacade.GetItemSecurity(path);
        }
        #endregion

        #region Download
        /// <summary>
        /// Download an item from the reportserver
        /// </summary>
        /// <param name="path">The path on the report server to download</param>
        /// <param name="destination">The destination on the client to save the downloaded files in</param>
        /// <param name="type">The type of the selected item to download</param>
        /// <param name="preserveFolders">if true, the folder structure will be preserved, when false
        /// all files in subfolders will be saved to the destination folder</param>
        /// <remarks>Datasources cannot be downloaded, existing items will be overwritten and empty folders will be skipped</remarks> 
        public void DownloadItem(string path, string destinationFolder, ReportItemTypes type, bool preserveFolders)
        {
            switch (type)
            {
                case ReportItemTypes.Folder:
                    foreach (ReportItemDTO catalogItem in RsFacade.ListChildren(path, true))
                    {
                        DownloadItem(catalogItem.Path, destinationFolder, catalogItem.Type, preserveFolders);
                    }
                    break;

                case ReportItemTypes.Report:

                    XmlDocument definition = new XmlDocument();
                    
                    definition.Load(new MemoryStream(RsFacade.GetReportDefinition(path)));

                    SaveItem(path, type, destinationFolder,preserveFolders, definition);

                    break;

                case ReportItemTypes.model:
                    XmlDocument model = new XmlDocument();

                    model.Load(new MemoryStream(RsFacade.GetModelDefinition(path)));

                    SaveItem(path, type, destinationFolder, preserveFolders, model);

                    break;
            }

            toolStripStatusLabel.Text = String.Format("Downloaded '{0}'", path);
            Application.DoEvents();
        }

        private string AppendFileSuffix(string path, ReportItemTypes type)
        {
            string filename = path;

            switch (type)
            {
                case ReportItemTypes.model:
                    filename = filename + ".smdl";
                    break;
                case ReportItemTypes.Report:
                    filename = filename + ".rdl";
                    break;
            }

            return filename;
        }

        private void SaveItem(string filename, ReportItemTypes type, string destination, bool preserveFolders, XmlDocument definition)
        {
            filename = AppendFileSuffix(filename, type);

            if (preserveFolders)
            {
                string sourceBaseDirectory = tvReportServer.SelectedNode.ToolTipText;

                //Create destinationdirectory by removing the base-directory from the clicked item

                string relativeFilePath = Path.GetDirectoryName(filename.Substring(sourceBaseDirectory.Length));

                destination = System.IO.Path.Combine(destination, relativeFilePath);
                destination = destination.Replace('/','\\');
            }

            filename = Path.GetFileName(filename);

            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            definition.Save(String.Format(@"{0}\{1}", destination, filename));

        }
        #endregion
    }
}
