using System.Configuration;

namespace ReportingServerManager.Logic.Configuration
{
    public class ServerSettingsSection : ConfigurationSection
    {
        [ConfigurationProperty("Servers",IsDefaultCollection=false)]
        public ServerSettingsConfigElementCollection Servers
        {
            get 
            {
                var coll = (ServerSettingsConfigElementCollection) base["Servers"];
                return coll;
            }
        }
    }
}
