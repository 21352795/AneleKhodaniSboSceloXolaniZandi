using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Template.BusinessLogic;

namespace Template.MVC5
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BootstrapBundleConfig.RegisterBundles();
            InitializeBusiness.Initilize();
        }
    }
}
