using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace Common.Configuration
{
    public abstract class AppConfigBase
    {
        private readonly string _sectionName;

        protected AppConfigBase(string sectionName = null)
        {
            if (!string.IsNullOrEmpty(sectionName) && ConfigurationManager.GetSection(sectionName) == null)
                throw new ArgumentException(string.Format("Section '{0}' does not exist in app config", sectionName), "sectionName");

            _sectionName = sectionName;
        }

        public string GetValue(string key)
        {
            if (!string.IsNullOrEmpty(_sectionName))
            {
                var section = ConfigurationManager.GetSection(_sectionName) as NameValueCollection;
                if (section != null && section.AllKeys.Contains(key))
                    return section[key];
            }
            else
            {
                if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
                    return ConfigurationManager.AppSettings[key];
            }
            return null;
        }
    }
}
