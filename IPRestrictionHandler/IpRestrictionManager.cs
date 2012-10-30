using System;
using System.Collections.Generic;
using System.Linq;
using IPRestriction.Handler.IPRestrictionProviders;
using IpRestriction.Logger.Domain;
using IpRestriction.Logger.Domain.Custom;

namespace IPRestriction.Handler
{
    public class IpRestrictionManager : IIpRestrictionManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEnumerable<IIpRestrictionProvider> _restrictionProviders;

        public IpRestrictionManager(IUnitOfWork unitOfWork, IEnumerable<IIpRestrictionProvider> restrictionProviders)
        {
            _unitOfWork = unitOfWork;
            _restrictionProviders = restrictionProviders;
        }

        public void LogAttempt(string ipAddress, string page)
        {
            _unitOfWork.RequestValidationExceptionLogs.Add(new RequestValidationExceptionLog
                                                               {
                                                                   IpAddress = ipAddress,
                                                                   Page = page,
                                                                   LogTime = DateTime.Now
                                                               });
            _unitOfWork.Commit();

            
            _restrictionProviders.ToList()
                .ForEach((provider)=>
                                {
                                    if (provider.NeedRestriction(ipAddress))
                                    {
                                        provider.ApplyRestriction(ipAddress);
                                    }
                                });
        }

        public bool IsAllowed(string ipAddress, string page)
        {
            return _restrictionProviders.All(provider => provider.IsAllowed(ipAddress));
        }
    }
}
