using System.Linq;
using IPRestriction.Handler.Configuration;
using IpRestriction.Logger.Domain.Custom;
using Microsoft.Web.Administration;

namespace IPRestriction.Handler.IPRestrictionProviders
{
    public class IisRestrictionProvider : IIpRestrictionProvider
    {
        private const string WebSite = "Default Web Site";

        private readonly IUnitOfWork _unitOfWork;        

        public IisRestrictionProvider(IUnitOfWork unitOfWork, IProviderSettings providerSettings)
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
            ToggleIpAddressBlocking(WebSite, ipAddress, true);
        }

        public void RemoveRestriction(string ipAddress)
        {
            ToggleIpAddressBlocking(WebSite, ipAddress, false);
            _unitOfWork.RequestValidationExceptionLogs.Remove(l => l.IpAddress.Equals(ipAddress));
            _unitOfWork.Commit();
        }

        public bool IsAllowed(string ipAddress)
        {
            //var attemptCount = _unitOfWork.RequestValidationExceptionLogs.Get(l => l.IpAddress.Equals(ipAddress)).Count();
            //return (attemptCount <= _providerSettings.AttemptThreshold);

            //this is handled by IIS server internally
            return true;
        }

        /// <summary>
        /// manages the ip restriction list (blacklist) in the IIS config (file applicationHost.config)
        /// </summary>
        /// <param name="site"></param>
        /// <param name="ip"></param>
        /// <param name="block"></param>
        static void ToggleIpAddressBlocking(string site, string ip, bool block)
        {
            var serverManager = new ServerManager();
            var hostConfig = serverManager.GetApplicationHostConfiguration();
            // Get ipSecurity section in configuration.
            var ipSecuritySection = hostConfig.GetSection("system.webServer/security/ipSecurity", site);
            var configElements = ipSecuritySection.GetCollection();
            // Check if this ip address is already blocked.
            var ipExists = false;
            foreach (var elem in configElements)
            {
                if (elem.ElementTagName == "add")
                {
                    var ipaddr = elem.Attributes["ipAddress"];
                    if (null == ipaddr)
                    {
                        continue;
                    }
                    if (ipaddr.Value.ToString() == ip)
                    {
                        ipExists = true;

                        // Simple change this attribute to false and we are done.
                        elem.Attributes["allowed"].Value = !block;
                        break;
                    }
                }
            }
            if (block && !ipExists)
            {
                // Create new element and add it to collection.
                var newElement = configElements.CreateElement("add");
                newElement.Attributes["ipAddress"].Value = ip;
                newElement.Attributes["allowed"].Value = false;
                configElements.Add(newElement);
            }

            serverManager.CommitChanges();
            serverManager.Dispose();
            //Thread.Sleep(5000);
        }

    }
}
