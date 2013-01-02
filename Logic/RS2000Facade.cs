using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using ReportingServerManager.Logic.Shared;
using ReportingServerManager.RSS_2000;

namespace ReportingServerManager.Logic
{
    public class RS2000Facade : FacadeBase, IRSFacade
    {
        private readonly ReportingService webserviceProxy = new ReportingService();

        public string WebServiceUrl
        {
            get { return webserviceProxy.Url; }
            set { webserviceProxy.Url = value; }
        }

        public void CreateFolder(string folder, string parent, string properties)
        {
            webserviceProxy.CreateFolder(folder, parent, null);
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

        public ReportWarning[] CreateReport(string filename, string destination, bool overwrite, byte[] definition, string properties)
        {
            var warnings = webserviceProxy.CreateReport(Path.GetFileNameWithoutExtension(filename), destination, overwrite, definition, null);

            return warnings != null ? Array.ConvertAll(warnings, ConvertSPWarningToReportWarning) : null;
        }

        public ReportWarning[] CreateModel(string filename, string destination, byte[] definition, string properties)
        {
            return null;
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
            throw new Exception("The method or operation is not implemented.");
        }

        public List<string> GetReportDatasources(string path)
        {
            return webserviceProxy.GetReportDataSources(path).Select(ds => ds.Name).ToList();
        }

        public byte[] GetReportDefinition(string path)
        {
            return webserviceProxy.GetReportDefinition(path);
        }

        public List<List<string>> GetReportParameters(string path)
        {
            return webserviceProxy.GetReportParameters(path, null, false, null, null)
                .Select(parameter => new List<String>
                                    {
                                        parameter.Name, parameter.Type.ToString(), parameter.AllowBlank.ToString(), parameter.Nullable.ToString(), parameter.MultiValue.ToString(), parameter.Prompt
                                    })
                .ToList();
        }

        public ReportItemDTO[] ListChildren(string item, bool recursive)
        {
            return Array.ConvertAll(webserviceProxy.ListChildren(item, recursive), ConvertCatalogItemToReportItemDTO);
        }

        public void MoveItem(string source, string destination, ReportItemTypes type)
        {
            webserviceProxy.MoveItem(source, destination.StartsWith("/") ? destination : "/" + destination);
        }

        public void SetItemDataSources(string item, string dataSourceName)
        {
            foreach (var availableDataSource in webserviceProxy.GetReportDataSources(item))
            {
                if (availableDataSource.Name != dataSourceName) continue;

                try
                {
                    var dsr = new DataSourceReference
                                  {
                                      Reference = dataSourceName
                                  };

                    var dataSources = new DataSource[1];

                    var ds = new DataSource
                                 {
                                     Item = dsr,
                                     Name =
                                         ("/" +
                                          dataSourceName.Split('/')[
                                              dataSourceName.Split('/').GetUpperBound(0)]).Trim('/')
                                 };
                    dataSources[0] = ds;

                    webserviceProxy.SetReportDataSources(item, dataSources);

                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("An error has occured: {0}", ex.Message));
                }
            }
        }

        public void SetItemSecurity(string itemPath, Dictionary<string, string[]> policies)
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

        private static ReportItemTypes GetReportItemTypeFromSSRSItemType(ItemTypeEnum ssrsType)
        {
            var convertedType = ReportItemTypes.Unknown;

            switch (ssrsType)
            {
                case ItemTypeEnum.Folder: convertedType = ReportItemTypes.Folder; break;
                case ItemTypeEnum.Report: convertedType = ReportItemTypes.Report; break;
                case ItemTypeEnum.DataSource: convertedType = ReportItemTypes.Datasource; break;
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

        public void CreateDataSource(Datasource datasource, string parent)
        {
            var datasourceDefinition = new DataSourceDefinition
                          {
                              ConnectString = datasource.ConnectionString,
                              Enabled = datasource.Enabled,
                              Extension = datasource.Extension,
                              Password = datasource.Password,
                              Prompt = datasource.Prompt,
                              UserName = datasource.Username,
                              ImpersonateUser = datasource.SetExecutionContext,
                              WindowsCredentials =
                                  datasource.UsePromptedCredentialsAsWindowsCredentials ||
                                  datasource.UseStoredCredentialsAsWindowsCredentials,
                              CredentialRetrieval =
                                  GetSSRSCredentialRetrievalTypeFromEnum(
                                      datasource.CredentialRetrievalType)
                          };

            webserviceProxy.CreateDataSource(datasource.Name, parent, true, datasourceDefinition, null);
        }

        public List<DatasourceExtension> GetDataExtensions()
        {
            return webserviceProxy.ListExtensions(ExtensionTypeEnum.Data)
                .Select(extension => new DatasourceExtension(extension.Name, extension.LocalizedName))
                .ToList();
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

        public Datasource GetDatasource(string path)
        {
            var datasourceDefiniton = webserviceProxy.GetDataSourceContents(path);
            var datasource = new Datasource
                         {
                             ConnectionString = datasourceDefiniton.ConnectString,
                             CredentialRetrievalType =
                                 GetCredentialRetrievalTypeFromSSRSType(datasourceDefiniton.CredentialRetrieval),
                             Enabled = datasourceDefiniton.Enabled,
                             Extension = datasourceDefiniton.Extension,
                             Username = datasourceDefiniton.UserName,
                             Password = datasourceDefiniton.Password,
                             Prompt = datasourceDefiniton.Prompt,
                             SetExecutionContext = datasourceDefiniton.ImpersonateUser,
                             UseStoredCredentialsAsWindowsCredentials = datasourceDefiniton.WindowsCredentials,
                             UsePromptedCredentialsAsWindowsCredentials = datasourceDefiniton.WindowsCredentials
                         };

            return datasource;
        }

        public List<ReportItemDTO> ListDependantItems(string reportModelpath)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public IEnumerable<string> ListRoles()
        {
            return webserviceProxy.ListRoles().Select(r => r.Name);
        }
    }
}
