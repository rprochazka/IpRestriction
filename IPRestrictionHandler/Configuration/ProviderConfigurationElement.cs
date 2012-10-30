using System.Configuration;

namespace IPRestriction.Handler.Configuration
{
    public class ProviderConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name")]
        public string Name
        {
            get { return this["name"].ToString(); }
        }

        [ConfigurationProperty("classFullName", IsRequired = true)]
        public string ClassFullName
        {
            get { return this["classFullName"].ToString(); }
        }

        [ConfigurationProperty("attemptThreshold")]
        public int AttemptThreshold
        {
            get { return (int)this["attemptThreshold"]; }
        }

        [ConfigurationProperty("disableAccessInSeconds")]
        public int DisableAccessInSeconds
        {
            get { return (int)this["disableAccessInSeconds"]; }
        }

    }
}