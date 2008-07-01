using System;
using System.Collections.Generic;
using System.Text;
using RSS_Report_Retrievers.RSS;
using RSS_Report_Retrievers.Classes;

namespace RSS_Report_Retrievers.Classes
{
    public class RS2000Facade : IRSFacade
    {
        private ReportingService rs = new ReportingService();

        private string baseUrl;

        public string BaseUrl
        {
            get { return baseUrl; }
            set { baseUrl = value; }
        }
	

        #region IRSFacade Members

        public void CreateFolder(string Folder, string text, string properties)
        {
            rs.CreateFolder(Folder, text, null);
        }

        public void CreateReport(string filename, string destination, bool overwrite, byte[] definition, string Properties, out ReportWarning[] warnings)
        {
            warnings = null;
            rs.CreateReport(System.IO.Path.GetFileNameWithoutExtension(filename), destination, overwrite, definition, null);
        }

        public System.Net.ICredentials Credentials
        {
            get
            {
                return rs.Credentials;
            }
            set
            {
                rs.Credentials = value;
            }
        }

        public void DeleteItem(string path)
        {
            rs.DeleteItem(path);
        }

        public List<List<string>> GetItemProperties(string path)
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

        public List<List<string>> GetItemSecurity(string path)
        {
            bool inheritParent;

            List<List<String>> permissions = new List<List<string>>();
            foreach (Policy permission in rs.GetPolicies(path, out inheritParent))
            {
                List<String> permissioninfo = new List<String>();
                string roles = "";
                permissioninfo.Add(permission.GroupUserName);
                foreach (Role role in permission.Roles)
                {
                    roles += role.Name + ",";
                }
                permissioninfo.Add(inheritParent.ToString());
                permissioninfo.Add(roles.TrimEnd(','));
                permissions.Add(permissioninfo);
            }

            return permissions;
        }

        public byte[] GetModelDefinition(string path)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public List<string> GetReportDatasources(string path)
        {
            List<String> datasources = new List<string>();
            foreach (DataSource ds in rs.GetReportDataSources(path))
            {
                datasources.Add(ds.Name);
            }

            return datasources;
        }

        public byte[] GetReportDefinition(string path)
        {
            return rs.GetReportDefinition(path);
        }

        public List<List<string>> GetReportParameters(string path)
        {
            List<List<String>> parameters = new List<List<string>>();

            foreach (RSS.ReportParameter parameter in rs.GetReportParameters(path, null, false, null, null))
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
            return Array.ConvertAll<CatalogItem,ReportItemDTO>(rs.ListChildren(item, recursive),ConvertCatalogItemToReportItemDTO);
        }

        public void MoveItem(string source, string destination, ReportItemTypes type)
        {
            rs.MoveItem(source, destination.StartsWith("/") ? destination : "/" + destination);
        }

        public void SetItemDataSources(string item, string dataSourceName)
        {
            foreach (DataSource availableDataSource in rs.GetReportDataSources(item))
            {
                // Only update the report when the selected datasource is used in that report
                if (availableDataSource.Name == dataSourceName)
                {
                    try
                    {
                        DataSourceReference dsr = new DataSourceReference();
                        dsr.Reference = dataSourceName;

                        DataSource[] dataSources = new DataSource[1];

                        DataSource ds = new DataSource();
                        ds.Item = (DataSourceDefinitionOrReference)dsr;
                        ds.Name = ("/" + dataSourceName.Split('/')[dataSourceName.Split('/').GetUpperBound(0)]).Trim('/');
                        dataSources[0] = ds;

                        rs.SetReportDataSources(item, dataSources);

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(String.Format("An error has occured: {0}", ex.Message));
                    }
                }
            }
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

    }
}
