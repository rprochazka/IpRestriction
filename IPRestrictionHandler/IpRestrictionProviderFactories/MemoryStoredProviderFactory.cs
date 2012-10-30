using IPRestriction.Handler.Configuration;
using IPRestriction.Handler.IPRestrictionProviders;
using IpRestriction.Logger.Domain.Custom;

namespace IPRestriction.Handler.IpRestrictionProviderFactories
{
    public class MemoryStoredProviderFactory : IProviderFactory
    {                
        public IIpRestrictionProvider CreateProvider(IUnitOfWork unitOfWork, IProviderSettings providerSettings)
        {
            return new MemoryStoredIpRestrictionProvider(unitOfWork, providerSettings);
        }
    }
}
