using IPRestriction.Handler.Configuration;
using IPRestriction.Handler.IPRestrictionProviders;
using IpRestriction.Logger.Domain.Custom;

namespace IPRestriction.Handler.IpRestrictionProviderFactories
{
    public class IISConfigProviderFactory : IProviderFactory
    {        
        public IIpRestrictionProvider CreateProvider(IUnitOfWork unitOfWork, IProviderSettings providerSettings)
        {
            return new IisRestrictionProvider(unitOfWork, providerSettings);
        }
    }
}
