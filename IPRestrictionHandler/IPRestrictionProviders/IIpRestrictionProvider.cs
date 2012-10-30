using IPRestriction.Handler.Configuration;

namespace IPRestriction.Handler.IPRestrictionProviders
{
    public interface IIpRestrictionProvider
    {
        IProviderSettings ProviderSettings { get; }

        /// <summary>
        /// determines whether to apply restriction
        /// </summary>
        /// <returns></returns>
        bool NeedRestriction(string ipAddress);

        /// <summary>
        /// make appropriate restriction
        /// </summary>
        /// <param name="ipAddress"></param>
        void ApplyRestriction(string ipAddress);

        /// <summary>
        /// remove appropriate restriction
        /// </summary>
        /// <param name="ipAddress"></param>
        void RemoveRestriction(string ipAddress);

        bool IsAllowed(string ipAddress);
    }
}
