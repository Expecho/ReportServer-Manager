using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using ReportingServerManager.Logic.Configuration;

namespace ReportingServerManager.Logic
{
    class ReportingServicesFactory
    {
        public static Controller CreateFromSettings(ServerSettingsConfigElement config, TreeView tvReportServer, ToolStripStatusLabel lbl, ListView lv)
        {
            var controller = new Controller(tvReportServer, lbl, lv)
                                 {
                                     RsFacade = CreateFacadeFromSettings(config)
                                 };

            return controller;
        }

        private static IRSFacade CreateFacadeFromSettings(ServerSettingsConfigElement config)
        {
            var facadeImplementationType =
                Assembly.GetCallingAssembly()
                    .GetTypes()
                    .ToList()
                    .Where(type => type.Namespace == "ReportingServerManager.Logic")
                    .FirstOrDefault(GetTypeSpecification(config));

            if (facadeImplementationType == null)
                throw new NotSupportedException(String.Format("No implementation for SQL Server version '{0}'", config.SQLServerVersion));

            var facade = (IRSFacade)Activator.CreateInstance(facadeImplementationType);

            facade.BaseUrl = config.IsSharePointMode ? config.ReportLibrary : "/";
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

        private static Func<Type, bool> GetTypeSpecification(ServerSettingsConfigElement config)
        {
            var version = String.Empty;

            switch (config.SQLServerVersion)
            {
                case "2000":
                    version = "2000";
                    break;
                case "2005":
                case "2008":
                    version = "2005";
                    break;
                case "2008R2":
                case "2012":
                    version = "2005";
                    break;
            }

            var name = String.Format("RS{0}{1}Facade",
                                     version,
                                     config.IsSharePointMode ? "SharePoint" : String.Empty);

            return type => type.Name == name;
        }
    }
}
