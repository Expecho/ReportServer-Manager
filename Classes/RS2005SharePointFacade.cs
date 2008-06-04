using System;
using System.Collections.Generic;
using System.Text;
using RSS_Report_Retrievers.RSS_SP;
using RSS_Report_Retrievers.Classes;

namespace RSS_Report_Retrievers.Classes
{
    class RS2005SharePointFacade : IRSFacade
    {
        private ReportingService2006 rs = new ReportingService2006();

        #region IRSFacade Members

        public void CreateFolder(string Folder, string text, string properties)
        {
            rs.CreateFolder(Folder, text);
        }

        public void CreateReport(string filename, string destination, bool overwrite, byte[] definition, string Properties, out ReportWarning[] warnings)
        {
            Warning[] SpWarnings;
            rs.CreateReport(System.IO.Path.GetFileName(filename), destination, overwrite, definition, null, out SpWarnings);

            warnings = Array.ConvertAll<Warning, ReportWarning>(SpWarnings, ConvertSPWarningToReportWarning);
        }
        private static ReportWarning ConvertSPWarningToReportWarning(Warning w)
        {
            ReportWarning returnWarning;
            returnWarning.Code = w.Code;
            returnWarning.Message = w.Message;
            returnWarning.ObjectName = w.ObjectName;
            returnWarning.ObjectType = w.ObjectType;
            returnWarning.Severity = w.Severity;

            return returnWarning;
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
            Property propertyToLoad;

            List<Property> propertiesToLoad = new List<Property>();
            propertyToLoad = new Property();
            propertyToLoad.Name = "Description";
            propertiesToLoad.Add(propertyToLoad);
            propertyToLoad = new Property();
            propertyToLoad.Name = "Name";
            propertiesToLoad.Add(propertyToLoad);

            foreach (Property property in rs.GetProperties(path, null)) // propertiesToLoad
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
            return rs.GetModelDefinition(path);
        }

        public List<string> GetReportDatasources(string path)
        {
            List<String> datasources = new List<string>();
            foreach (DataSource ds in rs.GetItemDataSources(path))
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

            foreach (ReportParameter parameter in rs.GetReportParameters(path, null, null, null))
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
            return Array.ConvertAll<CatalogItem, ReportItemDTO>(rs.ListChildren(item), ConvertCatalogItemToReportItemDTO);
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

            rs.MoveItem(source, destination);
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
