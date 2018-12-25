using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ReportingServerManager.Logic.Shared;

namespace ReportingServerManager.Forms
{
    public partial class FormDatasource : Form
    {
        public FormDatasource()
        {
            InitializeComponent();
        }

        #region Properties
        private Datasource datasource;

        public Datasource Datasource
        {
            get
            {
                datasource.ConnectionString = txtConnStr.Text;
                datasource.Prompt = txtCredentialsPrompt.Text;
                datasource.Name = txtName.Text;
                datasource.Username = radStored.Checked ? txtUsername.Text : null;
                datasource.Password = radStored.Checked ? txtPassword.Text : null;
                datasource.Enabled = chkEnabled.Checked;
                datasource.SetExecutionContext = chkExecutionContext.Checked;
                datasource.UsePromptedCredentialsAsWindowsCredentials = chkUsePromptedCredentialsAsWindowsCredentials.Checked;
                datasource.UseStoredCredentialsAsWindowsCredentials = chkUsePromptedCredentialsAsWindowsCredentials.Checked;
                datasource.Extension = string.Empty;

                if (radNone.Checked) datasource.CredentialRetrievalType = CredentialRetrievalTypes.None;
                if (radStored.Checked) datasource.CredentialRetrievalType = CredentialRetrievalTypes.Store;
                if (radPrompt.Checked) datasource.CredentialRetrievalType = CredentialRetrievalTypes.Prompt;
                if (radWindowsAuthentication.Checked) datasource.CredentialRetrievalType = CredentialRetrievalTypes.Integrated;

                if (cmbExtensions.SelectedIndex != -1)
                {
                    foreach (var extension in extensions)
                    {
                        if (extension.FriendlyName == cmbExtensions.SelectedItem.ToString())
                        {
                            datasource.Extension = extension.Name;
                            break;
                        }
                    }
                }

                return datasource;
            }
            set
            {
                if (value.CredentialRetrievalType == CredentialRetrievalTypes.Integrated)
                    radWindowsAuthentication.Checked = true;
                if (value.CredentialRetrievalType == CredentialRetrievalTypes.None)
                    radNone.Checked = true;
                if (value.CredentialRetrievalType == CredentialRetrievalTypes.Store)
                    radStored.Checked = true;
                if (value.CredentialRetrievalType == CredentialRetrievalTypes.Prompt)
                    radPrompt.Checked = true;

                txtConnStr.Text = value.ConnectionString;
                txtCredentialsPrompt.Text = value.Prompt;
                txtName.Text = value.Name;
                txtUsername.Text = value.CredentialRetrievalType == CredentialRetrievalTypes.Store ? value.Username : String.Empty;
                txtPassword.Text = value.CredentialRetrievalType == CredentialRetrievalTypes.Store ? value.Password : String.Empty;
                chkEnabled.Checked = value.Enabled;
                chkExecutionContext.Checked = value.SetExecutionContext;
                chkUsePromptedCredentialsAsWindowsCredentials.Checked = value.UsePromptedCredentialsAsWindowsCredentials;
                chkUseStoredCredentialsAsWindowsCredentials.Checked = value.UseStoredCredentialsAsWindowsCredentials;
             
                foreach (var extension in extensions)
                {
                    if (value.Extension == extension.Name && cmbExtensions.Items.IndexOf(extension.FriendlyName) != -1)
                    {
                        cmbExtensions.SelectedItem = extension.FriendlyName;
                    }
                }
            }
        }

        private List<DatasourceExtension> extensions;

        public List<DatasourceExtension> Extensions
        {
            set
            {
                extensions = value;

                foreach (var extension in extensions)
                {
                    cmbExtensions.Items.Add(extension.FriendlyName);
                }

                cmbExtensions.SelectedIndex = 0;
            }
        }

        #endregion

        private void BtnOkClick(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Please fill in a name for the datasource");
            }
            else
            {
                Close();
            }
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            Close();
        }

        private void RadAuthenticationCheckedChanged(object sender, EventArgs e)
        {
            switch (((RadioButton)sender).Name)
            {
                case "radPrompt":
                    datasource.CredentialRetrievalType = CredentialRetrievalTypes.Prompt;
                    txtUsername.Text = null;
                    txtPassword.Text = null;
                    chkUseStoredCredentialsAsWindowsCredentials.Checked = false;
                    chkExecutionContext.Checked = false;
                    break;
                case "radStored":
                    datasource.CredentialRetrievalType = CredentialRetrievalTypes.Store;
                    chkUsePromptedCredentialsAsWindowsCredentials.Checked = false;
                    txtCredentialsPrompt.Text = "";
                    break;
                case "radNone":
                    datasource.CredentialRetrievalType = CredentialRetrievalTypes.None;
                    txtUsername.Text = null;
                    txtPassword.Text = null;
                    chkUseStoredCredentialsAsWindowsCredentials.Checked = false;
                    chkExecutionContext.Checked = false;
                    chkExecutionContext.Checked = false;
                    chkUsePromptedCredentialsAsWindowsCredentials.Checked = false;
                    txtCredentialsPrompt.Text = null;
                    break;
                case "radWindowsAuthentication":
                    datasource.CredentialRetrievalType = CredentialRetrievalTypes.Integrated;
                    txtUsername.Text = null;
                    txtPassword.Text = null;
                    chkUseStoredCredentialsAsWindowsCredentials.Checked = false;
                    chkExecutionContext.Checked = false;
                    chkUsePromptedCredentialsAsWindowsCredentials.Checked = false;
                    txtCredentialsPrompt.Text = null;
                    break;
            }

            txtCredentialsPrompt.Enabled = datasource.CredentialRetrievalType == CredentialRetrievalTypes.Prompt;
            chkUsePromptedCredentialsAsWindowsCredentials.Enabled = datasource.CredentialRetrievalType == CredentialRetrievalTypes.Prompt;
            txtUsername.Enabled = datasource.CredentialRetrievalType == CredentialRetrievalTypes.Store;
            txtPassword.Enabled = datasource.CredentialRetrievalType == CredentialRetrievalTypes.Store;
            chkUseStoredCredentialsAsWindowsCredentials.Enabled = datasource.CredentialRetrievalType == CredentialRetrievalTypes.Store;
            chkExecutionContext.Enabled = datasource.CredentialRetrievalType == CredentialRetrievalTypes.Store;
        }
    }
}