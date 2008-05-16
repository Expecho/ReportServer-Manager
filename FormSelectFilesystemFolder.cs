using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RSS_Report_Retrievers
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

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = txtFoldername.Text == "" ? @"C:\" : txtFoldername.Text;   
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtFoldername.Text = folderBrowserDialog.SelectedPath;   
            }
        }

        private void frmSelectFilesystemFolder_Load(object sender, EventArgs e)
        {
            txtFoldername.Text = @"C:\"; 
        }
    }
}