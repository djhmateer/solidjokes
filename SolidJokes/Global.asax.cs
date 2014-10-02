using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SolidJokes.Web {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Patch in our own Factory/Composition Root into MVC pipeline
            var controllerFactory = new SolidJokesControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}
