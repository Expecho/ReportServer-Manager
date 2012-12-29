using System;
using System.Windows.Forms;
using ReportingServerManager.Logic;
using ReportingServerManager.Logic.Shared;

namespace ReportingServerManager.Forms
{
    public partial class FormSSRSSItemSelector : Form
    {
        private readonly ViewItems viewItem = ViewItems.Folders;
        private readonly Controller controller;
        
        #region Properties
        /// <summary>
        /// Gets the path on the report server of the selected item
        /// </summary>
        public string SelectedItemPath
        {
            get
            {
                return tvReportServer.SelectedNode == null ? null : tvReportServer.SelectedNode.ToolTipText;
            }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="viewItems"></param>
        public FormSSRSSItemSelector(ViewItems viewItems)
        {
            InitializeComponent();
            viewItem = viewItems;

            try
            {
                controller = ReportingServicesFactory.CreateFromSettings(FormSSRSExplorer.SelectedServer, tvReportServer, null, null);
            }
            catch (Exception ex)
            {
                LogHandler.WriteLogEntry(ex);
            }
            
            controller.ViewItem = viewItem; 
        }

        private void FrmSSRSSExplorerShown(object sender, EventArgs e)
        {
            try
            {
                controller.PopulateTreeView(FormSSRSExplorer.SelectedServer.Alias);
            }
            catch (Exception ex)
            {
                LogHandler.WriteLogEntry(ex);
            }
        }
        
        /// <summary>
        /// Close the form and accept the double clicked TreeNode as the selected item
        /// </summary>
        private void TvReportServerMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && tvReportServer.GetNodeAt(e.Location) != null)
            {
                tvReportServer.SelectedNode = tvReportServer.GetNodeAt(e.Location);
                DialogResult = DialogResult.OK;  
                Close(); 
            }
        }

        /// <summary>
        /// Make sure a valid item is selected
        /// </summary>
        private void FrmSSRSSExplorerFormClosing(object sender, FormClosingEventArgs e)
        {
            if ((sender as FormSSRSSItemSelector).DialogResult == DialogResult.OK)
            {
                if (tvReportServer.SelectedNode == null)
                {
                    MessageBox.Show(String.Format("Please select a {0}.", viewItem == ViewItems.Folders ? "folder" : "datasource"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Cancel = true;
                }
                else if (viewItem == ViewItems.Datasources && (ReportItemTypes)tvReportServer.SelectedNode.Tag != ReportItemTypes.Datasource && (ReportItemTypes)tvReportServer.SelectedNode.Tag != ReportItemTypes.Model)
                {
                    MessageBox.Show("Please select a datasource.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Cancel = true;
                }
                else if (viewItem == ViewItems.Folders && (ReportItemTypes)tvReportServer.SelectedNode.Tag != ReportItemTypes.Folder)
                {
                    MessageBox.Show("Please select a folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Cancel = true;
                }
            }
        }

        private void FrmSSRSSExplorerLoad(object sender, EventArgs e)
        {
            Text = viewItem == ViewItems.Folders ? "Select a folder" : "Select a datasource";
        }
    }
}