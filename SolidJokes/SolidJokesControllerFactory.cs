using System;
using System.Web.Mvc;
using System.Web.Routing;
using SolidJokes.Controllers;

namespace SolidJokes {
    public class SolidJokesControllerFactory : DefaultControllerFactory {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
            if (controllerType == typeof(HomeController)) {
                //return new HomeController();
                return new HomeControllerLogger(new HomeController());
            }
            // AccountController will still go the normal tightly coupled way
            return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}