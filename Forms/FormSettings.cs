using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RSS_Report_Retrievers
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            global::RSS_Report_Retrievers.Properties.Settings.Default.Save();
            this.Close();  
        }

        private void chkWindowsAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            SetControlState();
        }

        private void SetControlState()
        {
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
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}