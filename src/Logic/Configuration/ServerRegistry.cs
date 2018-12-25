using System.Configuration;

namespace ReportingServerManager.Logic.Configuration
{
    class ServerRegistry
    {
        private static ServerSettingsSection section;
        private static System.Configuration.Configuration config;

        public static ServerSettingsConfigElementCollection GetServerSettings()
        {
            InitConfig();
            
            return section.Servers;
        }

        private static void InitConfig()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var sectionGroups = config.SectionGroups;

            var sectionGroup = sectionGroups["applicationSettings"];

            if (sectionGroup != null) 
                section = (ServerSettingsSection)sectionGroup.Sections["ServerSettings"];
        }

        public static void AddElement(ServerSettingsConfigElement el)
        {
            section.Servers.Add(el);
        }

        public static void StoreSettings()
        {
            config.Save(ConfigurationSaveMode.Full);
        }

        public static void RemoveElement(ServerSettingsConfigElement el)
        {
            section.Servers.Remove(el);
        }
    }
}
