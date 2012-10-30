using System;
using System.Web;
using System.Web.Caching;

namespace IPRestriction.Handler.IPRestrictionProviders
{
    public class ExpiredIpRestrictionProvider : IpRestrictionProviderDecorator
    {   
        public ExpiredIpRestrictionProvider(IIpRestrictionProvider ipRestrictionProvider) : base(ipRestrictionProvider)
        {
        }

        public override void ApplyRestriction(string ipAddress)
        {
            base.ApplyRestriction(ipAddress);
            CreateExpirableIpRestriction(ipAddress);
        }

        /// <summary>
        /// creates a cache item for each blocked IP address with expiration time and a removal callback
        /// the callback is used to remove the IP from the blacklist
        /// </summary>
        /// <param name="ipAddress"></param>
        private void CreateExpirableIpRestriction(string ipAddress)
        {
            const string cachePrefix = "BlockedIP_";
            string cacheItemKey = String.Format("{0}{1}", cachePrefix, ipAddress);
            HttpContext.Current.Cache.Insert(
                cacheItemKey, "true", null, DateTime.Now.AddSeconds(ProviderSettings.DisableAccessInSeconds),
                Cache.NoSlidingExpiration, CacheItemPriority.Default, (key, value, reason) =>
                {
                    if (reason != CacheItemRemovedReason.Removed)
                    {
                        base.RemoveRestriction(ipAddress);
                    }
                });
        }
    }
}
