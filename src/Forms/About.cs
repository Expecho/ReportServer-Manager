using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace ReportingServerManager.Forms
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            label1.Text = String.Format("Version {0}", Assembly.GetExecutingAssembly().GetName().Version);
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            Close(); 
        }
    }
}