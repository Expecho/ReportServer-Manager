using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using ReportingServerManager.Logic;
using ReportingServerManager.Logic.Configuration;
using ReportingServerManager.Logic.Shared;

namespace ReportingServerManager.Forms
{
    public partial class FormSSRSExplorer : Form
    {
        private const string REPORT_FILTER_STRING = "Reports|*.rdl";
        private const string DATASET_FILTER_STRING = "Datasets|*.rsd";
        private const string MODEL_FILTER_STRING = "Models|*.smdl";
        private const string MODEL_FILEEXTENSION = ".smdl";
        private Controller controller;

        public static ServerSettingsConfigElement SelectedServer = null;
        ServerSettingsConfigElementCollection serverCollection = null;


        public FormSSRSExplorer()
        {
            InitializeComponent();
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Populate the TreeView with reports in the report server
        /// </summary>
        private void FrmReportSelectorShown(object sender, EventArgs e)
        {
            BuildConnectSubMenu();
        }

        /// <summary>
        /// Hide copy/move/delete commands when needed
        /// </summary>
        private void ContextMenuStripOpening(object sender, CancelEventArgs e)
        {
            if (controller == null)
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

                replaceModelToolStripMenuItem.Visible = lvItems.SelectedItems.Count > 0 && (ReportItemTypes)lvItems.SelectedItems[0].Tag == ReportItemTypes.Model;
                setItemSecurityToolStripMenuItem.Visible = lvItems.SelectedItems.Count > 0 && (ReportItemTypes)lvItems.SelectedItems[0].Tag == ReportItemTypes.Folder;
            }
        }

        /// <summary>
        /// When a node is expanded, show its items in the ListView
        /// </summary>
        private void TvReportServerAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (controller != null)
            {
                try
                {
                    controller.PopulateItems(e.Node.ToolTipText);
                }
                catch (Exception ex)
                {
                    LogHandler.WriteLogEntry(ex);
                    MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                }
            }
        }

        /// <summary>
        /// When a folder is double clicked in the ListView, set this folder as the selected folder in the TreeView
        /// The items in the folder will then be automatically displayed in the ListView because of the 
        /// tvReportServer_AfterSelect event handler
        /// </summary>
        private void LvItemsMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var lvi = lvItems.GetItemAt(e.X, e.Y);
                if (lvi != null && (ReportItemTypes)lvi.Tag == ReportItemTypes.Folder)
                {
                    foreach (var node in tvReportServer.Nodes.Find(lvi.Text, true))
                    {
                        if (node.ToolTipText == lvi.ToolTipText)
                        {
                            try
                            {
                                tvReportServer.SelectedNode = node;
                                node.Nodes.Clear();
                                controller.ExpandNodeContent(node, true);
                                node.Expand();
                            }
                            catch (Exception ex)
                            {
                                LogHandler.WriteLogEntry(ex);
                                MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Create a new folder, add the new folder to the TreeView en ListView
        /// </summary>
        private void NewFolderToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (tvReportServer.SelectedNode != null)
            {
                var getName = new FormGetName("Create new folder");
                if (getName.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (getName.Name.Trim() == "")
                        {
                            MessageBox.Show("Name cannot be blank");
                        }
                        controller.CreateFolder(getName.Name, tvReportServer.SelectedNode);

                        toolStripStatusLabel.Text = String.Format("Created folder '{0}'", getName.Name);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("An error occured: {0}", ex.Message));
                        LogHandler.WriteLogEntry(ex);  
                    }
                }

                try
                {
                    controller.PopulateItems(tvReportServer.SelectedNode.ToolTipText);
                }
                catch (Exception ex)
                {
                    LogHandler.WriteLogEntry(ex);
                    MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                }
            }
        }

        /// <summary>
        /// Upload files
        /// </summary>
        private void FilesToolStripMenuItemClick(object sender, EventArgs e)
        {
            openFileDialog.Filter = REPORT_FILTER_STRING + "|" + DATASET_FILTER_STRING + "|" + MODEL_FILTER_STRING;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var overwrite = MessageBox.Show("Overwrite existing items?", "Upload", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

                Cursor = Cursors.WaitCursor;

                foreach (var filename in openFileDialog.FileNames)
                {
                    var destinationFolder = tvReportServer.SelectedNode.ToolTipText;

                    try
                    {
                        if (Path.GetExtension(filename) == MODEL_FILEEXTENSION)
                        {
                            controller.CreateModel(filename, destinationFolder, overwrite);
                        }
                        else
                        {
                            controller.UploadReport(filename, destinationFolder, overwrite);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHandler.WriteLogEntry(ex);
                        MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                    }
                }

                try
                {
                    controller.PopulateItems(tvReportServer.SelectedNode.ToolTipText);
                }
                catch (Exception ex)
                {
                    LogHandler.WriteLogEntry(ex);
                    MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                }

                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Upload folder
        /// </summary>
        private void FolderToolStripMenuItemClick(object sender, EventArgs e)
        {
            var folderSelector = new FolderBrowserDialog();
            folderSelector.ShowNewFolderButton = false;
            folderSelector.SelectedPath = Properties.Settings.Default.LastSelectedFolder;

            if (folderSelector.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.LastSelectedFolder = folderSelector.SelectedPath.ToString();
                Properties.Settings.Default.Save();

                var overwrite = MessageBox.Show("Overwrite existing items?", "Upload", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

                Cursor = Cursors.WaitCursor;

                try
                {
                    controller.UploadFolder(folderSelector.SelectedPath, tvReportServer.SelectedNode.ToolTipText, overwrite, tvReportServer.SelectedNode);
                    controller.PopulateItems(tvReportServer.SelectedNode.ToolTipText);
                }
                catch (Exception ex)
                {
                    LogHandler.WriteLogEntry(ex);
                    MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                }

                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        private void DeleteToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;

                foreach (ListViewItem item in lvItems.SelectedItems)
                {
                    try
                    {
                        controller.DeleteItem(item.ToolTipText);

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
                    catch (Exception ex)
                    {
                        LogHandler.WriteLogEntry(ex);
                        MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                    }
                }

                Cursor = Cursors.Default;

                controller.PopulateItems(tvReportServer.SelectedNode.ToolTipText);
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
            var parts = path.Split('/');
            return path.Split('/')[parts.Length - 1];
        }

        public static string GetItemPath(string path, bool forFileSystem)
        {
            path = path.Substring(0, path.LastIndexOf("/", StringComparison.Ordinal));
            
            return forFileSystem ? path.Replace("/", @"\") : path;
        }
        #endregion


        /// <summary>
        /// Displays the server-window
        /// </summary>
        private void ShowSelectServer()
        {
            using (var servers = new FormServers())
            {
                servers.ShowDialog();

                if (servers.DialogResult != DialogResult.Cancel)
                {
                    SelectedServer = servers.SelectedServer;
                }
            }

            BuildConnectSubMenu();
        }


        /// <summary>
        /// Exit application
        /// </summary>
        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        private void SettingsToolStripMenuItemClick(object s, EventArgs e)
        {
            ShowSelectServer();
        }

        /// <summary>
        /// Rename an item
        /// </summary>
        private void RenameToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (var getName = new FormGetName("Rename item"))
            {
                getName.Name = lvItems.SelectedItems[0].Text;
                if (getName.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        controller.MoveItem(lvItems.SelectedItems[0].ToolTipText,
                                            GetItemPath(lvItems.SelectedItems[0].ToolTipText, false) + "/" +
                                            getName.Name, (ReportItemTypes) lvItems.SelectedItems[0].Tag);
                        toolStripStatusLabel.Text = String.Format("Renamed item '{0}' to '{1}'",
                                                                  lvItems.SelectedItems[0].Text,
                                                                  GetItemPath(lvItems.SelectedItems[0].ToolTipText,
                                                                              false) + "/" + getName.Name);

                        if ((ReportItemTypes) lvItems.SelectedItems[0].Tag == ReportItemTypes.Folder)
                        {
                            controller.PopulateTreeView(SelectedServer.Alias);
                        }

                        controller.PopulateItems(tvReportServer.SelectedNode.ToolTipText);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                        LogHandler.WriteLogEntry(ex);
                    }
                }
            }
        }

        /// <summary>
        /// Display properties of selected item
        /// </summary>
        private void PropertiesToolStripMenuItemClick(object sender, EventArgs e)
        {
            var frmProperties = new FormProperties(lvItems.SelectedItems[0].ToolTipText, (ReportItemTypes)lvItems.SelectedItems[0].Tag);
            frmProperties.ShowDialog();
        }

        /// <summary>
        /// Set the datasource of a report
        /// Check if any of a report datasources matches the selected datasource. If so update that datasource, otherwise 
        /// provide information about the report datasources
        /// </summary>
        private void SetDatasourceToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (var ssrsExplorer = new FormSSRSSItemSelector(ViewItems.Datasources))
            {
                if (ssrsExplorer.ShowDialog() == DialogResult.OK && ssrsExplorer.SelectedItemPath != null)
                {
                    foreach (ListViewItem item in lvItems.SelectedItems)
                    {
                        try
                        {
                            controller.SetDatasource(item.ToolTipText, ssrsExplorer.SelectedItemPath,
                                                     (ReportItemTypes) item.Tag);
                        }
                        catch (Exception ex)
                        {
                            LogHandler.WriteLogEntry(ex);
                            MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Move reports and folders
        /// </summary>
        private void MoveToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (var ssrsExplorer = new FormSSRSSItemSelector(ViewItems.Folders))
            {
                if (ssrsExplorer.ShowDialog() == DialogResult.OK && ssrsExplorer.SelectedItemPath != null)
                {
                    Cursor = Cursors.WaitCursor;

                    foreach (ListViewItem item in lvItems.SelectedItems)
                    {
                        try
                        {
                            controller.MoveItem(item.ToolTipText,
                                                ssrsExplorer.SelectedItemPath.Trim('/') + "/" + item.Text,
                                                (ReportItemTypes) item.Tag);
                            toolStripStatusLabel.Text = String.Format("Moved item '{0}' to {1}", item.Text,
                                                                      ssrsExplorer.SelectedItemPath);
                            Application.DoEvents();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                            LogHandler.WriteLogEntry(ex);
                        }
                    }

                    try
                    {
                        controller.PopulateTreeView(SelectedServer.Alias);
                        controller.PopulateItems(tvReportServer.SelectedNode.ToolTipText);
                    }
                    catch (Exception ex)
                    {
                        LogHandler.WriteLogEntry(ex);
                        MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                    }

                    Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Download reports, alo includes any subfolders
        /// </summary>
        private void DownloadToolStripMenuItemClick(object sender, EventArgs e)
        {
            using(var folderSelector = new FolderBrowserDialog())
            {
                folderSelector.SelectedPath = Properties.Settings.Default.LastSelectedFolder;

                if (folderSelector.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.LastSelectedFolder = folderSelector.SelectedPath.ToString();
                    Properties.Settings.Default.Save();

                    var askToPreserveFolders = lvItems.SelectedItems.Cast<ListViewItem>().Any(item => (ReportItemTypes) item.Tag == ReportItemTypes.Folder);

                    var preserveFolders = askToPreserveFolders && 
                        MessageBox.Show("Preserve folders?", "Download items", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                    
                    Cursor = Cursors.WaitCursor;

                    foreach (ListViewItem item in lvItems.SelectedItems)
                    {
                        try
                        {
                            controller.DownloadItem(item.Text, item.ToolTipText, folderSelector.SelectedPath,
                                                    (ReportItemTypes) item.Tag, preserveFolders);
                        }
                        catch (Exception ex)
                        {
                            LogHandler.WriteLogEntry(ex);
                            MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                        }
                    }

                    Cursor = Cursors.Default;
                }
            }
        }

        private void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (var frmAbout = new About())
            {
                frmAbout.ShowDialog(this);
            }
        }


        private void BuildConnectSubMenu()
        {
            serverCollection = ServerRegistry.GetServerSettings();

            connectToolStripMenuItem.DropDownItems.Clear();

            foreach (ServerSettingsConfigElement configEl in serverCollection)
            {
                var newItem = new ToolStripMenuItem(configEl.Alias, null, ConnectToolStripMenuItemClick);
                connectToolStripMenuItem.DropDownItems.Add(newItem);
            }
        }

        private void ConnectToolStripMenuItemClick(object sender, EventArgs e)
        {
            SelectedServer = serverCollection.Get(((ToolStripMenuItem)sender).Text);
            Connect();
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
                    try
                    {
                        controller = ReportingServicesFactory.CreateFromSettings(SelectedServer, tvReportServer, toolStripStatusLabel, lvItems);
                    }
                    catch (Exception ex)
                    {
                        LogHandler.WriteLogEntry(ex);
                        MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                    }
                }

                try
                {
                    controller.PopulateTreeView(SelectedServer.Alias);
                    toolStripStatusLabel.Text = String.Format("Connected to {0}", SelectedServer.Url);

                    Text = "SSRS Explorer - connected to " + SelectedServer.Alias;
                }
                catch(Exception ex)
                {
                    Text = "SSRS Explorer - not connected";

                    toolStripStatusLabel.Text = "Not connected";
                    
                    MessageBox.Show("Cannot connect. Check server settings.", "Initialisation failed.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    
                    controller = null;

                    LogHandler.WriteLogEntry(ex, String.Format("Error connecting to {0}:", SelectedServer.Url));   
                }
            }
            else
            {
                ShowSelectServer();
            }

        }

        private void CreateDatasourceToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (var frmDatasource = new FormDatasource())
            {
                frmDatasource.Extensions = controller.GetDataExtensions();

                if (frmDatasource.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        controller.CreateDataSource(frmDatasource.Datasource, tvReportServer.SelectedNode.ToolTipText);
                        controller.PopulateItems(tvReportServer.SelectedNode.ToolTipText);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                        LogHandler.WriteLogEntry(ex);
                    }
                }
            }
        }

        private void EditToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (var frmDatasource = new FormDatasource())
            {
                frmDatasource.Extensions = controller.GetDataExtensions();

                var ds = controller.GetDatasource(lvItems.SelectedItems[0].ToolTipText);
                ds.Name = lvItems.SelectedItems[0].Text;
                frmDatasource.Datasource = ds;

                if (frmDatasource.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        controller.CreateDataSource(frmDatasource.Datasource, tvReportServer.SelectedNode.ToolTipText);
                        controller.PopulateItems(tvReportServer.SelectedNode.ToolTipText);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                        LogHandler.WriteLogEntry(ex);
                    }
                }
            }
        }

        private void ReplaceModelToolStripMenuItemClick(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = MODEL_FILTER_STRING;
            openFileDialog.ShowDialog();

            var fileName = openFileDialog.FileName;

            if (fileName != String.Empty)
            {
                var existingModelPath = lvItems.SelectedItems[0].ToolTipText;
                var newModelSMDL = File.ReadAllText(fileName);

                if (MessageBox.Show("Do you want to perform a compatibility check before replacing?", "Perform compatibility check?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var form = new FormDependantItems(controller, existingModelPath, newModelSMDL);

                    form.ShowDialog();
                }

                if (MessageBox.Show("Do you want to replace the existing model?", "Replace model", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        controller.ReplaceModel(fileName, existingModelPath);
                    }
                    catch (Exception ex)
                    {
                        LogHandler.WriteLogEntry(ex);
                        MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
                    }
                }
            }
        }

        private void AddPermissionsForUserToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                controller.AddPolicyForMyReports(lvItems.SelectedItems[0].ToolTipText, lvItems.SelectedItems[0].Text, new FormSetPolicy());
            }
            catch (Exception ex)
            {
                LogHandler.WriteLogEntry(ex);
                MessageBox.Show(String.Format("An error has occured: {0}", ex.Message));
            }
        }
    }
}