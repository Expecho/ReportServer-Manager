using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace RSS_Report_Retrievers.Classes
{
    class ServerRegistry
    {
        private static ServerSettingsSection section = null;
        private static Configuration config = null;

        public static ServerSettingsConfigElementCollection GetServerSettings()
        {
            InitConfig();
            
            return section.Servers;
        }

        private static void InitConfig()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            ConfigurationSectionGroupCollection sectionGroups = config.SectionGroups;

            ConfigurationSectionGroup sectionGroup = sectionGroups["applicationSettings"];

            section = (ServerSettingsSection)sectionGroup.Sections["ServerSettings"];
        }

        public static void AddElement(ServerSettingsConfigElement el)
        {
            section.Servers.Add(el);
        }

        public static void StoreSettings()
        {
 //           System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //config.Sections.Remove("ServerSettings");

//            ServerSettingsSection newSection = new ServerSettingsSection();

//            config.Sections.Add("ServerSettings", newSection);

            config.Save(ConfigurationSaveMode.Full);
        }

        public static void RemoveElement(ServerSettingsConfigElement el)
        {
            section.Servers.Remove(el);
        }
    }
}
