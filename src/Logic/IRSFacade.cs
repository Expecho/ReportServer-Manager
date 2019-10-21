using System.Collections.Generic;
using System.Net;
using ReportingServerManager.Logic.Shared;

namespace ReportingServerManager.Logic
{
    public interface IRSFacade
    {
        void CreateFolder(string folder, string parent, string properties);
        ReportWarning[] CreateReport(string filename, string destination, bool overwrite, byte[] definition, string properties);
        ReportWarning[] CreateDataset(string filename, string destination, bool overwrite, byte[] definition, string properties);
        ReportWarning[] CreateModel(string filename, string folder, byte[] definition, string properties);
        ICredentials Credentials { get; set; }
        string BaseUrl { get; set; }
        string WebServiceUrl { get; set; }
        bool PathIncludesExtension { get; set; }
        bool NativeMode { get; set; }
        string SiteUrl { get; set; }
        void DeleteItem(string path);
        List<List<string>> GetItemProperties(string path);
        Dictionary<string, string[]> GetItemSecurity(string path, out bool inheritsParentSecurity);
        byte[] GetModelDefinition(string path);
        List<string> GetReportDatasources(string path);
        byte[] GetReportDefinition(string path);
        List<List<string>> GetReportParameters(string path);
        ReportItemDTO[] ListChildren(string item, bool recursive);
        void MoveItem(string source, string destination, ReportItemTypes type);
        void SetItemDataSources(string item, string dataSourceName);
        void SetItemSecurity(string itemPath, Dictionary<string, string[]> policies, bool inherit = false);
        void CreateDataSource(Datasource datasource, string parent);
        List<DatasourceExtension> GetDataExtensions();
        Datasource GetDatasource(string path);
        List<ReportItemDTO> ListDependantItems(string reportModelpath);
        IEnumerable<string> ListRoles();
    }
}
