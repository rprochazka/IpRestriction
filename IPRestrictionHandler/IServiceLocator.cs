using System.Collections.Generic;
using IPRestriction.Handler.IPRestrictionProviders;

namespace IPRestriction.Handler
{
    public interface IServiceLocator
    {
        IEnumerable<IIpRestrictionProvider> GetRestrictionProviders();
    }
}
