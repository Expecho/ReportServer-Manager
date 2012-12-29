namespace ReportingServerManager.Logic.Shared
{
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
}