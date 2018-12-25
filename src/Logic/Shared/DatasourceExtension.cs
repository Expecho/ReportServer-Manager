namespace ReportingServerManager.Logic.Shared
{
    public class DatasourceExtension
    {
        public DatasourceExtension(string name, string friendlyName)
        {
            Name = name;
            FriendlyName = friendlyName; 
        }

        public string Name { get; set; }
        public string FriendlyName { get; set; }
    }
}