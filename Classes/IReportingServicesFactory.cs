using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using RSS_Report_Retrievers.Classes;

namespace RSS_Report_Retrievers
{
    class ReportingServicesFactory
    {
        public static Controller CreateFromSettings(Classes.ServerSettingsConfigElement config, TreeView tvReportServer, ToolStripStatusLabel lbl, ListView lv)
        {
            Controller controller = new Controller(tvReportServer, lbl, lv);

            controller.RsFacade = CreateFacadeFromSettings(config);

            return controller;
        }

        private static IRSFacade CreateFacadeFromSettings(Classes.ServerSettingsConfigElement config)
        {
            IRSFacade facade;

            if (config.IsSharePointMode)
            {
                facade = new RS2005SharePointFacade();
                facade.BaseUrl = config.ReportLibrary;
                facade.PathIncludesExtension = true;
            }
            else
            {
                if (config.IsSQL2000)
                    facade = new RS2000Facade();
                else
                    facade = new RS2005Facade();

                facade.BaseUrl = "/";
                facade.PathIncludesExtension = false;
            }

            facade.WebServiceUrl = config.Url; 

            // use windows authentication when indicated
            if (config.UseWindowsAuth)
            {
                facade.Credentials = System.Net.CredentialCache.DefaultCredentials;
            }
            else
            {
                facade.Credentials = new System.Net.NetworkCredential(
                        config.WindowsUsername,
                        config.WindowsPwd,
                        config.WindowsDomain
                );
            }

            return facade;
        }
    }


}
