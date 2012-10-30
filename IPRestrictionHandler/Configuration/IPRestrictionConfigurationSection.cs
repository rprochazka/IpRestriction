using System.Configuration;

namespace IPRestriction.Handler.Configuration
{
    public class IPRestrictionConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("providers")]
        public ProviderConfigurationElementCollection Providers
        {
            get { return this["providers"] as ProviderConfigurationElementCollection; }
        }
  
    }
}
