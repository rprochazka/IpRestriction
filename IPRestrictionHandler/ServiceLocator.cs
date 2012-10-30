using System;
using System.Collections.Generic;
using IPRestriction.Handler.Configuration;
using IPRestriction.Handler.IPRestrictionProviders;
using IPRestriction.Handler.IpRestrictionProviderFactories;
using IPRestriction.Logger.Data.Custom;
using IpRestriction.Logger.Domain.Custom;

namespace IPRestriction.Handler
{
    public class ServiceLocator
    {
        public static IUnitOfWork GetUnitOfWork()
        {
            return new EFUnitOfWork();
        }

        private static readonly IConfigurationProvider ConfigurationProvider = new ConfigurationProvider();
        public static IConfigurationProvider GetConfigurationProvider()
        {
            return ConfigurationProvider;
        }

        public static IpRestrictionManager GetRestrictionManager()
        {
            return new IpRestrictionManager(GetUnitOfWork(), GetRestrictionProviders());
        }
        
        private static IEnumerable<IIpRestrictionProvider> GetRestrictionProviders()
        {            
            var providerSettings = GetConfigurationProvider().GetProviderSettings();
            var providers = new List<IIpRestrictionProvider>();
            foreach (var providerSetting in providerSettings)
            {
                var providerFactory =
                    Activator.CreateInstance(Type.GetType(providerSetting.ClassName)) as
                    IProviderFactory;
                if (null == providerFactory)
                {
                    throw new Exception("Could not found the IFactoryProvider type");
                }

                providers.Add(providerFactory.CreateProvider(GetUnitOfWork(), providerSetting));
            }

            return providers;

        }

    }
}
