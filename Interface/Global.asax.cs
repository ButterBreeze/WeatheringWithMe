using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;

namespace Interface
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly ILog _log = LogManager.GetLogger("AdoNetAppenderException");
            
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        
        protected void Application_Error(object sender, EventArgs e)
        {
            // Get the exception object.
            var ex = Server.GetLastError();
            
            _log.Error(ex.Message, ex);
        }
    }
}