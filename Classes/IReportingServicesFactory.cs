using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;  

namespace RSS_Report_Retrievers
{
    interface IReportingServicesFactory
    {
        void PopulateTreeView();
        void PopulateItems(string path);
        TreeNode CreateFolder(string name, TreeNode parent);
        void UploadItem(string filename, string destination, bool overwrite);
        void UploadFolder(string path, string destination, bool overwrite, TreeNode parent);
        void DeleteItem(string path);
        void SetDatasource(string report, string datasource, ItemTypes type);
        void MoveItem(string source, string destination, ItemTypes type);
        List<List<String>> GetReportParameters(string path);
        List<String> GetReportDatasources(string path);
        List<List<String>> GetItemProperties(string path);
        List<List<String>> GetItemSecurity(string path);
        void DownloadItem(string path, string destination, ItemTypes type, bool preserveFolders);

        ViewItems ViewItem { get; set; }
    }
}
