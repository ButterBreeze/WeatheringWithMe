using System;
using System.Threading;
using System.Web.Mvc;
using log4net;

namespace Interface.Logging
{
    public class LogActionAttribute : ActionFilterAttribute
    {
        private readonly ILog _log = LogManager.GetLogger("AdoNetAppenderWebRequests");
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var controller = filterContext.RequestContext.RouteData.Values["Controller"];
            var action = filterContext.RequestContext.RouteData.Values["Action"];

            LogicalThreadContext.Properties["controller"] = controller;
            LogicalThreadContext.Properties["action"] = action;
            _log.Info("Web Request");

            base.OnActionExecuting(filterContext);
        }
    }
}