using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using ReportingServerManager.Logic.Shared;
using ReportingServerManager.RSS_2005_SHAREPOINT;

namespace ReportingServerManager.Logic
{
    class RS2005SharePointFacade : FacadeBase, IRSFacade
    {
        private readonly ReportingService2006 webserviceProxy = new ReportingService2006();

        public string WebServiceUrl
        {
            get { return webserviceProxy.Url; }
            set { webserviceProxy.Url = value; }
        }

        public void CreateFolder(string folder, string parent, string properties)
        {
            webserviceProxy.CreateFolder(folder, parent);
        }

        public ReportWarning[] CreateReport(string filename, string destination, bool overwrite, byte[] definition, string properties)
        {
            Warning[] warnings;
            webserviceProxy.CreateReport(Path.GetFileName(filename), destination, overwrite, definition, null, out warnings);
            
            return warnings != null ? Array.ConvertAll(warnings, ConvertSPWarningToReportWarning) : null;
        }

        public ReportWarning[] CreateModel(string filename, string destination,byte[] definition, string properties)
        {
            Warning[] warnings;
            webserviceProxy.CreateModel(Path.GetFileName(filename), destination, definition, null, out warnings);

            return Array.ConvertAll(warnings, ConvertSPWarningToReportWarning);
        }

        public ICredentials Credentials
        {
            get
            {
                return webserviceProxy.Credentials;
            }
            set
            {
                webserviceProxy.Credentials = value;
            }
        }

        public void DeleteItem(string path)
        {
            webserviceProxy.DeleteItem(path);
        }

        public List<List<string>> GetItemProperties(string path)
        {
            return webserviceProxy.GetProperties(path, null)
                .Select(property => new List<String>
                            {
                                property.Name, property.Value
                            })
                .ToList();
        }

        public Dictionary<string, string[]> GetItemSecurity(string path, out bool inheritsParentSecurity)
        {
            var permissions = new Dictionary<string, string[]>(StringComparer.CurrentCultureIgnoreCase);
            foreach (var permission in webserviceProxy.GetPolicies(path, out inheritsParentSecurity))
            {
                var roles = Array.ConvertAll(permission.Roles, r => r.Name);
                
                permissions.Add(permission.GroupUserName, roles);
            }

            return permissions;
        }

        public byte[] GetModelDefinition(string path)
        {
            return webserviceProxy.GetModelDefinition(path);
        }

        public List<string> GetReportDatasources(string path)
        {
            return webserviceProxy.GetItemDataSources(path).Select(ds => ds.Name).ToList();
        }

        public byte[] GetReportDefinition(string path)
        {
            return webserviceProxy.GetReportDefinition(path);
        }

        public List<List<string>> GetReportParameters(string path)
        {
            return webserviceProxy.GetReportParameters(path, null, null, null)
                .Select(parameter => new List<String>
                                {
                                    parameter.Name, parameter.Type.ToString(), parameter.AllowBlank.ToString(), parameter.Nullable.ToString(), parameter.MultiValue.ToString(), parameter.Prompt
                                })
                .ToList();
        }

        public ReportItemDTO[] ListChildren(string item, bool recursive)
        {
            return Array.ConvertAll(webserviceProxy.ListChildren(item), ConvertCatalogItemToReportItemDTO);
        }

        public IEnumerable<string> ListRoles()
        {
            return webserviceProxy.ListRoles(SecurityScopeEnum.Catalog,BaseUrl).Select(r => r.Name);
        }

        public void MoveItem(string source, string destination, ReportItemTypes type)
        {
            if (!destination.EndsWith(".rsds") && type == ReportItemTypes.Datasource)
            {
                destination += ".rsds";
            }
            else if (!destination.EndsWith(".rdl") && type == ReportItemTypes.Report)
            {
                destination += ".rdl";
            }

            webserviceProxy.MoveItem(source, destination);
        }

        public void SetItemDataSources(string item, string dataSourceName)
        {
            var reportDataSources = webserviceProxy.GetItemDataSources(item);

            foreach (var existingDataSource in reportDataSources)
            {
                var dsr = new DataSourceReference
                              {
                                  Reference = dataSourceName
                              };

                existingDataSource.Item = dsr;
            }

            webserviceProxy.SetItemDataSources(item, reportDataSources);
        }

        public void SetItemSecurity(string itemPath, Dictionary<string, string[]> policies, bool inherit = false)
        {
            webserviceProxy.SetPolicies(
                itemPath,
                policies.Keys.Select(userName => new Policy
                {
                    GroupUserName = userName,
                    Roles = Array.ConvertAll(policies[userName], roleName =>
                    {
                        var r = new Role { Name = roleName };
                        return r;
                    })
                })
                .Where(newPolicy => (!String.IsNullOrEmpty(newPolicy.GroupUserName)) && newPolicy.Roles.Length > 0)
                .ToArray());
        }

        public void CreateDataSource(Datasource datasource, string parent)
        {
            var dataSourceDefinition = new DataSourceDefinition
                          {
                              CredentialRetrieval =
                                  GetSSRSCredentialRetrievalTypeFromEnum(
                                      datasource.CredentialRetrievalType),
                              ConnectString = datasource.ConnectionString,
                              Enabled = datasource.Enabled,
                              Extension = datasource.Extension,
                              Prompt = datasource.Prompt
                          };

            if (dataSourceDefinition.CredentialRetrieval == CredentialRetrievalEnum.Store)
            {
                dataSourceDefinition.UserName = datasource.Username;
                dataSourceDefinition.Password = datasource.Password;
            }

            dataSourceDefinition.ImpersonateUser = datasource.SetExecutionContext;
            dataSourceDefinition.WindowsCredentials = datasource.UsePromptedCredentialsAsWindowsCredentials || datasource.UseStoredCredentialsAsWindowsCredentials;
            

            if (!datasource.Name.EndsWith(".rsds"))
            {
                datasource.Name += ".rsds"; 
            }

            webserviceProxy.CreateDataSource(datasource.Name, parent, true, dataSourceDefinition, null);
        }

        public List<DatasourceExtension> GetDataExtensions()
        {
            return webserviceProxy.ListExtensions(ExtensionTypeEnum.Data).Select(extension => new DatasourceExtension(extension.Name, extension.LocalizedName)).ToList();
        }

        public Datasource GetDatasource(string path)
        {
            var def = webserviceProxy.GetDataSourceContents(path);
            var ds = new Datasource
                         {
                             ConnectionString = def.ConnectString,
                             CredentialRetrievalType =
                                 GetCredentialRetrievalTypeFromSSRSType(def.CredentialRetrieval),
                             Enabled = def.Enabled,
                             Extension = def.Extension,
                             Username = def.UserName,
                             Password = def.Password,
                             Prompt = def.Prompt,
                             SetExecutionContext = def.ImpersonateUser,
                             UseStoredCredentialsAsWindowsCredentials = def.WindowsCredentials,
                             UsePromptedCredentialsAsWindowsCredentials = def.WindowsCredentials
                         };

            return ds;
        }

        public List<ReportItemDTO> ListDependantItems(string reportModel)
        {
            var response = webserviceProxy.ListDependentItems(reportModel);

            return response.Select(item => new ReportItemDTO
                                               {
                                                   Name = item.Name, Path = item.Path
                                               }).ToList();
        }

        private static CredentialRetrievalTypes GetCredentialRetrievalTypeFromSSRSType(CredentialRetrievalEnum type)
        {
            var convertedType = CredentialRetrievalTypes.None;

            switch (type)
            {
                case CredentialRetrievalEnum.Integrated: convertedType = CredentialRetrievalTypes.Integrated; break;
                case CredentialRetrievalEnum.None: convertedType = CredentialRetrievalTypes.None; break;
                case CredentialRetrievalEnum.Prompt: convertedType = CredentialRetrievalTypes.Prompt; break;
                case CredentialRetrievalEnum.Store: convertedType = CredentialRetrievalTypes.Store; break;
            }

            return convertedType;
        }

        private static ReportItemTypes GetReportItemTypeFromSSRSItemType(ItemTypeEnum ssrsType)
        {
            var convertedType = ReportItemTypes.Unknown;

            switch (ssrsType)
            {
                case ItemTypeEnum.Folder: convertedType = ReportItemTypes.Folder; break;
                case ItemTypeEnum.Report: convertedType = ReportItemTypes.Report; break;
                case ItemTypeEnum.DataSource: convertedType = ReportItemTypes.Datasource; break;
                case ItemTypeEnum.Model: convertedType = ReportItemTypes.Model; break;
            }

            return convertedType;
        }

        private static ReportItemDTO ConvertCatalogItemToReportItemDTO(CatalogItem item)
        {
            return new ReportItemDTO
                    {
                        Hidden = item.Hidden,
                        Name = item.Name,
                        Path = item.Path,
                        Type = GetReportItemTypeFromSSRSItemType(item.Type)
                    };
        }

        private static CredentialRetrievalEnum GetSSRSCredentialRetrievalTypeFromEnum(CredentialRetrievalTypes type)
        {
            var convertedType = CredentialRetrievalEnum.None;

            switch (type)
            {
                case CredentialRetrievalTypes.Integrated: convertedType = CredentialRetrievalEnum.Integrated; break;
                case CredentialRetrievalTypes.None: convertedType = CredentialRetrievalEnum.None; break;
                case CredentialRetrievalTypes.Prompt: convertedType = CredentialRetrievalEnum.Prompt; break;
                case CredentialRetrievalTypes.Store: convertedType = CredentialRetrievalEnum.Store; break;
            }

            return convertedType;
        }

        private static ReportWarning ConvertSPWarningToReportWarning(Warning warning)
        {
            return new ReportWarning
            {
                Code = warning.Code,
                Message = warning.Message,
                ObjectName = warning.ObjectName,
                ObjectType = warning.ObjectType,
                Severity = warning.Severity
            };
        }

        public ReportWarning[] CreateDataset(string filename, string destination, bool overwrite, byte[] definition, string properties)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
