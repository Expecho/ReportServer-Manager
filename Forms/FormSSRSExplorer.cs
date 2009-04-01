using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using RSS_Report_Retrievers.Classes;

namespace RSS_Report_Retrievers
{
    public partial class FormSSRSExplorer : Form
    {
        private const string REPORT_FILTER_STRING = "Reports|*.rdl";
        private const string MODEL_FILTER_STRING = "Models|*.smdl";
        private const string MODEL_FILEEXTENSION = ".smdl";
        private Controller rs;

        public static ServerSettingsConfigElement SelectedServer = null;
        ServerSettingsConfigElementCollection serverCollection = null;


        public FormSSRSExplorer()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Populate the TreeView with reports in the report server
        /// </summary>
        private void frmReportSelector_Shown(object sender, EventArgs e)
        {
            BuildConnectSubMenu();
            //Connect();
        }

        /// <summary>
        /// Hide copy/move/delete commands when needed
        /// </summary>
        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (rs == null)
            {
                e.Cancel = true;
            }
            else
            {
                moveToolStripMenuItem.Visible = lvItems.SelectedItems.Count > 0;
                deleteToolStripMenuItem.Visible = lvItems.SelectedItems.Count > 0;
                toolStripMenuItemSep2.Visible = lvItems.SelectedItems.Count > 0;
                downloadToolStripMenuItem.Visible = lvItems.SelectedItems.Count > 0 && (ReportItemTypes)lvItems.SelectedItems[0].Tag != ReportItemTypes.Datasource;
                renameToolStripMenuItem.Visible = lvItems.SelectedItems.Count == 1;
                propertiesToolStripMenuItem.Visible = lvItems.SelectedItems.Count == 1;
                newFolderToolStripMenuItem.Visible = lvItems.SelectedItems.Count == 0;
                createDatasourceToolStripMenuItem.Visible = lvItems.SelectedItems.Count == 0;
                uploadToolStripMenuItem.Visible = lvItems.SelectedItems.Count == 0;
                setDatasourceToolStripMenuItem.Visible = lvItems.SelectedItems.Count > 0 && (ReportItemTypes)lvItems.SelectedItems[0].Tag != ReportItemTypes.Datasource;
                editToolStripMenuItem.Visible = lvItems.SelectedItems.Count > 0 && (ReportItemTypes)lvItems.SelectedItems[0].Tag == ReportItemTypes.Datasource;
                toolStripMenuItemSep1.Visible = toolStripMenuItemSep1.Visible && lvItems.SelectedItems.Count > 0 && (ReportItemTypes)lvItems.SelectedItems[0].Tag != ReportItemTypes.Datasource;

                replaceModelToolStripMenuItem.Visible = lvItems.SelectedItems.Count > 0 && (ReportItemTypes)lvItems.SelectedItems[0].Tag == ReportItemTypes.model;
                setItemSecurityToolStripMenuItem.Visible = lvItems.SelectedItems.Count > 0 && (ReportItemTypes)lvItems.SelectedItems[0].Tag == ReportItemTypes.Folder;
            }
        }

        /// <summary>
        /// When a node is expanded, show its items in the ListView
        /// </summary>
        private void tvReportServer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (rs != null)
            {
                rs.PopulateItems(e.Node.ToolTipText);
            }
        }

        /// <summary>
        /// When a folder is double clicked in the ListView, set this folder as the selected folder in the TreeView
        /// The items in the folder will then be automatically displayed in the ListView because of the 
        /// tvReportServer_AfterSelect event handler
        /// </summary>
        private void lvItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ListViewItem lvi = lvItems.GetItemAt(e.X, e.Y);
                if (lvi != null && (ReportItemTypes)lvi.Tag == ReportItemTypes.Folder)
                {
                    foreach (TreeNode node in tvReportServer.Nodes.Find(lvi.Text, true))
                    {
                        if (node.ToolTipText == lvi.ToolTipText)
                        {
                            tvReportServer.SelectedNode = node;
                            node.Nodes.Clear();
                            rs.ExpandNodeContent(node, true);
                            node.Expand();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Create a new folder, add the new folder to the TreeView en ListView
        /// </summary>
        private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvReportServer.SelectedNode != null)
            {
                FormGetName GetName = new FormGetName("Create new folder");
                if (GetName.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (GetName.Name.Trim() == "")
                        {
                            throw new Exception("Name cannot be blank");
                        }
                        rs.CreateFolder(GetName.Name, tvReportServer.SelectedNode);

                        toolStripStatusLabel.Text = String.Format("Created folder '{0}'", GetName.Name);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("An error occured: {0}", ex.Message));
                    }
                }

                rs.PopulateItems(tvReportServer.SelectedNode.ToolTipText);
            }
        }

        /// <summary>
        /// Upload files
        /// </summary>
        private void filesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = REPORT_FILTER_STRING + "|" + MODEL_FILTER_STRING;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                bool overwrite = MessageBox.Show("Overwrite existing items?", "Upload", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

                this.Cursor = Cursors.WaitCursor;

                foreach (string filename in openFileDialog.FileNames)
                {
                    string destinationFolder = tvReportServer.SelectedNode.ToolTipText;

                    if (Path.GetExtension(filename) == MODEL_FILEEXTENSION)
                    {
                        rs.CreateModel(filename, destinationFolder, overwrite);
                    }
                    else
                    {
                        rs.UploadReport(filename, destinationFolder, overwrite);
                    }
                }

                rs.PopulateItems(tvReportServer.SelectedNode.ToolTipText);

                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Upload folder
        /// </summary>
        private void folderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSelectFilesystemFolder folderSelector = new FormSelectFilesystemFolder();
            folderSelector.EnableCreateNewFolder = false;
            if (folderSelector.ShowDialog() == DialogResult.OK)
            {
                bool overwrite = MessageBox.Show("Overwrite existing items?", "Upload", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

                this.Cursor = Cursors.WaitCursor;

                rs.UploadFolder(folderSelector.Foldername, tvReportServer.SelectedNode.ToolTipText, overwrite, tvReportServer.SelectedNode);
                rs.PopulateItems(tvReportServer.SelectedNode.ToolTipText);

                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                this.Cursor = Cursors.WaitCursor;

                foreach (ListViewItem item in lvItems.SelectedItems)
                {
                    rs.DeleteItem(item.ToolTipText);

                    if ((ReportItemTypes)item.Tag == ReportItemTypes.Folder)
                    {
                        foreach (TreeNode node in tvReportServer.SelectedNode.Nodes)
                        {
                            if (node.ToolTipText == item.ToolTipText)
                            {
                                tvReportServer.Nodes.Remove(node);
                            }
                        }
                    }
                }

                this.Cursor = Cursors.Default;

                rs.PopulateItems(tvReportServer.SelectedNode.ToolTipText);
            }
        }

        #region Helper functions
        /// <summary>
        /// Extracts the name of an item on the ReportServer, like System.IO.Path.GetFileName
        /// </summary>
        /// <param name="path">path of the item</param>
        /// <returns>name of the item</returns>
        public static string GetItemName(string path)
        {
            string[] parts = path.Split('/');
            return path.Split('/')[parts.Length - 1];
        }

        /// <summary>
        /// Extracts the path withouth the name of an item on the ReportServer, like System.IO.Path.GetFileName
        /// </summary>
        /// <param name="path">path of the item</param>
        /// <returns>path of the item withouth the name</returns>
        public static string GetItemPath(string path, bool forFileSystem)
        {
            path = path.Substring(0, path.LastIndexOf("/"));
            return forFileSystem ? path.Replace("/", @"\") : path;
        }
        #endregion


        /// <summary>
        /// Displays the server-window
        /// </summary>
        private void ShowSelectServer()
        {
            FormServers servers = new FormServers();

            servers.ShowDialog();

            if (servers.DialogResult != DialogResult.Cancel)
            {
                SelectedServer = servers.SelectedServer;
            }

            BuildConnectSubMenu();
        }


        /// <summary>
        /// Exit application
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void settingsToolStripMenuItem_Click(object s, EventArgs e)
        {
            ShowSelectServer();
        }

        /// <summary>
        /// Rename an item
        /// </summary>
        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGetName GetName = new FormGetName("Rename item");
            GetName.Name = lvItems.SelectedItems[0].Text;
            if (GetName.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    rs.MoveItem(lvItems.SelectedItems[0].ToolTipText, GetItemPath(lvItems.SelectedItems[0].ToolTipText, false) + "/" + GetName.Name, (ReportItemTypes)lvItems.SelectedItems[0].Tag);
                    toolStripStatusLabel.Text = String.Format("Renamed item '{0}' to '{1}'", lvItems.SelectedItems[0].Text, GetItemPath(lvItems.SelectedItems[0].ToolTipText, false) + "/" + GetName.Name);

                    if ((ReportItemTypes)lvItems.SelectedItems[0].Tag == ReportItemTypes.Folder)
                    {
                        rs.PopulateTreeView();
                    }

                    rs.PopulateItems(tvReportServer.SelectedNode.ToolTipText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                }
            }
        }

        /// <summary>
        /// Display properties of selected item
        /// </summary>
        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProperties frmProperties = new FormProperties(lvItems.SelectedItems[0].ToolTipText, (ReportItemTypes)lvItems.SelectedItems[0].Tag);
            frmProperties.ShowDialog();
        }

        /// <summary>
        /// Set the datasource of a report
        /// Check if any of a report datasources matches the selected datasource. If so update that datasource, otherwise 
        /// provide information about the report datasources
        /// </summary>
        private void setDatasourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSSRSSItemSelector ssrsExplorer = new FormSSRSSItemSelector(ViewItems.Datasources);
            if (ssrsExplorer.ShowDialog() == DialogResult.OK && ssrsExplorer.SelectedItemPath != null)
            {
                foreach (ListViewItem item in lvItems.SelectedItems)
                {
                    rs.SetDatasource(item.ToolTipText, ssrsExplorer.SelectedItemPath, (ReportItemTypes)item.Tag);
                }
            }
        }

        /// <summary>
        /// Move reports and folders
        /// </summary>
        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSSRSSItemSelector ssrsExplorer = new FormSSRSSItemSelector(ViewItems.Folders);
            if (ssrsExplorer.ShowDialog() == DialogResult.OK && ssrsExplorer.SelectedItemPath != null)
            {
                this.Cursor = Cursors.WaitCursor;

                foreach (ListViewItem item in lvItems.SelectedItems)
                {
                    try
                    {
                        rs.MoveItem(item.ToolTipText, ssrsExplorer.SelectedItemPath.Trim('/') + "/" + item.Text, (ReportItemTypes)item.Tag);
                        toolStripStatusLabel.Text = String.Format("Moved item '{0}' to {1}", item.Text, ssrsExplorer.SelectedItemPath);
                        Application.DoEvents();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                    }
                }

                rs.PopulateTreeView();
                rs.PopulateItems(tvReportServer.SelectedNode.ToolTipText);

                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Download reports, alo includes any subfolders
        /// </summary>
        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSelectFilesystemFolder folderSelector = new FormSelectFilesystemFolder();
            if (folderSelector.ShowDialog() == DialogResult.OK)
            {
                bool askToPreserveFolders = false;
                foreach (ListViewItem item in lvItems.SelectedItems)
                {
                    if ((ReportItemTypes)item.Tag == ReportItemTypes.Folder)
                    {
                        askToPreserveFolders = true;
                        break;
                    }
                }
                
                bool preserveFolders = askToPreserveFolders ? MessageBox.Show("Preserve folders?", "Download items", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes : false;
                
                if (!Directory.Exists(folderSelector.Foldername))
                {
                    Directory.CreateDirectory(folderSelector.Foldername);
                }

                this.Cursor = Cursors.WaitCursor;

                foreach (ListViewItem item in lvItems.SelectedItems)
                {
                    rs.DownloadItem(item.ToolTipText, folderSelector.Foldername, (ReportItemTypes)item.Tag, preserveFolders);
                }

                this.Cursor = Cursors.Default;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About frmAbout = new About();
            frmAbout.ShowDialog(this);  
        }


        private void BuildConnectSubMenu()
        {
            serverCollection = ServerRegistry.GetServerSettings();

            connectToolStripMenuItem.DropDownItems.Clear();

            foreach (ServerSettingsConfigElement configEl in serverCollection)
            {
                ToolStripMenuItem newItem = new ToolStripMenuItem(configEl.Alias, null, connectToolStripMenuItem_Click);
                connectToolStripMenuItem.DropDownItems.Add(newItem);
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedServer = this.serverCollection.Get(((ToolStripMenuItem)sender).Text);
            Connect();

            FormSetPolicy frm = new FormSetPolicy();

        }

        private void Connect()
        {
            if (SelectedServer != null)
            {
                if (String.IsNullOrEmpty(SelectedServer.Url))
                {
                    ShowSelectServer();
                }
                else
                {
                    rs = ReportingServicesFactory.CreateFromSettings(SelectedServer,tvReportServer, toolStripStatusLabel, lvItems);
                }

                try
                {
                    rs.PopulateTreeView();
                    toolStripStatusLabel.Text = String.Format("Connected to {0}", FormSSRSExplorer.SelectedServer.Url);

                    this.Text = "SSRS Explorer - connected to " + SelectedServer.Alias;
                }
                catch
                {
                    this.Text = "SSRS Explorer - not connected";

                    toolStripStatusLabel.Text = "Not connected";
                    
                    MessageBox.Show("Cannot connect. Check server settings.", "Initialisation failed.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    
                    rs = null;
                }
            }
            else
            {
                ShowSelectServer();
            }

        }

        private void createDatasourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDatasource frmDatasource = new FormDatasource();
            frmDatasource.Extensions = rs.GetDataExtensions(); 

            if (frmDatasource.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    rs.CreateDataSource(frmDatasource.Datasource, tvReportServer.SelectedNode.ToolTipText);
                    rs.PopulateItems(tvReportServer.SelectedNode.ToolTipText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDatasource frmDatasource = new FormDatasource();
            frmDatasource.Extensions = rs.GetDataExtensions();

            Datasource ds = rs.GetDatasource(lvItems.SelectedItems[0].ToolTipText);
            ds.Name = lvItems.SelectedItems[0].Text;     
            frmDatasource.Datasource = ds;
            
            if (frmDatasource.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    rs.CreateDataSource(frmDatasource.Datasource, tvReportServer.SelectedNode.ToolTipText);
                    rs.PopulateItems(tvReportServer.SelectedNode.ToolTipText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                }
            }
        }

        private void replaceModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFileDialog.FileName = "";
            this.openFileDialog.Filter = MODEL_FILTER_STRING;
            this.openFileDialog.ShowDialog();

            string fileName = this.openFileDialog.FileName;

            if (fileName != String.Empty)
            {
                string existingModelPath = lvItems.SelectedItems[0].ToolTipText;
                string newModelSMDL = System.IO.File.ReadAllText(fileName);

                if (MessageBox.Show("Do you want to perform a compatibility check before replacing?", "Perform compatibility check?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Forms.FormDependantItems form = new RSS_Report_Retrievers.Forms.FormDependantItems(this.rs, existingModelPath, newModelSMDL);

                    form.ShowDialog();
                }

                if(MessageBox.Show("Do you want to replace the existing model?", "Replace model", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    rs.ReplaceModel(fileName, existingModelPath);
            }
        }

        private void addPermissionsForUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rs.AddPolicyForMyReports(lvItems.SelectedItems[0].ToolTipText, lvItems.SelectedItems[0].Text, new FormSetPolicy());
        }

    }
}