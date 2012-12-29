using System;
using System.Windows.Forms;

namespace ReportingServerManager.Forms
{
    public partial class FormSelectFilesystemFolder : Form
    {
        public FormSelectFilesystemFolder()
        {
            InitializeComponent();
        }

        public string Foldername
        {
            get
            {
                return txtFoldername.Text;
            }
        }

        public bool EnableCreateNewFolder
        {
            set
            {
                folderBrowserDialog.ShowNewFolderButton = value;  
            }
        }

        private void BtnSelectFolderClick(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = txtFoldername.Text == "" ? @"C:\" : txtFoldername.Text;   
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtFoldername.Text = folderBrowserDialog.SelectedPath;   
            }
        }

        private void FrmSelectFilesystemFolderLoad(object sender, EventArgs e)
        {
            txtFoldername.Text = @"C:\"; 
        }
    }
}