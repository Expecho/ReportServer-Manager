using System;
using System.Net;
using System.Windows.Forms;
using ReportingServerManager.Logic.Configuration;

namespace ReportingServerManager.Logic
{
    internal class ReportingServicesFactory
    {
        internal static Controller CreateFromSettings(ServerSettingsConfigElement config, TreeView tvReportServer,
                                                      ToolStripStatusLabel lbl, ListView lv)
        {
            var controller = new Controller(tvReportServer, lbl, lv)
                                 {
                                     RsFacade = CreateFacadeFromSettings(config)
                                 };

            return controller;
        }

        internal static IRSFacade CreateFacadeFromSettings(ServerSettingsConfigElement config)
        {
            IRSFacade facade = null;

            switch (config.SQLServerVersion)
            {
                case "2000":
                    facade = new RS2000Facade();
                    break;
                case "2005":
                case "2008":
                    facade = config.IsSharePointMode ? new RS2005SharePointFacade() : (IRSFacade) new RS2005Facade();
                    break;
                case "2008R2":
                case "2012":
                    facade = new RS2008Facade();
                    break;
            }

            if (facade == null)
            {
                throw new NotSupportedException("Invalid config");
            }

            facade.BaseUrl = config.IsSharePointMode ? config.ReportLibrary : "/";
            facade.NativeMode = !config.IsSharePointMode;
            facade.SiteUrl = config.IsSharePointMode ? config.ReportLibrary : null;
            facade.PathIncludesExtension = config.IsSharePointMode;
            facade.WebServiceUrl = config.Url;
            facade.Credentials = config.UseWindowsAuth
                                     ? CredentialCache.DefaultCredentials
                                     : new NetworkCredential(
                                           config.WindowsUsername,
                                           config.WindowsPwd,
                                           config.WindowsDomain
                                           );

            return facade;
        }
    }
}
