using System;
using System.Collections.Generic;
using System.Text;

namespace RSS_Report_Retrievers.Classes
{
    class ServerRegistry
    {
        public IList<ServerSettingDTO> GetServerSettings()
        {
            return null;
        }

        public bool StoreNewSetting(ServerSettingDTO newSetting)
        {
            return false;
        }

        public bool DeleteSetting(ServerSettingDTO existingSetting)
        {
            return false;
        }
    }

    public class ServerSettingDTO
    {
        public string VisibleName;
        
        public string url = null;
        public bool isSharePointMode = false;
        public string reportLibrary = null;

        public bool useWindowsAuth = false;
        public string windowsDomain = null;
        public string windowsUsername = null;
        public string windowsPassword = null;
    }
}
