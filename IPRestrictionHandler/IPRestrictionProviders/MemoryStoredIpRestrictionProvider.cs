using System.Collections.Concurrent;
using System.Linq;
using IPRestriction.Handler.Configuration;
using IpRestriction.Logger.Domain.Custom;

namespace IPRestriction.Handler.IPRestrictionProviders
{
    public class MemoryStoredIpRestrictionProvider : IIpRestrictionProvider
    {
        private readonly IUnitOfWork _unitOfWork;

        public static ConcurrentDictionary<string,string> BlockedIpAddresses = new ConcurrentDictionary<string,string>();

        public MemoryStoredIpRestrictionProvider(IUnitOfWork unitOfWork, IProviderSettings providerSettings)
        {
            _unitOfWork = unitOfWork;
            ProviderSettings = providerSettings;
        }

        public IProviderSettings ProviderSettings { get; private set; }

        public bool NeedRestriction(string ipAddress)
        {
            var attemptCount = _unitOfWork.RequestValidationExceptionLogs.Get(l => l.IpAddress.Equals(ipAddress)).Count();
            return (attemptCount > ProviderSettings.AttemptThreshold);
        }

        public void ApplyRestriction(string ipAddress)
        {
            BlockedIpAddresses.AddOrUpdate(ipAddress, string.Empty, (k, v) => string.Empty );
        }

        public void RemoveRestriction(string ipAddress)
        {
            string value;
            BlockedIpAddresses.TryRemove(ipAddress, out value);
            _unitOfWork.RequestValidationExceptionLogs.Remove(l => l.IpAddress.Equals(ipAddress));
            _unitOfWork.Commit();
        }

        public bool IsAllowed(string ipAddress)
        {
            string value;
            return (!BlockedIpAddresses.TryGetValue(ipAddress, out value));
        }
    }
}
