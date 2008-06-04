using System;
using System.Collections.Generic;

namespace RSS_Report_Retrievers.Classes
{
    interface IRSFacade
    { 
        void CreateFolder(string Folder, string Parent, string properties);
        void CreateReport(string filename, string destination, bool overwrite, byte[] definition, string Properties,out ReportWarning[] warnings);
        System.Net.ICredentials Credentials { get; set; }
        void DeleteItem(string path);
        List<List<string>> GetItemProperties(string path);
        List<List<string>> GetItemSecurity(string path);
        byte[] GetModelDefinition(string path);
        List<string> GetReportDatasources(string path);
        byte[] GetReportDefinition(string path);
        List<List<string>> GetReportParameters(string path);
        ReportItemDTO[] ListChildren(string item, bool recursive);
        void MoveItem(string source, string destination, ReportItemTypes type);
        void SetItemDataSources(string item, string dataSourceName);
    }
}
