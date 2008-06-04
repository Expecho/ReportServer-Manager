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
        private IController rs; 

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

                foreach (List<string> permissions in rs.GetItemSecurity(path))
                {
                    ListViewItem lvi = new ListViewItem();
                    foreach (string permission in permissions)
                    {
                        if (permissions.IndexOf(permission) == 0)
                        {
                            lvi.Text = permission;
                        }
                        else
                        {
                            lvi.SubItems.Add(permission);
                        }
                    }
                    lvPermissions.Items.Add(lvi);
                }

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