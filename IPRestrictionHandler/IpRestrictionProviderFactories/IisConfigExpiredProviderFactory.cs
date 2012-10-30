using IPRestriction.Handler.Configuration;
using IPRestriction.Handler.IPRestrictionProviders;
using IpRestriction.Logger.Domain.Custom;

namespace IPRestriction.Handler.IpRestrictionProviderFactories
{
    public class IisConfigExpiredProviderFactory : IProviderFactory
    {
        public IIpRestrictionProvider CreateProvider(IUnitOfWork unitOfWork, IProviderSettings providerSettings)
        {
            IIpRestrictionProvider provider = new IisRestrictionProvider(unitOfWork, providerSettings);
            provider = new ExpiredIpRestrictionProvider(provider);
            return provider;
        }
    }
}
