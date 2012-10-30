using System.Collections.Generic;

namespace IPRestriction.Handler.Configuration
{
    public interface IConfigurationProvider
    {
        IEnumerable<IProviderSettings> GetProviderSettings();
    }
}
