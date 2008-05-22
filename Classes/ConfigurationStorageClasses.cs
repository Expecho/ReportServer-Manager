using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace RSS_Report_Retrievers.Classes
{
    public class ServerSettingsSection : System.Configuration.ConfigurationSection
    {
        [ConfigurationProperty("Servers",IsDefaultCollection=false)]
        public ServerSettingsConfigElementCollection Servers
        {
            get {
                ServerSettingsConfigElementCollection coll = (ServerSettingsConfigElementCollection) base["Servers"];
                return coll;
                }
        }
    }

    public class ServerSettingsConfigElementCollection : ConfigurationElementCollection
    {


        public ServerSettingsConfigElement Get(string key)
        {
            return (ServerSettingsConfigElement) base.BaseGet(key); 
        }
	

        protected override ConfigurationElement CreateNewElement()
        {
            return new ServerSettingsConfigElement();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return base.CreateNewElement(elementName);
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServerSettingsConfigElement)element).Alias;
        }

        public void Add(ServerSettingsConfigElement el)
        {
            if (this.BaseGet(GetElementKey(el)) != null)
                base.BaseRemove(GetElementKey(el));

            base.BaseAdd(el,true);
        }

        public void Remove(ServerSettingsConfigElement el)
        {
            base.BaseRemove(el.Alias);
        }
    }

    public class ServerSettingsConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("alias")]
        public string Alias
        {
            get { return (string) this["alias"]; }
            set { this["alias"] = value; }
        }

        [ConfigurationProperty("isSQL2000")]
        public bool IsSQL2000
        {
            get { return (bool)this["isSQL2000"]; }
            set { this["isSQL2000"] = value; }
        }

        [ConfigurationProperty("url")]
        public string Url
        {
            get { return (string)this["url"]; }
            set { this["url"] = value; }
        }

        [ConfigurationProperty("isSharePointMode")]
        public bool IsSharePointMode
        {
            get { return (bool)this["isSharePointMode"]; }
            set { this["isSharePointMode"] = value; }
        }

        [ConfigurationProperty("reportLibrary")]
        public string ReportLibrary
        {
            get { return (string)this["reportLibrary"]; }
            set { this["reportLibrary"] = value; }
        }

        [ConfigurationProperty("useWindowsAuth")]
        public bool UseWindowsAuth
        {
            get { return (bool)this["useWindowsAuth"]; }
            set { this["useWindowsAuth"] = value; }
        }

        [ConfigurationProperty("windowsDomain")]
        public string WindowsDomain
        {
            get { return (string)this["windowsDomain"]; }
            set { this["windowsDomain"] = value; }
        }

        [ConfigurationProperty("windowsPwd")]
        public string WindowsPwd
        {
            get { return (string)this["windowsPwd"]; }
            set { this["windowsPwd"] = value; }
        }

        [ConfigurationProperty("windowsUsername")]
        public string WindowsUsername
        {
            get { return (string)this["windowsUsername"]; }
            set { this["windowsUsername"] = value; }
        }

        public ServerSettingsConfigElement Clone()
        {
            ServerSettingsConfigElement clone = new ServerSettingsConfigElement();

            foreach (ConfigurationProperty prop in this.Properties)
                clone[prop.Name] = this[prop.Name];

            return clone;
        }
    }
}
