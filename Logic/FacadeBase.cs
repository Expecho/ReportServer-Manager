namespace ReportingServerManager.Logic
{
    public abstract  class FacadeBase
    {
        public string BaseUrl { get; set; }

        public bool PathIncludesExtension { get; set; }

        public bool NativeMode { get; set; }

        public string SiteUrl { get; set; }
    }
}
