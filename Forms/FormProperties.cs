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
    public partial class FormProperties : Form
    {
        private string path;
        private ReportItemTypes itemType;
        private Controller rs; 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_path">path of selected item</param>
        /// <param name="_rs">instance of ReportService class</param>
        /// <param name="_type">type of the selected item</param> 
        public FormProperties(string _path, ReportItemTypes _type)
        {
            InitializeComponent();

            rs = ReportingServicesFactory.CreateFromSettings(FormSSRSExplorer.SelectedServer,null,null,null);

            path = _path;
            itemType = _type; 
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        /// <summary>
        /// Display item properties
        /// </summary>
        private void FormProperties_Shown(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                foreach (List<string> properties in rs.GetItemProperties(path))
                {
                    ListViewItem lvi = new ListViewItem(); 
                    foreach (string property in properties)
                    {
                        if (properties.IndexOf(property) == 0)
                        {
                            lvi.Text = property; 
                        }
                        else
                        {
                            lvi.SubItems.Add(property);   
                        }
                    }
                    lvProperties.Items.Add(lvi);   
                }

                Application.DoEvents();

                bool inheritsParent;
                Dictionary<string,string[]> itemSecurity = rs.GetItemSecurity(path, out inheritsParent);
                foreach (string userName in itemSecurity.Keys )
                {
                    ListViewItem visibleUsername = new ListViewItem();


                    visibleUsername.Text = userName;
                    visibleUsername.SubItems.Add(String.Join(",", itemSecurity[userName]));

                    lvPermissions.Items.Add(visibleUsername);
                }

                if (inheritsParent)
                    lblInheritsPermissions.Text = "Inherited from parent object";
                else
                    lblInheritsPermissions.Text = "";

                Application.DoEvents();

                if (itemType == ReportItemTypes.Report)
                {
                    foreach (string datasource in rs.GetReportDatasources(path))
                    {
                        lvDataSources.Items.Add(datasource);
                    }

                    Application.DoEvents();

                    foreach (List<String> parameters in rs.GetReportParameters(path))
                    {
                        ListViewItem lvi = new ListViewItem();
                        foreach (string parameter in parameters)
                        {
                            if (parameters.IndexOf(parameter) == 0)
                            {
                                lvi.Text = parameter;
                            }
                            else
                            {
                                lvi.SubItems.Add(parameter);
                            }
                        }
                        lvParameters.Items.Add(lvi);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("An exception occured while retrieving item information: {0}", ex.Message));  
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
   }
}