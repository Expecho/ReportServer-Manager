using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RSS_Report_Retrievers.Classes;

namespace RSS_Report_Retrievers
{
    public partial class FormSSRSSItemSelector : Form
    {
        private ViewItems viewItem = ViewItems.Folders;
        private Controller rs;
        
        #region Properties
        public string SelectedItemPath
        {
            get
            {
                return tvReportServer.SelectedNode == null ? null : tvReportServer.SelectedNode.ToolTipText;
            }
        }
        #endregion

        public FormSSRSSItemSelector(ViewItems viewItems)
        {
            InitializeComponent();
            viewItem = viewItems;

            try
            {
                rs = ReportingServicesFactory.CreateFromSettings(FormSSRSExplorer.SelectedServer, tvReportServer, null, null);
            }
            catch (Exception ex)
            {
                LogHandler.WriteLogEntry(ex);
            }
            
            rs.ViewItem = viewItem; 
        }

        private void frmSSRSSExplorer_Shown(object sender, EventArgs e)
        {
            try
            {
                rs.PopulateTreeView(FormSSRSExplorer.SelectedServer.Alias);
            }
            catch (Exception ex)
            {
                LogHandler.WriteLogEntry(ex);
            }
        }
        
        /// <summary>
        /// Close the form and accept the double clicked TreeNode as the selected item
        /// </summary>
        private void tvReportServer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && tvReportServer.GetNodeAt(e.Location) != null)
            {
                tvReportServer.SelectedNode = tvReportServer.GetNodeAt(e.Location);
                this.DialogResult = DialogResult.OK;  
                this.Close(); 
            }
        }

        /// <summary>
        /// Make sure a valid item is selected
        /// </summary>
        private void frmSSRSSExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((sender as FormSSRSSItemSelector).DialogResult == DialogResult.OK)
            {
                if (tvReportServer.SelectedNode == null)
                {
                    MessageBox.Show(String.Format("Please select a {0}.", viewItem == ViewItems.Folders ? "folder" : "datasource"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Cancel = true;
                }
                else if (viewItem == ViewItems.Datasources && (ReportItemTypes)tvReportServer.SelectedNode.Tag != ReportItemTypes.Datasource && (ReportItemTypes)tvReportServer.SelectedNode.Tag != ReportItemTypes.model)
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

        private void frmSSRSSExplorer_Load(object sender, EventArgs e)
        {
            this.Text = viewItem == ViewItems.Folders ? "Select a folder" : "Select a datasource";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

    }
}