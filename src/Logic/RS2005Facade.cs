using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ReportingServerManager.RSS_2005_NATIVE;
using ReportingServerManager.Logic.Shared;

namespace ReportingServerManager.Logic
{
    public class RS2005Facade : FacadeBase, IRSFacade
    {
        private readonly ReportingService2005 webserviceProxy = new ReportingService2005();

        public string WebServiceUrl
        {
            get { return webserviceProxy.Url; }
            set { webserviceProxy.Url = value; }
        }

        public void CreateFolder(string folder, string parent, string properties)
        {
            webserviceProxy.CreateFolder(folder, parent, null);
        }

        public ReportWarning[] CreateReport(string filename, string destination, bool overwrite, Byte[] definition, string properties)
        {
            var warnings = webserviceProxy.CreateReport(Path.GetFileNameWithoutExtension(filename), destination, overwrite, definition, null);

            return warnings != null ? Array.ConvertAll(warnings, ConvertSPWarningToReportWarning) : null;
        }

        public ReportWarning[] CreateModel(string visibleName, string parentFolder, Byte[] definition, string properties)
        {
            return Array.ConvertAll(webserviceProxy.CreateModel(visibleName, parentFolder, definition, null), ConvertSPWarningToReportWarning);
        }

        public System.Net.ICredentials Credentials
        {
            get { return webserviceProxy.Credentials; }
            set { webserviceProxy.Credentials = value; }
        }

        public void DeleteItem(string path)
        {
            webserviceProxy.DeleteItem(path);
        }

        public List<List<String>> GetItemProperties(string path)
        {
            return webserviceProxy.GetProperties(path, null).Select(property => new List<String>
                                                                                    {
                                                                                        property.Name, property.Value
                                                                                    }).ToList();
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

        public List<String> GetReportDatasources(string path)
        {
            return webserviceProxy.GetItemDataSources(path)
                .Select(datasource => datasource.Item)
                .OfType<DataSourceReference>()
                .Select(dataSourceReference => dataSourceReference.Reference)
                .ToList();
        }

        public byte[] GetReportDefinition(string path)
        {
            return webserviceProxy.GetReportDefinition(path);
        }

        public List<List<String>> GetReportParameters(string path)
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
            var catItems = webserviceProxy.ListChildren(item, recursive);

            return Array.ConvertAll(catItems, ConvertCatalogItemToReportItemDTO);
        }

        public void MoveItem(string source, string destination, ReportItemTypes type)
        {
            webserviceProxy.MoveItem(source, destination.StartsWith("/") ? destination : "/" + destination);
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

        public void SetItemSecurity(string itemPath, Dictionary<string, string[]> policies)
        {
            if (policies == null)
            {
                webserviceProxy.SetPolicies(itemPath, new Policy[] {});
                return;
            }

            webserviceProxy.SetPolicies(
                itemPath,
                policies.Keys.Select(userName => new Policy
                {
                    GroupUserName = userName,
                    Roles = Array.ConvertAll(policies[userName], roleName =>
                        {
                            var r = new Role {Name = roleName};
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
                                               ConnectString = datasource.ConnectionString,
                                               Enabled = datasource.Enabled,
                                               Extension = datasource.Extension,
                                               Password = datasource.Password,
                                               Prompt = datasource.Prompt,
                                               UserName = datasource.Username,
                                               ImpersonateUser = datasource.SetExecutionContext,
                                               WindowsCredentials =
                                                   datasource
                                                       .UsePromptedCredentialsAsWindowsCredentials ||
                                                   datasource.UseStoredCredentialsAsWindowsCredentials,
                                               CredentialRetrieval =
                                                   GetSSRSCredentialRetrievalTypeFromEnum(
                                                       datasource.CredentialRetrievalType)
                                           };

            webserviceProxy.CreateDataSource(datasource.Name, parent, true, dataSourceDefinition, null);
        }

        public List<DatasourceExtension> GetDataExtensions()
        {
            return webserviceProxy.ListExtensions(ExtensionTypeEnum.Data)
                .Select(extension => new DatasourceExtension(extension.Name, extension.LocalizedName))
                .ToList();
        }

        public Datasource GetDatasource(string path)
        {
            var dataSourceContents = webserviceProxy.GetDataSourceContents(path);
            var datasource = new Datasource
                                 {
                                     ConnectionString = dataSourceContents.ConnectString,
                                     CredentialRetrievalType =
                                         GetCredentialRetrievalTypeFromSSRSType(
                                             dataSourceContents.CredentialRetrieval),
                                     Enabled = dataSourceContents.Enabled,
                                     Extension = dataSourceContents.Extension,
                                     Username = dataSourceContents.UserName,
                                     Password = dataSourceContents.Password,
                                     Prompt = dataSourceContents.Prompt,
                                     SetExecutionContext = dataSourceContents.ImpersonateUser,
                                     UseStoredCredentialsAsWindowsCredentials =
                                         dataSourceContents.WindowsCredentials,
                                     UsePromptedCredentialsAsWindowsCredentials =
                                         dataSourceContents.WindowsCredentials
                                 };

            return datasource;
        }

        public List<ReportItemDTO> ListDependantItems(string reportModelpath)
        {
            var items = webserviceProxy.ListDependentItems(reportModelpath);

            return items.Select(item => new ReportItemDTO
                                            {
                                                Name = item.Name,
                                                Path = item.Path
                                            }).ToList();
        }

        public IEnumerable<string> ListRoles()
        {
            return webserviceProxy.ListRoles(SecurityScopeEnum.Catalog).Select(r => r.Name);
        }

        public List<ReportItemDTO> LoadDependantItems(string reportModelpath)
        {
            throw new Exception("The method or operation is not implemented.");
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

        private static ReportWarning ConvertSPWarningToReportWarning(Warning warning)
        {
            var returnWarning = new ReportWarning();

            if (warning != null)
            {
                returnWarning.Code = warning.Code;
                returnWarning.Message = warning.Message;
                returnWarning.ObjectName = warning.ObjectName;
                returnWarning.ObjectType = warning.ObjectType;
                returnWarning.Severity = warning.Severity;
            }

            return returnWarning;
        }
    }
}
