using System.Configuration;

namespace ReportingServerManager.Logic.Configuration
{
    public class ServerSettingsConfigElementCollection : ConfigurationElementCollection
    {
        public ServerSettingsConfigElement Get(string key)
        {
            return (ServerSettingsConfigElement)BaseGet(key); 
        }
	

        protected override ConfigurationElement CreateNewElement()
        {
            return new ServerSettingsConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServerSettingsConfigElement)element).Alias;
        }

        public void Add(ServerSettingsConfigElement el)
        {
            if (BaseGet(GetElementKey(el)) != null)
                BaseRemove(GetElementKey(el));

            BaseAdd(el,true);
        }

        public void Remove(ServerSettingsConfigElement el)
        {
            BaseRemove(el.Alias);
        }
    }
}