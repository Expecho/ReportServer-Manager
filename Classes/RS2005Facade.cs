using System;
using System.Collections.Generic;
using System.Text;
using RSS_Report_Retrievers.RSS_2005;
namespace RSS_Report_Retrievers.Classes
{
    public class RS2005Facade : RSS_Report_Retrievers.Classes.IRSFacade
    {
        private ReportingService2005 rs = new ReportingService2005();

        private string baseUrl;

        public string BaseUrl
        {
            get { return baseUrl; }
            set { baseUrl = value; }
        }
	
        public System.Net.ICredentials Credentials
        {
            get { return rs.Credentials; }
            set { rs.Credentials = value; }
        }

        public ReportItemDTO[] ListChildren(string item, bool recursive)
        {
            CatalogItem[] catItems = rs.ListChildren(item, recursive);

            return Array.ConvertAll<CatalogItem, ReportItemDTO>(catItems, ConvertCatalogItemToReportItemDTO);
        }

        public void DeleteItem(string path)
        {
            rs.DeleteItem(path);
        }

        public void CreateFolder(string Folder, string Parent, string properties)
        {
            rs.CreateFolder(Folder, Parent, null);
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

        public void CreateReport(string filename, string destination, bool overwrite, Byte[] definition, string Properties, out ReportWarning[] warnings)
        {
            warnings = null;
            rs.CreateReport(System.IO.Path.GetFileNameWithoutExtension(filename), destination, overwrite, definition, null);            
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

        public List<List<String>> GetReportParameters(string path)
        {
            List<List<String>> parameters = new List<List<string>>();

            foreach (RSS_2005.ReportParameter parameter in rs.GetReportParameters(path, null, false, null, null))
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

        public List<List<String>> GetItemSecurity(string path)
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

        public byte[] GetReportDefinition(string path)
        {
            return rs.GetReportDefinition(path);
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
                datasources.Add(ds.Name);
            }

            return datasources;
        }

        public void MoveItem(string source, string destination, ReportItemTypes type)
        {
            rs.MoveItem(source, destination.StartsWith("/") ? destination : "/" + destination);
        }

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
    }
}
