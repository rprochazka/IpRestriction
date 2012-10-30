using System.Collections.Generic;
using System.Configuration;

namespace IPRestriction.Handler.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly IPRestrictionConfigurationSection _configurationSection;

        public ConfigurationProvider()
        {
            _configurationSection =
                ((IPRestrictionConfigurationSection) ConfigurationManager.GetSection("dynamicIPRestrictions"));
        }

        public IEnumerable<IProviderSettings> GetProviderSettings()
        {
            var settings = new List<IProviderSettings>();
            var providers = _configurationSection.Providers;
            if (null != providers)
            {
                for (int i = 0; i < providers.Count; i++)
                {
                    var provider = providers[i];
                    settings.Add(new ProviderSettings
                                     {
                                         ClassName = provider.ClassFullName,
                                         AttemptThreshold = provider.AttemptThreshold,
                                         DisableAccessInSeconds = provider.DisableAccessInSeconds
                                     });
                }
            }

            return settings;
        }
    }
}
