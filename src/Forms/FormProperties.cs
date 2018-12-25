using System;
using System.Windows.Forms;
using RSS_Report_Retrievers;
using ReportingServerManager.Logic;
using ReportingServerManager.Logic.Shared;

namespace ReportingServerManager.Forms
{
    public partial class FormProperties : Form
    {
        private readonly string path;
        private readonly ReportItemTypes itemType;
        private readonly Controller controller;

        public FormProperties(string path, ReportItemTypes itemType)
        {
            InitializeComponent();

            controller = ReportingServicesFactory.CreateFromSettings(FormSSRSExplorer.SelectedServer,null,null,null);

            this.path = path;
            this.itemType = itemType; 
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            Close(); 
        }

        /// <summary>
        /// Display item properties
        /// </summary>
        private void FormPropertiesShown(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                foreach (var properties in controller.GetItemProperties(path))
                {
                    var lvi = new ListViewItem();
                    foreach (var property in properties)
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
                var itemSecurity = controller.GetItemSecurity(path, out inheritsParent);
                foreach (var userName in itemSecurity.Keys)
                {
                    var visibleUsername = new ListViewItem
                                              {
                                                  Text = userName
                                              };


                    visibleUsername.SubItems.Add(String.Join(",", itemSecurity[userName]));

                    lvPermissions.Items.Add(visibleUsername);
                }

                lblInheritsPermissions.Text = inheritsParent ? "Inherited from parent object" : string.Empty;

                Application.DoEvents();

                if (itemType != ReportItemTypes.Report) 
                    return;
                
                foreach (var datasource in controller.GetReportDatasources(path))
                {
                    lvDataSources.Items.Add(datasource);
                }

                Application.DoEvents();

                foreach (var parameters in controller.GetReportParameters(path))
                {
                    var lvi = new ListViewItem();
                    foreach (var parameter in parameters)
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