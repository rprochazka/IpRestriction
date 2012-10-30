using System;
using System.Web;
using IPRestriction.Handler;

namespace IPRestriction.Modules
{
    public class IpRestrictionHttpModule : IHttpModule
    {
        private IIpRestrictionManager _restrictionManager;

        #region IHttpModule implementation
        public void Init(HttpApplication context)
        {
            context.Error += OnError;
            context.BeginRequest += OnBeginRequest;            
        }

        public void Dispose() { }
        #endregion

        #region event handlers
        void OnBeginRequest(object sender, EventArgs e)
        {
            InitIpRestrictionManager();

            HttpContext context = ((HttpApplication)sender).Context;
            string ipAddress = context.Request.UserHostAddress;
            string page = context.Request.Url.LocalPath;
            if (!_restrictionManager.IsAllowed(ipAddress, page))
            {
                context.Response.StatusCode = 403;  // (Forbidden)
            }
        }

        void OnError(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            var currentContext = application.Context;            
            var currentException = application.Server.GetLastError();
            
            if (currentException is HttpRequestValidationException)
            {                
                string ipAddress = currentContext.Request.UserHostAddress;
                string page = currentContext.Request.Url.LocalPath;
                _restrictionManager.LogAttempt(ipAddress, page);

                //application.Server.ClearError();
                //if (!_ipRestrictionProvider.IsIpAddressAllowed(ipAddress))
                //{
                //    currentContext.Response.StatusCode = 403;  // (Forbidden)
                //}
                //else
                //{
                //    if (currentContext.Request.UrlReferrer != null)
                //        currentContext.Response.Redirect(currentContext.Request.UrlReferrer.AbsoluteUri, true);
                //}                                
            }                        
        }
        #endregion

        private void InitIpRestrictionManager()
        {
            _restrictionManager = ServiceLocator.GetRestrictionManager();
        }
    }
}