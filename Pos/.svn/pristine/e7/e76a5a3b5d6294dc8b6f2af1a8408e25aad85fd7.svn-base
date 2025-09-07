using System;
using System.Configuration;

namespace Lib.Utilities
{
    public class AppSettings
    {
        public string AppName { get { return GetSetting("AppName"); } }
        public string AppAbbr { get { return GetSetting("AppAbbr"); } }

        internal string GetSetting(string settingKey)
        {
            try { return ConfigurationManager.AppSettings[settingKey]; }
            catch (Exception ex) { throw ex; }
        }
    }
}
