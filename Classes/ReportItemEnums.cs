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

    public enum CredentialRetrievalTypes
    {
        None = 0,
        Prompt = 1,
        Integrated = 2,
        Store = 3
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

    public struct DatasourceExtension
    {
        public DatasourceExtension(string name, string friendlyName)
        {
            Name = name;
            FriendlyName = friendlyName; 
        }

        public string Name;
        public string FriendlyName;
    }

    public struct Datasource
    {
        public string Name;
        public string ConnectionString;
        public CredentialRetrievalTypes CredentialRetrievalType;
        public bool UsePromptedCredentialsAsWindowsCredentials;
        public string Prompt;
        public string Username;
        public string Password;
        public string Extension;
        public bool Enabled;
        public bool UseStoredCredentialsAsWindowsCredentials;
        public bool SetExecutionContext;
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
