namespace ReportingServerManager.Logic.Shared
{
    public class ReportItemDTO
    {
        public string Name { get; set; }
        public bool Hidden { get; set; }
        public ReportItemTypes Type { get; set; }
        public string Path { get; set; }
    }
}