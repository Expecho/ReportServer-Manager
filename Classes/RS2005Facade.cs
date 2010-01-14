using System;
using System.Collections.Generic;
using System.Text;
using RSS_Report_Retrievers.RSS_2005_NATIVE;
using RSS_Report_Retrievers.Classes;

namespace RSS_Report_Retrievers.Classes
{
    public class RS2005Facade : IRSFacade
    {
        private ReportingService2005 rs = new ReportingService2005();

        private string baseUrl;

        public string BaseUrl
        {
            get { return baseUrl; }
            set { baseUrl = value; }
        }

        private bool pathIncludesExtension;

        public bool PathIncludesExtension
        {
            get { return pathIncludesExtension; }
            set { pathIncludesExtension = value; }
        }

        #region IRSFacade Members
        public void CreateFolder(string Folder, string Parent, string properties)
        {
            rs.CreateFolder(Folder, Parent, null);
        }

        public ReportWarning[] CreateReport(string filename, string destination, bool overwrite, Byte[] definition, string Properties)
        {
            Warning[] err = rs.CreateReport(System.IO.Path.GetFileNameWithoutExtension(filename), destination, overwrite, definition, null);
            if (err != null)
            {

                return System.Array.ConvertAll<Warning, ReportWarning>(err, ConvertSPWarningToReportWarning);
            }
            else
            {
                return null;
            }

        }

        public ReportWarning[] CreateModel(string visibleName, string parentFolder, Byte[] definition, string Properties)
        {
            return System.Array.ConvertAll<Warning, ReportWarning>(rs.CreateModel(visibleName, parentFolder, definition, null), ConvertSPWarningToReportWarning);
        }

        public System.Net.ICredentials Credentials
        {
            get { return rs.Credentials; }
            set { rs.Credentials = value; }
        }

        public void DeleteItem(string path)
        {
            rs.DeleteItem(path);
        }

        public List<List<String>> GetItemProperties(string path)
        {
            List<List<String>> properties = new List<List<string>>();
            foreach (Property property in rs.GetProperties(path, null))
            {
                List<String> propertieinfo = new List<String>();
                propertieinfo.Add(property.Name);
                propertieinfo.Add(property.Value);
                properties.Add(propertieinfo);
            }

            

            return properties;
        }

        public Dictionary<string,string[]> GetItemSecurity(string path, out bool inheritsParentSecurity)
        {
            Dictionary<string, string[]> permissions = new Dictionary<string, string[]>(StringComparer.CurrentCultureIgnoreCase);

            foreach (Policy permission in rs.GetPolicies(path, out inheritsParentSecurity))
            {
                string[] roles = Array.ConvertAll<Role, string>(permission.Roles, delegate(Role r) { return r.Name; });
                permissions.Add(permission.GroupUserName, roles);
            }

            return permissions;
        }

        public byte[] GetModelDefinition(string path)
        {
            return rs.GetModelDefinition(path);
        }

        public List<String> GetReportDatasources(string path)
        {
            List<String> datasources = new List<string>();
            foreach (DataSource ds in rs.GetItemDataSources(path))
            {
                RSS_Report_Retrievers.RSS_2005_NATIVE.DataSourceReference theRef = ds.Item as RSS_Report_Retrievers.RSS_2005_NATIVE.DataSourceReference;

                if(theRef != null)            
                    datasources.Add(theRef.Reference);
            }

            return datasources;
        }

        public byte[] GetReportDefinition(string path)
        {
            return rs.GetReportDefinition(path);
        }

        public List<List<String>> GetReportParameters(string path)
        {
            List<List<String>> parameters = new List<List<string>>();

            foreach (RSS_2005_NATIVE.ReportParameter parameter in rs.GetReportParameters(path, null, false, null, null))
            {
                List<String> parameterinfo = new List<String>();
                parameterinfo.Add(parameter.Name);
                parameterinfo.Add(parameter.Type.ToString());
                parameterinfo.Add(parameter.AllowBlank.ToString());
                parameterinfo.Add(parameter.Nullable.ToString());
                parameterinfo.Add(parameter.MultiValue.ToString());
                parameterinfo.Add(parameter.Prompt);
                parameters.Add(parameterinfo);
            }

            return parameters;
        }

        public ReportItemDTO[] ListChildren(string item, bool recursive)
        {
            CatalogItem[] catItems = rs.ListChildren(item, recursive);

            return Array.ConvertAll<CatalogItem, ReportItemDTO>(catItems, ConvertCatalogItemToReportItemDTO);
        }

        public void MoveItem(string source, string destination, ReportItemTypes type)
        {
            rs.MoveItem(source, destination.StartsWith("/") ? destination : "/" + destination);
        }

        public void SetItemDataSources(string item, string dataSourceName)
        {
            DataSource[] reportDataSources = rs.GetItemDataSources(item);

            foreach (DataSource existingDataSource in reportDataSources)
            {
                DataSourceReference dsr = new DataSourceReference();
                dsr.Reference = dataSourceName;

                existingDataSource.Item = dsr;
            }

            rs.SetItemDataSources(item, reportDataSources);
        }
        public void SetItemSecurity(string itemPath, Dictionary<string,string[]> policies)
        {
            List<Policy> policyList = new List<Policy>();

            foreach (string userName in policies.Keys)
            {
                Policy newPolicy = new Policy();
                newPolicy.GroupUserName = userName;

                newPolicy.Roles = Array.ConvertAll<string, Role>(policies[userName], delegate(string roleName) { Role r = new Role(); r.Name = roleName; return r; });
                
                if((!String.IsNullOrEmpty(newPolicy.GroupUserName)) &&
                    newPolicy.Roles.Length > 0)
                    policyList.Add(newPolicy);
            }

            rs.SetPolicies(itemPath, policyList.ToArray());
        }

        public void CreateDataSource(Datasource datasource, string parent)
        {
            DataSourceDefinition def = new DataSourceDefinition();
            def.ConnectString = datasource.ConnectionString;
            def.Enabled = datasource.Enabled;
            def.Extension = datasource.Extension;
            def.Password = datasource.Password;
            def.Prompt = datasource.Prompt;
            def.UserName = datasource.Username;
            def.ImpersonateUser = datasource.SetExecutionContext;
            def.WindowsCredentials = datasource.UsePromptedCredentialsAsWindowsCredentials || datasource.UseStoredCredentialsAsWindowsCredentials;
            def.CredentialRetrieval = GetSSRSCredentialRetrievalTypeFromEnum(datasource.CredentialRetrievalType);

            rs.CreateDataSource(datasource.Name, parent, true, def, null);
        }

        public List<DatasourceExtension> GetDataExtensions()
        {
            List<DatasourceExtension> extensions = new List<DatasourceExtension>();

            foreach (Extension extension in rs.ListExtensions(ExtensionTypeEnum.Data))
            {
                extensions.Add(new DatasourceExtension(extension.Name, extension.LocalizedName));
            }

            return extensions;
        }

        public Datasource GetDatasource(string path)
        {
            DataSourceDefinition def = rs.GetDataSourceContents(path);
            Datasource ds = new Datasource();
            ds.ConnectionString = def.ConnectString;
            ds.CredentialRetrievalType = GetCredentialRetrievalTypeFromSSRSType(def.CredentialRetrieval);
            ds.Enabled = def.Enabled;
            ds.Extension = def.Extension;
            ds.Username = def.UserName;
            ds.Password = def.Password;
            ds.Prompt = def.Prompt;
            ds.SetExecutionContext = def.ImpersonateUser;
            ds.UseStoredCredentialsAsWindowsCredentials = def.WindowsCredentials;
            ds.UsePromptedCredentialsAsWindowsCredentials = def.WindowsCredentials;

            return ds;
        }

        public List<ReportItemDTO> ListDependantItems(string reportModelpath)
        {
            List<ReportItemDTO> items = new List<ReportItemDTO>();

            CatalogItem[] response = rs.ListDependentItems(reportModelpath);

            foreach (CatalogItem item in response)
            {
                ReportItemDTO dto = new ReportItemDTO();

                dto.Name = item.Name;
                dto.Path = item.Path;

                items.Add(dto);
            }

            return items;
        }

        public IEnumerable<string> ListRoles()
        {
            foreach (Role r in rs.ListRoles(SecurityScopeEnum.Catalog))
                yield return r.Name;
            
        }

        public List<ReportItemDTO> LoadDependantItems(string reportModelpath)
        {
            throw new Exception("The method or operation is not implemented.");
        }



        #endregion
        private static ReportItemTypes GetReportItemTypeFromSSRSItemTyp(ItemTypeEnum ssrsType)
        {
            ReportItemTypes convertedType = ReportItemTypes.Unknown;

            switch (ssrsType)
            {
                case ItemTypeEnum.Folder: convertedType = ReportItemTypes.Folder; break;
                case ItemTypeEnum.Report: convertedType = ReportItemTypes.Report; break;
                case ItemTypeEnum.DataSource: convertedType = ReportItemTypes.Datasource; break;
                case ItemTypeEnum.Model: convertedType = ReportItemTypes.model; break;
            }

            return convertedType;
        }

        private static CredentialRetrievalEnum GetSSRSCredentialRetrievalTypeFromEnum(CredentialRetrievalTypes type)
        {
            CredentialRetrievalEnum convertedType = CredentialRetrievalEnum.None; 

            switch (type)
            {
                case CredentialRetrievalTypes.Integrated: convertedType = CredentialRetrievalEnum.Integrated; break;
                case CredentialRetrievalTypes.None: convertedType = CredentialRetrievalEnum.None; break;
                case CredentialRetrievalTypes.Prompt: convertedType = CredentialRetrievalEnum.Prompt; break;
                case CredentialRetrievalTypes.Store: convertedType = CredentialRetrievalEnum.Store; break;
            }

            return convertedType;
        }

        private static CredentialRetrievalTypes GetCredentialRetrievalTypeFromSSRSType(CredentialRetrievalEnum type)
        {
            CredentialRetrievalTypes convertedType = CredentialRetrievalTypes.None;

            switch (type)
            {
                case CredentialRetrievalEnum.Integrated: convertedType = CredentialRetrievalTypes.Integrated; break;
                case CredentialRetrievalEnum.None: convertedType = CredentialRetrievalTypes.None; break;
                case CredentialRetrievalEnum.Prompt: convertedType = CredentialRetrievalTypes.Prompt; break;
                case CredentialRetrievalEnum.Store: convertedType = CredentialRetrievalTypes.Store; break;
            }

            return convertedType;
        }

        private static ReportItemDTO ConvertCatalogItemToReportItemDTO(CatalogItem item)
        {
            ReportItemDTO returnItem;

            returnItem.Hidden = item.Hidden;
            returnItem.Name = item.Name;
            returnItem.Path = item.Path;
            returnItem.Type = GetReportItemTypeFromSSRSItemTyp(item.Type);

            return returnItem;
        }

        private static ReportWarning ConvertSPWarningToReportWarning(Warning w)
        {

            ReportWarning returnWarning=new ReportWarning();

            if (w != null)
            {
                returnWarning.Code = w.Code;
                returnWarning.Message = w.Message;
                returnWarning.ObjectName = w.ObjectName;
                returnWarning.ObjectType = w.ObjectType;
                returnWarning.Severity = w.Severity;
            }
            return returnWarning;
        }

    
    }
}
