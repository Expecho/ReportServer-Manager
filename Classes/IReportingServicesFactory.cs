using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using RSS_Report_Retrievers.Classes;

namespace RSS_Report_Retrievers
{
    public interface IController
    {
        void PopulateTreeView();
        void PopulateItems(string path);
        TreeNode CreateFolder(string name, TreeNode parent);
        void UploadReport(string filename, string destinationFolder, bool overwrite);
        void ReplaceModel(string filename, string itemToReplace);
        void UploadFolder(string path, string destination, bool overwrite, TreeNode parent);
        void DeleteItem(string path);
        void SetDatasource(string report, string datasource, ReportItemTypes type);
        void MoveItem(string source, string destination, ReportItemTypes type);
        List<List<String>> GetReportParameters(string path);
        List<String> GetReportDatasources(string path);
        List<List<String>> GetItemProperties(string path);
        List<List<String>> GetItemSecurity(string path);
        byte[] GetReport(string path);
        void DownloadItem(string path, string destination, ReportItemTypes type, bool preserveFolders);
        void CreateDataSource(Datasource datasource, string path);
        List<DatasourceExtension> GetDataExtensions();
        ViewItems ViewItem { get; set; }
        Datasource GetDatasource(string path);
        List<ReportItemDTO> ListDependantItems(string modelPath);
    }

    class ReportingServicesFactory
    {
        public static IController CreateFromSettings(Classes.ServerSettingsConfigElement config, TreeView tvReportServer, ToolStripStatusLabel lbl, ListView lv)
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
            }
            else
            {
                if (config.IsSQL2000)
                    facade = new RS2000Facade();
                else
                    facade = new RS2005Facade();

                facade.BaseUrl = "/";
            }

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
