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
    public partial class FormDatasource : Form
    {
        public FormDatasource()
        {
            InitializeComponent();
        }

        #region Properties
        private Datasource datasource = new Datasource();

        public Datasource  Datasource
        {
            get 
            {
                datasource.ConnectionString = txtConnStr.Text;
                datasource.Prompt = txtCredentialsPrompt.Text;
                datasource.Name = txtName.Text;
                datasource.Username = txtUsername.Text;   
                datasource.Password = txtPassword.Text;
                datasource.Enabled = chkEnabled.Checked;
                datasource.SetExecutionContext = chkExecutionContext.Checked;
                datasource.UsePromptedCredentialsAsWindowsCredentials = chkUsePromptedCredentialsAsWindowsCredentials.Checked;
                datasource.UseStoredCredentialsAsWindowsCredentials = chkUsePromptedCredentialsAsWindowsCredentials.Checked;
                datasource.Extension = "";

                if (cmbExtensions.SelectedIndex != -1)
                {
                    foreach (DatasourceExtension extension in extensions)
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
                txtConnStr.Text = value.ConnectionString;
                txtCredentialsPrompt.Text = value.Prompt;
                txtName.Text = value.Name;
                txtUsername.Text = value.Username; 
                txtPassword.Text = value.Password;
                chkEnabled.Checked = value.Enabled;
                chkExecutionContext.Checked = value.SetExecutionContext;
                chkUsePromptedCredentialsAsWindowsCredentials.Checked = value.UsePromptedCredentialsAsWindowsCredentials;
                chkUseStoredCredentialsAsWindowsCredentials.Checked = value.UseStoredCredentialsAsWindowsCredentials;

                foreach (DatasourceExtension extension in extensions)
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

                foreach (DatasourceExtension extension in extensions)
                {
                    cmbExtensions.Items.Add(extension.FriendlyName);
                }

                cmbExtensions.SelectedIndex = 0; 
            }
        }
	
	    #endregion

        private void btnOK_Click(object sender, EventArgs e)
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            switch (((RadioButton)sender).Name)
            {
                case "radPrompt":
                    datasource.CredentialRetrievalType = CredentialRetrievalTypes.Prompt;
                    txtUsername.Text = "";
                    txtPassword.Text = "";
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
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    chkUseStoredCredentialsAsWindowsCredentials.Checked = false;
                    chkExecutionContext.Checked = false;
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    chkUseStoredCredentialsAsWindowsCredentials.Checked = false;
                    chkExecutionContext.Checked = false;
                    chkUsePromptedCredentialsAsWindowsCredentials.Checked = false;
                    txtCredentialsPrompt.Text = ""; 
                    break;
                case "radWindowsAuthentication":
                    datasource.CredentialRetrievalType = CredentialRetrievalTypes.Integrated;
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    chkUseStoredCredentialsAsWindowsCredentials.Checked = false;
                    chkExecutionContext.Checked = false;
                    chkUsePromptedCredentialsAsWindowsCredentials.Checked = false;
                    txtCredentialsPrompt.Text = ""; 
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