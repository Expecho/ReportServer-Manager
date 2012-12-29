using System;
using System.Collections.Generic;
using System.Net;
using ReportingServerManager.Logic.Shared;

namespace ReportingServerManager.Logic
{
    public class RS2008Facade : FacadeBase, IRSFacade
    {

        public void CreateFolder(string folder, string parent, string properties)
        {
            throw new NotImplementedException();
        }

        public ReportWarning[] CreateReport(string filename, string destination, bool overwrite, byte[] definition, string properties)
        {
            throw new NotImplementedException();
        }

        public ReportWarning[] CreateModel(string filename, string folder, byte[] definition, string properties)
        {
            throw new NotImplementedException();
        }

        public ICredentials Credentials
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string WebServiceUrl
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void DeleteItem(string path)
        {
            throw new NotImplementedException();
        }

        public List<List<string>> GetItemProperties(string path)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string[]> GetItemSecurity(string path, out bool inheritsParentSecurity)
        {
            throw new NotImplementedException();
        }

        public byte[] GetModelDefinition(string path)
        {
            throw new NotImplementedException();
        }

        public List<string> GetReportDatasources(string path)
        {
            throw new NotImplementedException();
        }

        public byte[] GetReportDefinition(string path)
        {
            throw new NotImplementedException();
        }

        public List<List<string>> GetReportParameters(string path)
        {
            throw new NotImplementedException();
        }

        public ReportItemDTO[] ListChildren(string item, bool recursive)
        {
            throw new NotImplementedException();
        }

        public void MoveItem(string source, string destination, ReportItemTypes type)
        {
            throw new NotImplementedException();
        }

        public void SetItemDataSources(string item, string dataSourceName)
        {
            throw new NotImplementedException();
        }

        public void SetItemSecurity(string itemPath, Dictionary<string, string[]> policies)
        {
            throw new NotImplementedException();
        }

        public void CreateDataSource(Datasource datasource, string parent)
        {
            throw new NotImplementedException();
        }

        public List<DatasourceExtension> GetDataExtensions()
        {
            throw new NotImplementedException();
        }

        public Datasource GetDatasource(string path)
        {
            throw new NotImplementedException();
        }

        public List<ReportItemDTO> ListDependantItems(string reportModelpath)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ListRoles()
        {
            throw new NotImplementedException();
        }
    }
}
