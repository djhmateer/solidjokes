using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SolidJokes.Controllers;

namespace SolidJokes {
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

    public class SolidJokesControllerFactory : DefaultControllerFactory {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
            var logger = new Logger();
            if (controllerType == typeof(HomeController)) {
                return new HomeController(logger);
            }
            // AccountController will still go the normal tightly coupled way
            return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}
