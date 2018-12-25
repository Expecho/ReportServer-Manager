using System;
using System.Windows.Forms;
using ReportingServerManager.Logic.Configuration;

namespace ReportingServerManager.Forms
{
    public partial class FormSettings : Form
    {
        public ServerSettingsConfigElement CurrentSetting { get; set; }

        public FormSettings()
        {
            CurrentSetting = null;
            InitializeComponent();
        }

        public FormSettings(bool lockAliasTextfield)
            : this()
        {
            txtAlias.Enabled = !lockAliasTextfield;
        }

        private void BtnSaveClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ChkWindowsAuthenticationCheckedChanged(object sender, EventArgs e)
        {
            SetControlState();
        }

        private void SetControlState()
        {
            bsServerSettings.DataSource = CurrentSetting;

            txtDomain.Enabled = !chkWindowsAuthentication.Checked;
            txtUsername.Enabled = !chkWindowsAuthentication.Checked;
            txtPassword.Enabled = !chkWindowsAuthentication.Checked;

        }

        private void FrmSettingsLoad(object sender, EventArgs e)
        {
            SetControlState();
            txtReportLibrary.Enabled = chkSharePointMode.Checked;
        }

        private void ChkSharePointModeCheckedChanged(object sender, EventArgs e)
        {
            txtReportLibrary.Enabled = chkSharePointMode.Checked;
            txtReportLibrary.Text = chkSharePointMode.Checked ? "http://<servername>/sites/<sitename>/<library>" : String.Empty;

            switch (cmbServerVersion.SelectedItem.ToString())
            {
                case "2005":
                case "2008":
                    txtURL.Text = chkSharePointMode.Checked ? "http://<servername>/reportserver/reportservice2006.asmx" :  "http://<servername>/reportserver/reportservice2005.asmx";
                    break;
                case "2008R2":
                case "2012":
                    ShowSharePointDocLibDetails(true);
                    txtURL.Text = chkSharePointMode.Checked ? "http://<servername>/reportserver/reportservice2010.asmx" : "http://<servername>/reportserver/reportservice2010.asmx";
                    break;
            }
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ShowSharePointDocLibDetails(bool isVisible)
        {
            chkSharePointMode.Enabled = isVisible;
            txtReportLibrary.Enabled = isVisible && chkSharePointMode.Checked;
        }

        private void CmbServerVersionSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbServerVersion.SelectedItem.ToString())
            {
                case "2000":
                    ShowSharePointDocLibDetails(false);
                    txtURL.Text = "http://<servername>/reportserver/reportservice.asmx";
                    break;
                case "2005":
                case "2008":
                    ShowSharePointDocLibDetails(true);
                    txtURL.Text = "http://<servername>/reportserver/reportservice2005.asmx";
                    break;
                case "2008R2":
                case "2012":
                    ShowSharePointDocLibDetails(true);
                    txtURL.Text = "http://<servername>/reportserver/reportservice2010.asmx";
                    break;
            }
        }
    }
}