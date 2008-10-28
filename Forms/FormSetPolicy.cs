using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RSS_Report_Retrievers
{
    public interface IFormSetPolicy
    {
        void Init(System.Collections.Generic.IEnumerable<string> availableRoles, string itemName);
        string UserName { get; }
        DialogResult ShowDialog();
        List<string> SelectedRoles { get;}
    }

    public partial class FormSetPolicy : Form, IFormSetPolicy
    {
        public Dictionary<string, string[]> policyList = null;
        public List<string> selectedRoles = new List<string>();


        public string UserName
        {
            get { return txtUsername.Text; }
        }

        public List<string> SelectedRoles
        {
            get { return this.selectedRoles; }
        }

        public FormSetPolicy()
        {
            InitializeComponent();
        }

        public void Init(IEnumerable<string> availableRoles, string itemName)
        {
            this.groupBox.Text = "Adding user to " + itemName;

            this.txtUsername.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            listAvailableRoles.Items.Clear();

            foreach (string role in availableRoles)
            {
                listAvailableRoles.Items.Add(role);
            }

        }

        private void btnAddToSelectedRoles_Click(object sender, EventArgs e)
        {
            List<object> selectedItems = new List<object>();
            foreach (object o in listAvailableRoles.SelectedItems)
            {
                this.listSelectedRoles.Items.Add(o);

                selectedItems.Add(o);
            }

            foreach(object o in selectedItems)
                this.listAvailableRoles.Items.Remove(o);
        }

        private void btnRemoveFromSelectedRoles_Click(object sender, EventArgs e)
        {
            List<object> selectedItems = new List<object>();
            foreach (object o in this.listSelectedRoles.SelectedItems)
            {
                this.listAvailableRoles.Items.Add(o);
                selectedItems.Add(o);
            }
        
            foreach (object o in selectedItems)
                this.listSelectedRoles.Items.Remove(o);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            foreach (object o in this.listSelectedRoles.Items)
                SelectedRoles.Add(o.ToString());

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}