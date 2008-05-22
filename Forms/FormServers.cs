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
    public partial class FormServers : Form
    {
        public ServerSettingsConfigElement SelectedServer = null;

        public FormServers()
        {
            InitializeComponent();
        }

        private void FormServers_Load(object sender, EventArgs e)
        {
            ReloadServerlist();
        }
        
        private void ReloadServerlist()
        {
            this.bsRegistredServers.DataSource = RSS_Report_Retrievers.Classes.ServerRegistry.GetServerSettings();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ServerSettingsConfigElement newEl = new ServerSettingsConfigElement();

            ShowSettingsForm(newEl);
        }

        private void ShowSettingsForm(Classes.ServerSettingsConfigElement setting)
        {
            FormSettings f = new FormSettings(setting.Alias != string.Empty);

            f.CurrentSetting = setting;

            f.ShowDialog();

            if (f.DialogResult == DialogResult.OK)
            {
                ServerRegistry.AddElement(setting);

                ServerRegistry.StoreSettings();
            }

            ReloadServerlist();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                int selIndex = this.dataGridView1.SelectedRows[0].Index;
                ServerSettingsConfigElement setting = (ServerSettingsConfigElement) bsRegistredServers[selIndex];

                ShowSettingsForm(setting.Clone());
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                int selIndex = this.dataGridView1.SelectedRows[0].Index;
                ServerSettingsConfigElement setting = (ServerSettingsConfigElement)bsRegistredServers[selIndex];

                ShowSettingsForm(setting);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                int selIndex = this.dataGridView1.SelectedRows[0].Index;
                ServerSettingsConfigElement setting = (ServerSettingsConfigElement)bsRegistredServers[selIndex];

                ServerRegistry.RemoveElement(setting);

                ServerRegistry.StoreSettings();
            }

            ReloadServerlist();
        }
    }
}