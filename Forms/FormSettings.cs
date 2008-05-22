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
    public partial class FormSettings : Form
    {
        private const string WS2000_SUFFIX = "ReportService.asmx";
        private const string WS2005or2008_SUFFIX = "ReportService2005.asmx";
        private const string WS2005_SHAREPOINT_SUFFIX = "ReportService2006.asmx";
        private const string WS2005_SHAREPOINT_DOCLIB_URL = "http://server/SiteDirectory/Rapportage/Rapporten";

        private ServerSettingsConfigElement currentSetting = null;

        public ServerSettingsConfigElement CurrentSetting
        {
            get { return currentSetting; }
            set { currentSetting = value; }
        }

        public FormSettings()
        {
            InitializeComponent();
        }

        public FormSettings(bool lockAliasTextfield)
            : this()
        {
            txtAlias.Enabled = !lockAliasTextfield;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void chkWindowsAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            SetControlState();
        }

        private void SetControlState()
        {
            this.bsServerSettings.DataSource = this.CurrentSetting;

            txtDomain.Enabled = !chkWindowsAuthentication.Checked;
            txtUsername.Enabled = !chkWindowsAuthentication.Checked;
            txtPassword.Enabled = !chkWindowsAuthentication.Checked;

        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            SetControlState();
            txtReportLibrary.Enabled = chkSharePointMode.Checked;   
        }

        private void chkSharePointMode_CheckedChanged(object sender, EventArgs e)
        {
            txtReportLibrary.Enabled = chkSharePointMode.Checked;

            if (chkSharePointMode.Checked)
                ChangeWSSuffix(WS2005_SHAREPOINT_SUFFIX);
            else
                ChangeWSSuffix(WS2005or2008_SUFFIX);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close(); 
        }

        private void ChangeWSSuffix(string newURLEnding)
        {
            int ix = txtURL.Text.LastIndexOf('/');

            if (ix > 0)
            {
                string urlStart = txtURL.Text.Substring(0, ix);

                txtURL.Text = urlStart + "/" + newURLEnding;
            }
        }

        private void ShowSharePointDocLibDetails(bool isVisible)
        {
            chkSharePointMode.Enabled = isVisible;
            txtReportLibrary.Enabled = isVisible;
        }

        private void chkSharePointMode_Click(object sender, EventArgs e)
        {
            ChangeWSSuffix(WS2005_SHAREPOINT_DOCLIB_URL);
        }

        private void rbSQL2000_Click(object sender, EventArgs e)
        {
            ShowSharePointDocLibDetails(false);
            ChangeWSSuffix(WS2000_SUFFIX);
        }

        private void rbSQL2005or2008_Click(object sender, EventArgs e)
        {
            ShowSharePointDocLibDetails(true);
            ChangeWSSuffix(WS2005or2008_SUFFIX);
        }
    }
}