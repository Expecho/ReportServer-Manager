// ****************************************************************************************************
// Created by Peter Bons. Feel free to modify as needed.
// my website: http://www.2000miles.nl
// ****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using RSS_Report_Retrievers.RSS_2005;
using System.Xml;

namespace RSS_Report_Retrievers
{
    class DefaultMode2005 : IReportingServicesFactory
    {
        private ReportingService2005 rs = new ReportingService2005();
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
        public DefaultMode2005(TreeView treeView, ToolStripStatusLabel label, ListView listView)
        {
            tvReportServer = treeView;
            toolStripStatusLabel = label;
            lvItems = listView;

            // use windows authentication when indicated
            if (global::RSS_Report_Retrievers.Properties.Settings.Default.UseWindowsAuthentication)
            {
                rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            }
            else
            {
                rs.Credentials = new System.Net.NetworkCredential(
                        global::RSS_Report_Retrievers.Properties.Settings.Default.Username,
                        global::RSS_Report_Retrievers.Properties.Settings.Default.Password,
                        global::RSS_Report_Retrievers.Properties.Settings.Default.Domain
                );
            }
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

        /// <summary>
        /// Convert the item type of the reportserver class to our own item type enum
        /// </summary>
        /// <param name="type">The reportserver type to convert</param>
        /// <returns>The converted type</returns>
        private ItemTypes ConvertItemType(ItemTypeEnum type)
        {
            ItemTypes convertedType = ItemTypes.Unknown;

            switch (type)
            {
                case ItemTypeEnum.Folder: convertedType = ItemTypes.Folder; break;
                case ItemTypeEnum.Report: convertedType = ItemTypes.Report; break;
                case ItemTypeEnum.DataSource: convertedType = ItemTypes.Datasource; break;
            }

            return convertedType;
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
            root.Tag = ItemTypes.Folder;
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
            foreach (CatalogItem item in rs.ListChildren(parent.ToolTipText, false))
            {
                if (item.Type == ItemTypeEnum.Folder && (viewItem == ViewItems.Folders || viewItem == ViewItems.All))
                {
                    TreeNode folder = new TreeNode(item.Name);
                    folder.Name = item.Name;
                    folder.ImageIndex = item.Hidden ? 4 : 2;
                    folder.Tag = ItemTypes.Folder;
                    folder.ToolTipText = item.Path;
                    parent.Nodes.Add(folder);
                    ExpandNodeContent(folder); //Explore subfolders on the report server
                }
                else if (item.Type == ItemTypeEnum.DataSource && (viewItem == ViewItems.Datasources || viewItem == ViewItems.All))
                {
                    TreeNode datasource = new TreeNode(item.Name);
                    datasource.Name = item.Name;
                    datasource.ImageIndex = 0;
                    datasource.Tag = ItemTypes.Datasource;
                    datasource.ToolTipText = item.Path;
                    parent.Nodes.Add(datasource);
                }
                else if (item.Type == ItemTypeEnum.Report && viewItem == ViewItems.All)
                {
                    TreeNode report = new TreeNode(item.Name);
                    report.Name = item.Name;
                    report.ImageIndex = 1;
                    report.Tag = ItemTypes.Report;
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

            foreach (CatalogItem item in rs.ListChildren(path, false))
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = item.Name;
                lvi.ToolTipText = item.Path;
              
                switch (item.Type)
                {
                    case ItemTypeEnum.Folder:
                        lvi.ImageIndex = item.Hidden ? 4 : 2; ;
                        lvi.Tag = ItemTypes.Folder;
                        break;
                    case ItemTypeEnum.Report:
                        lvi.ImageIndex = item.Hidden ? 5 : 1;
                        lvi.Tag = ItemTypes.Report; 
                        break;
                    case ItemTypeEnum.DataSource:
                        lvi.Tag = ItemTypes.Datasource; 
                        lvi.ImageIndex = 0;
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
                rs.DeleteItem(path);

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
                rs.CreateFolder(name, parent.ToolTipText, null);
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
            folder.Tag = ItemTypes.Folder;

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

                rs.CreateReport(Path.GetFileNameWithoutExtension(filename), destination, overwrite, definition, null);

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
        public void SetDatasource(string item, string datasource, ItemTypes type)
        {
            string dataSourceName = ("/" + datasource.Split('/')[datasource.Split('/').GetUpperBound(0)]).Trim('/');

            switch (type)
            {
                case ItemTypes.Folder:
                    foreach (CatalogItem catalogItem in rs.ListChildren(item, true))
                    {
                        SetDatasource(catalogItem.Path, datasource, ConvertItemType(catalogItem.Type));
                    }
                    break;
                case ItemTypes.Report:
                    foreach (DataSource availableDataSource in rs.GetItemDataSources(item))
                    {
                        // Only update the report when the selected datasource is used in that report
                        if (availableDataSource.Name == dataSourceName)
                        {
                            try
                            {
                                DataSourceReference dsr = new DataSourceReference();
                                dsr.Reference = datasource;

                                DataSource[] dataSources = new DataSource[1];

                                DataSource ds = new DataSource();
                                ds.Item = (DataSourceDefinitionOrReference)dsr;
                                ds.Name = ("/" + datasource.Split('/')[datasource.Split('/').GetUpperBound(0)]).Trim('/');
                                dataSources[0] = ds;

                                rs.SetItemDataSources(item, dataSources);

                                toolStripStatusLabel.Text = String.Format("Updated datasource of {0}", item);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(String.Format("An error has occured: {0}", ex.Message));
                            }
                        }
                    }
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
        public void MoveItem(string source, string destination, ItemTypes type)
        {
            rs.MoveItem(source, destination.StartsWith("/") ? destination : "/" + destination);
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
            List<List<String>> parameters = new List<List<string>>();

            foreach (RSS_2005.ReportParameter parameter in rs.GetReportParameters(path, null, false, null, null))
            {
                List<String> parameterinfo = new List<String>();
                parameterinfo.Add(parameter.Name);
                parameterinfo.Add(parameter.Type.ToString());
                parameterinfo.Add(parameter.AllowBlank.ToString());
                parameterinfo.Add(parameter.Nullable.ToString());
                parameterinfo.Add(parameter.MultiValue.ToString());
                parameterinfo.Add(parameter.Prompt);
                parameters.Add(parameterinfo);
            }

            return parameters;
        }

        /// <summary>
        /// Get a list of datasource names used in a report
        /// </summary>
        /// <param name="path">path of the item to query</param>
        /// <returns>A list of datasource names used in a report</returns>
        public List<String> GetReportDatasources(string path)
        {
            List<String> datasources = new List<string>();
            foreach (DataSource ds in rs.GetItemDataSources(path))
            {
                datasources.Add(ds.Name);
            }

            return datasources;
        }

        /// <summary>
        /// Get a list of properties an their values
        /// </summary>
        /// <param name="path">path of the item to query</param>
        /// <returns>A list of item properties and their values</returns>
        public List<List<String>> GetItemProperties(string path)
        {
            List<List<String>> properties = new List<List<string>>(); 
            foreach (Property property in rs.GetProperties(path, null))
            {
                List<String> propertieinfo = new List<String>();
                propertieinfo.Add(property.Name);
                propertieinfo.Add(property.Value);
                properties.Add(propertieinfo);   
            }

            return properties; 
        }

        /// <summary>
        /// Get a list of item permissions and their roles
        /// </summary>
        /// <param name="path">path of the item to query</param>
        /// <returns>A list of item permissions and their roles</returns>
        public List<List<String>> GetItemSecurity(string path)
        {
            bool inheritParent;

            List<List<String>> permissions = new List<List<string>>();
            foreach (Policy permission in rs.GetPolicies(path, out inheritParent))
            {
                List<String> permissioninfo = new List<String>();
                string roles = "";
                permissioninfo.Add(permission.GroupUserName);
                foreach (Role role in permission.Roles)
                {
                    roles += role.Name + ",";
                }
                permissioninfo.Add(inheritParent.ToString());
                permissioninfo.Add(roles.TrimEnd(','));
                permissions.Add(permissioninfo);
            }

            return permissions;
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
        public void DownloadItem(string path, string destination, ItemTypes type, bool preserveFolders)
        {
            switch (type)
            {
                case ItemTypes.Folder:
                    foreach (CatalogItem catalogItem in rs.ListChildren(path, true))
                    {
                        DownloadItem(catalogItem.Path, destination, ConvertItemType(catalogItem.Type), preserveFolders);
                    }
                    break;
                case ItemTypes.Report:
                    try
                    {
                        XmlDocument definition = new XmlDocument();
                        definition.Load(new MemoryStream(rs.GetReportDefinition(path)));

                        string directory = destination;

                        if (preserveFolders)
                        {
                            if (tvReportServer.SelectedNode.ToolTipText != "/")
                            {
                                directory = String.Format(@"{0}\{1}", destination, FormSSRSExplorer.GetItemPath("/" + path.ToLower().Replace(tvReportServer.SelectedNode.ToolTipText.ToLower(), ""), true));
                            }
                            else
                            {
                                directory = String.Format(@"{0}\{1}", destination, FormSSRSExplorer.GetItemPath(path, true));
                            }
                        }
                        
                        string filename = FormSSRSExplorer.GetItemName(path);

                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }

                        definition.Save(String.Format(@"{0}\{1}.rdl", directory, filename));

                        toolStripStatusLabel.Text = String.Format("Downloaded '{0}'", filename);
                        Application.DoEvents();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                    }
                    break;
            }
        }
        #endregion
    }
}
