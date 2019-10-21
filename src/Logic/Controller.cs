using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using ReportingServerManager.Forms;
using ReportingServerManager.Logic.Shared;

namespace ReportingServerManager.Logic
{
    public class Controller
    {
        public IRSFacade RsFacade;
        private readonly TreeView tvReportServer;
        private readonly ToolStripStatusLabel toolStripStatusLabel;
        private readonly ListView lvItems;
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
        public void PopulateTreeView(string serverAlias)
        {
            tvReportServer.Nodes.Clear();

            var root = new TreeNode(serverAlias)
            {
                Name = "/",
                ToolTipText = RsFacade.BaseUrl,
                Tag = ReportItemTypes.Folder
            };

            tvReportServer.Nodes.Add(root);

            if (toolStripStatusLabel != null)
            {
                toolStripStatusLabel.Text = "Retrieving server information....";
                Application.DoEvents();
            }

            try
            {
                ExpandNodeContent(root, true);
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

        public void ExpandNodeContent(TreeNode parent, bool recurseSubfolders)
        {
            foreach (var item in RsFacade.ListChildren(parent.ToolTipText, false))
            {
                if (item.Type == ReportItemTypes.Folder && (viewItem == ViewItems.Folders || viewItem == ViewItems.All || viewItem == ViewItems.Datasources))
                {
                    var folder = new TreeNode(item.Name)
                    {
                        Name = item.Name,
                        ImageIndex = 3,
                        Tag = ReportItemTypes.Folder,
                        ToolTipText = item.Path
                    };
                    parent.Nodes.Add(folder);

                    if (recurseSubfolders)
                        ExpandNodeContent(folder, true);
                }
                else if (item.Type == ReportItemTypes.Datasource && (viewItem == ViewItems.Datasources || viewItem == ViewItems.All))
                {
                    var datasource = new TreeNode(item.Name)
                    {
                        Name = item.Name,
                        ImageIndex = 2,
                        Tag = ReportItemTypes.Datasource,
                        ToolTipText = item.Path
                    };

                    parent.Nodes.Add(datasource);
                }
                else if (item.Type == ReportItemTypes.Report && viewItem == ViewItems.All)
                {
                    var report = new TreeNode(item.Name)
                    {
                        Name = item.Name,
                        ImageIndex = 5,
                        Tag = ReportItemTypes.Report,
                        ToolTipText = item.Path
                    };
                    parent.Nodes.Add(report);
                }
                else if (item.Type == ReportItemTypes.Model && (viewItem == ViewItems.Datasources || viewItem == ViewItems.All))
                {
                    var model = new TreeNode(item.Name)
                    {
                        Name = item.Name,
                        ImageIndex = 0,
                        Tag = ReportItemTypes.Model,
                        ToolTipText = item.Path
                    };

                    parent.Nodes.Add(model);
                }

            }
        }
        #endregion

        #region Populate Server Items
        public void PopulateItems(string path)
        {
            lvItems.Items.Clear();

            foreach (var item in RsFacade.ListChildren(path, false))
            {
                var lvi = new ListViewItem
                {
                    Text = item.Name,
                    ToolTipText = item.Path
                };

                switch (item.Type)
                {
                    case ReportItemTypes.Folder:
                        lvi.ImageIndex = 3;
                        lvi.Tag = ReportItemTypes.Folder;

                        break;
                    case ReportItemTypes.Report:
                        lvi.ImageIndex = 5;
                        lvi.Tag = ReportItemTypes.Report;
                        break;
                    case ReportItemTypes.Datasource:
                        lvi.Tag = ReportItemTypes.Datasource;
                        lvi.ImageIndex = 2;
                        break;
                    case ReportItemTypes.Model:
                        lvi.Tag = ReportItemTypes.Model;
                        lvi.ImageIndex = 0;
                        break;
                    case ReportItemTypes.Dataset:
                        lvi.Tag = ReportItemTypes.Dataset;
                        lvi.ImageIndex = 1;
                        break;
                    case ReportItemTypes.MobileReport:
                        lvi.Tag = ReportItemTypes.MobileReport;
                        lvi.ImageIndex = 6;
                        break;
                }
                lvItems.Items.Add(lvi);
            }
            //Sort the listview to display folders first
            lvItems.ListViewItemSorter = new ListViewSorter();
            lvItems.Sorting = SortOrder.Ascending;
        }
        #endregion

        #region Delete
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

        #region New/Create
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

            var folder = new TreeNode(name)
            {
                Name = name,
                ImageIndex = 4,
                Tag = ReportItemTypes.Folder
            };

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

        public void CreateDataSource(Datasource datasource, string path)
        {
            try
            {
                RsFacade.CreateDataSource(datasource, path);

                toolStripStatusLabel.Text = String.Format("Updated / Created new datasource '{0}'", datasource.Name);
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("An error occured: {0}", ex.Message));
            }

        }

        public void AddPolicyForMyReports(string userName, IEnumerable<string> roles)
        {
            var currentItem = new ReportItemDTO();

            try
            {
                currentItem.Path = "/Users Folders";
                var usersFolders = RsFacade.ListChildren("/Users Folders", false);

                foreach (var item in usersFolders)
                {
                    currentItem = item;

                    if (item.Type == ReportItemTypes.Folder)
                    {
                        var itemPath = item.Path + "/My reports";
                        bool inheritsDummy;
                        var existingPolicies = RsFacade.GetItemSecurity(itemPath, out inheritsDummy);

                        RemoveExistingUser(existingPolicies, userName);

                        existingPolicies.Add(userName, roles.ToArray());

                        RsFacade.SetItemSecurity(itemPath, existingPolicies);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Current Item:{0}\r\n{1}", currentItem.Path, ex));
            }
        }

        private void RemoveExistingUser(Dictionary<string, string[]> existingPolicies, string userName)
        {
            if (existingPolicies != null && existingPolicies.ContainsKey(userName))
            {
                existingPolicies.Remove(userName);
            }
        }


        public void AddPolicyForMyReports(string itemPath, string visibleName, IFormSetPolicy policyDialog)
        {
            policyDialog.Init(RsFacade.ListRoles(), visibleName);

            if (policyDialog.ShowDialog() != DialogResult.OK)
                return;

            AddPolicyForMyReports(policyDialog.UserName, policyDialog.SelectedRoles);

            MessageBox.Show("MyReports-folders updated!");
        }
        #endregion

        #region Upload
        public void UploadFolder(string path, string destination, bool overwrite, TreeNode parent)
        {
            foreach (var filename in Directory.GetFiles(path, "*.rdl", SearchOption.TopDirectoryOnly))
            {
                UploadReport(filename, destination, overwrite);
            }

            foreach (var filename in Directory.GetFiles(path, "*.rsd", SearchOption.TopDirectoryOnly))
            {
                UploadReport(filename, destination, overwrite);
            }

            foreach (var foldername in Directory.GetDirectories(path))
            {
                UploadFolder(foldername, destination.TrimEnd('/') + "/" + Path.GetFileName(foldername), overwrite, CreateFolder(Path.GetFileName(foldername), parent));
            }
        }

        public void ReplaceModel(string filename, string itemToReplace)
        {
            var definition = GetBytesFromFile(filename);

            var visibleName = Path.GetFileName(itemToReplace);

            var datasource = RsFacade.GetReportDatasources(itemToReplace);

            var dependantItems = RsFacade.ListDependantItems(itemToReplace);

            RsFacade.DeleteItem(itemToReplace);

            var destinationFolder = Path.GetDirectoryName(itemToReplace)
                                            .Replace('\\', '/');


            RsFacade.CreateModel(visibleName, destinationFolder, definition, null);

            if (datasource != null && datasource.Count > 0)
                RsFacade.SetItemDataSources(itemToReplace, datasource[0]);

            UpdateDatasourceForDependantItems(dependantItems, itemToReplace);

            toolStripStatusLabel.Text = String.Format("Uploaded item {0}", Path.GetFileName(filename));
            Application.DoEvents();
        }

        private void UpdateDatasourceForDependantItems(IEnumerable<ReportItemDTO> dependantItems, string itemToReplace)
        {
            foreach (var report in dependantItems)
            {
                RsFacade.SetItemDataSources(report.Path, itemToReplace);
            }
        }

        public void CreateModel(string filename, string destinationFolder, bool overwrite)
        {
            var def = GetBytesFromFile(filename);
            var visibleName = Path.GetFileNameWithoutExtension(filename);

            RsFacade.CreateModel(visibleName, destinationFolder, def, null);
        }

        public void UploadReport(string filename, string destinationFolder, bool overwrite)
        {
            try
            {
                var definition = GetBytesFromFile(filename);

                var visibleName = Path.GetFileName(filename);

                if (Path.GetExtension(filename) == ".rdl")
                    RsFacade.CreateReport(visibleName, destinationFolder, overwrite, definition, null);
                else
                    RsFacade.CreateDataset(visibleName, destinationFolder, overwrite, definition, null);


                toolStripStatusLabel.Text = String.Format("Uploaded item {0}", Path.GetFileName(filename));
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("An error has occured: {0}", ex.Message));
            }
        }

        internal static Byte[] GetBytesFromFile(string filename)
        {
            using (var stream = File.OpenRead(filename))
            {
                var definition = new Byte[stream.Length];

                stream.Read(definition, 0, (int)stream.Length);

                return definition;
            }
        }
        #endregion

        #region Set Datasource
        public void SetDatasource(string item, string datasource, ReportItemTypes type)
        {
            switch (type)
            {
                case ReportItemTypes.Folder:
                    foreach (var catalogItem in RsFacade.ListChildren(item, true))
                    {
                        SetDatasource(catalogItem.Path, datasource, catalogItem.Type);
                    }
                    break;
                case ReportItemTypes.Report:
                case ReportItemTypes.Dataset:
                case ReportItemTypes.MobileReport:
                case ReportItemTypes.Model:

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
        public void MoveItem(string source, string destination, ReportItemTypes type)
        {
            RsFacade.MoveItem(source, destination, type);
        }
        #endregion

        #region Get Item Information
        public List<List<String>> GetReportParameters(string path)
        {
            return RsFacade.GetReportParameters(path);
        }

        public List<String> GetReportDatasources(string path)
        {
            return RsFacade.GetReportDatasources(path);
        }

        public List<List<String>> GetItemProperties(string path)
        {

            return RsFacade.GetItemProperties(path);
        }

        public Dictionary<string, string[]> GetItemSecurity(string path, out bool inheritsParent)
        {
            return RsFacade.GetItemSecurity(path, out inheritsParent);
        }
        #endregion

        public IEnumerable<string> ListRoles()
        {
            return RsFacade.ListRoles();
        }
        #region Download
        public void DownloadItem(string folderName, string path, string destinationFolder, ReportItemTypes type, bool preserveFolders)
        {
            switch (type)
            {
                case ReportItemTypes.Folder:

                    if (preserveFolders)
                    {
                        destinationFolder = destinationFolder + "\\" + folderName;
                        if (!Directory.Exists(destinationFolder))
                            Directory.CreateDirectory(destinationFolder);
                    }

                    foreach (var catalogItem in RsFacade.ListChildren(path, true))
                    {
                        DownloadItem(catalogItem.Name, catalogItem.Path, destinationFolder, catalogItem.Type, preserveFolders);
                    }
                    break;

                case ReportItemTypes.Dataset:
                case ReportItemTypes.Report:
                case ReportItemTypes.MobileReport:

                    var definition = new XmlDocument();

                    definition.Load(new MemoryStream(RsFacade.GetReportDefinition(path)));

                    SaveItem(path, type, destinationFolder, definition);

                    break;

                case ReportItemTypes.Model:
                    var model = new XmlDocument();

                    model.Load(new MemoryStream(RsFacade.GetModelDefinition(path)));

                    SaveItem(path, type, destinationFolder, model);

                    break;
            }

            toolStripStatusLabel.Text = String.Format("Downloaded '{0}'", path);
            Application.DoEvents();
        }

        private string AppendFileSuffix(string path, ReportItemTypes type)
        {
            var filename = path;

            switch (type)
            {
                case ReportItemTypes.Model:
                    filename = filename + ".smdl";
                    break;
                case ReportItemTypes.Dataset:
                    filename = filename + ".rsd";
                    break;
                case ReportItemTypes.MobileReport:
                    filename = filename + ".rsmobile";
                    break;
                case ReportItemTypes.Report:
                    filename = filename + ".rdl";
                    break;
            }

            return filename;
        }

        private void SaveItem(string filename, ReportItemTypes type, string destination, XmlDocument definition)
        {
            if (!RsFacade.PathIncludesExtension)
            {
                filename = AppendFileSuffix(filename, type);
            }

            //if (preserveFolders)
            //{
            //    var sourceBaseDirectory = tvReportServer.SelectedNode.ToolTipText;

            //    var relativeFilePath = Path.GetDirectoryName(filename.Substring(sourceBaseDirectory.Length));

            //    destination = Path.Combine(destination, relativeFilePath);
            //    destination = destination.Replace('/', '\\');
            //}

            filename = Path.GetFileName(filename);

            //if (!Directory.Exists(destination))
            //{
            //    Directory.CreateDirectory(destination);
            //}

            definition.Save(String.Format(@"{0}\{1}", destination, filename));

        }

        public List<ReportItemDTO> ListDependantItems(string modelPath)
        {
            return RsFacade.ListDependantItems(modelPath);
        }

        public byte[] GetReport(string path)
        {
            return RsFacade.GetReportDefinition(path);
        }

        public List<DatasourceExtension> GetDataExtensions()
        {
            return RsFacade.GetDataExtensions();
        }

        public Datasource GetDatasource(string path)
        {
            return RsFacade.GetDatasource(path);
        }
        #endregion



    }

    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class ListViewSorter : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            var lviX = (ListViewItem)x;
            var lviY = (ListViewItem)y;

            if ((lviX.Tag.Equals(ReportItemTypes.Folder) && lviY.Tag.Equals(ReportItemTypes.Folder)) || ((!lviX.Tag.Equals(ReportItemTypes.Folder) && !lviY.Tag.Equals(ReportItemTypes.Folder))))
                return System.Collections.CaseInsensitiveComparer.Default.Compare(lviX.Text, lviY.Text);
            else if (lviX.Tag.Equals(ReportItemTypes.Folder))
                return -1;
            else
                return 1;
        }
    }
}
