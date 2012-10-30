using IPRestriction.Handler.Configuration;

namespace IPRestriction.Handler.IPRestrictionProviders
{
    public class IpRestrictionProviderDecorator : IIpRestrictionProvider
    {
        private readonly IIpRestrictionProvider _ipRestrictionProvider;        

        public IpRestrictionProviderDecorator(IIpRestrictionProvider restrictionProvider)
        {
            _ipRestrictionProvider = restrictionProvider;            
        }

        public IProviderSettings ProviderSettings
        {
            get { return _ipRestrictionProvider.ProviderSettings; }
        }

        public virtual bool NeedRestriction(string ipAddress)
        {
            return _ipRestrictionProvider.NeedRestriction(ipAddress);
        }

        public virtual void ApplyRestriction(string ipAddress)
        {
            _ipRestrictionProvider.ApplyRestriction(ipAddress);
        }

        public virtual void RemoveRestriction(string ipAddress)
        {
            _ipRestrictionProvider.RemoveRestriction(ipAddress);
        }

        public virtual bool IsAllowed(string ipAddress)
        {
            return _ipRestrictionProvider.IsAllowed(ipAddress);
        }
    }
}
