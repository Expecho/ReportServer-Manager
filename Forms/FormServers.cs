using System;
using System.Windows.Forms;
using RSS_Report_Retrievers;
using ReportingServerManager.Logic.Configuration;

namespace ReportingServerManager.Forms
{
    public partial class FormServers : Form
    {
        public ServerSettingsConfigElement SelectedServer = null;

        public FormServers()
        {
            InitializeComponent();
        }

        private void FormServersLoad(object sender, EventArgs e)
        {
            ReloadServerlist();
        }
        
        private void ReloadServerlist()
        {
            bsRegistredServers.DataSource = ServerRegistry.GetServerSettings();
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAddClick(object sender, EventArgs e)
        {
           ShowSettingsForm(new ServerSettingsConfigElement());
        }

        private void ShowSettingsForm(ServerSettingsConfigElement setting)
        {
            using (var form = new FormSettings(setting.Alias != string.Empty))
            {
                form.CurrentSetting = setting;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    ServerRegistry.AddElement(setting);

                    ServerRegistry.StoreSettings();
                }
            }

            ReloadServerlist();
        }

        private void BtnEditClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selIndex = dataGridView1.SelectedRows[0].Index;
                var setting = bsRegistredServers[selIndex] as ServerSettingsConfigElement;

                if (setting != null)
                {
                    ShowSettingsForm(setting.Clone());
                }
            }
        }

        private void DataGridView1SelectionChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = true;
        }

        private void BtnRemoveClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selIndex = dataGridView1.SelectedRows[0].Index;
                var setting = bsRegistredServers[selIndex] as ServerSettingsConfigElement;

                if (setting != null)
                {
                    ServerRegistry.RemoveElement(setting);
                    ServerRegistry.StoreSettings();
                }
            }

            ReloadServerlist();
        }
    }
}