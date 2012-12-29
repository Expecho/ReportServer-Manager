using System.Configuration;

namespace ReportingServerManager.Logic.Configuration
{
    public class ServerSettingsConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("alias")]
        public string Alias
        {
            get { return (string) this["alias"]; }
            set { this["alias"] = value; }
        }

        [ConfigurationProperty("SQLServerVersion")]
        public string SQLServerVersion
        {
            get { return (string)this["SQLServerVersion"]; }
            set { this["SQLServerVersion"] = value; }
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
            get { 
                try
                {
                    return CryptoHelper.ToInsecureString(CryptoHelper.DecryptString((string)this["windowsPwd"]));
                }
                catch{
                    return (string)this["windowsPwd"];
                }
            }

            set { this["windowsPwd"] = CryptoHelper.EncryptString(CryptoHelper.ToSecureString(value)); }
        }

        [ConfigurationProperty("windowsUsername")]
        public string WindowsUsername
        {
            get { return (string)this["windowsUsername"]; }
            set { this["windowsUsername"] = value; }
        }

        public ServerSettingsConfigElement Clone()
        {
            var clone = new ServerSettingsConfigElement();

            foreach (ConfigurationProperty prop in Properties)
                clone[prop.Name] = this[prop.Name];

            return clone;
        }
    }
}