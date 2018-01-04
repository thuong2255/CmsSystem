using CmsSystem.Web.App_Start;
using System.Web.Mvc;
using System.Web.Routing;

namespace CmsSystem.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Bootstrapper.Run();
        }
    }
}