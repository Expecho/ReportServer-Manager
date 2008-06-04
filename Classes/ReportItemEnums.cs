using System;
using System.Collections.Generic;
using System.Text;

namespace RSS_Report_Retrievers.Classes
{
    public enum ReportItemTypes
    {
        Folder = 0,
        Datasource = 1,
        Report = 2,
        model = 3,
        Unknown = 4
    }

    public enum ViewItems
    {
        All = 0,
        Datasources = 1,
        Folders = 2
    }

    public struct ReportItemDTO
    {
        public string Name;
        public bool Hidden;
        public ReportItemTypes Type;
        public string Path;
    }

    public struct ReportWarning
    {
        public string Code;

        public string Severity;

        public string ObjectName;

        public string ObjectType;

        public string Message;
    }
}
