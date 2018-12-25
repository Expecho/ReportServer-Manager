using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Forms;

namespace ReportingServerManager.Forms
{
    public partial class FormSetPolicy : Form, IFormSetPolicy
    {
        private readonly List<string> selectedRoles = new List<string>();

        public Dictionary<string, string[]> PolicyList { get; set; }

        public string UserName
        {
            get { return txtUsername.Text; }
        }

        public List<string> SelectedRoles
        {
            get { return selectedRoles; }
        }

        public FormSetPolicy()
        {
            InitializeComponent();
        }

        public void Init(IEnumerable<string> availableRoles, string itemName)
        {
            groupBox.Text = "Adding user to " + itemName;

            txtUsername.Text = WindowsIdentity.GetCurrent().Name;

            listAvailableRoles.Items.Clear();

            foreach (var role in availableRoles)
            {
                listAvailableRoles.Items.Add(role);
            }

        }

        private void BtnAddToSelectedRolesClick(object sender, EventArgs e)
        {
            var selectedItems = new List<object>();
            foreach (var selectedItem in listAvailableRoles.SelectedItems)
            {
                listSelectedRoles.Items.Add(selectedItem);

                selectedItems.Add(selectedItem);
            }

            foreach (var selectedItem in selectedItems)
                listAvailableRoles.Items.Remove(selectedItem);
        }

        private void BtnRemoveFromSelectedRolesClick(object sender, EventArgs e)
        {
           var selectedItems = new List<object>();
           foreach (var selectedItem in listSelectedRoles.SelectedItems)
            {
                listAvailableRoles.Items.Add(selectedItem);
                selectedItems.Add(selectedItem);
            }

           foreach (var selectedItem in selectedItems)
               listSelectedRoles.Items.Remove(selectedItem);
        }

        private void BtnApplyClick(object sender, EventArgs e)
        {
            foreach (var selectedItem in listSelectedRoles.Items)
                SelectedRoles.Add(selectedItem.ToString());

            DialogResult = DialogResult.OK;

            Close();
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}